using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// This code is responsible for creating and moving dynamic entities.
/// 
/// </summary>

public class DrawCubePhysics : MonoBehaviour 
{

	public ProceduralMap ProceduralMapPhysics;
	public int DrawCubeScale = 1;
	public DynamicEntity[] DynamicEntities;
	//
	public int DynamicEntityCount = 1;
	//public Anims AnimsColl;
	//
	public Vector3 CollMove;
	public Vector3 RopeCollMove;
	//
	float Speed = 0.016f;
	//
	bool IsPlayerGrounded;
	//
	VectorRot VectorRotPlayerMovement;

	int NumOfColls = 0;

	void OnGUI()
	{
		/*GUI.Label(new Rect(50,110,200,30),
			"PlayerCellPos: " + PlayerCellPos.ToString());
		GUI.Label(new Rect(50,90,200,30), 
			"CellStaticObjectsCursor: " + GameObjectsCreation.EntityCells[PlayerCellPos].StaticIndecesCursor.ToString());
		GUI.Label(new Rect(50,70,200,30), 
			"CellStaticObjects: " + GameObjectsCreation.EntityCells[PlayerCellPos].CellStaticIndeces[0].ToString());
		GUI.Label(new Rect(50,50,200,30), 
			"Velocity: " + Velocity.ToString());*/

	}

	void PlayerMove()
	{
		if(DynamicEntities[0].IsColliding)
		{
			if(Input.GetKey(KeyCode.W))
			{
				DynamicEntities[0].Velocity
				+= DynamicEntities[0].DynamicObject.transform.forward * Speed;
			}
			if(Input.GetKey(KeyCode.S))
			{
				DynamicEntities[0].Velocity  
				+= -DynamicEntities[0].DynamicObject.transform.forward * Speed;
			}
			if(Input.GetKey(KeyCode.A))
			{
				DynamicEntities[0].Velocity
				+= -DynamicEntities[0].DynamicObject.transform.right * Speed;
			}
			if(Input.GetKey(KeyCode.D))
			{
				DynamicEntities[0].Velocity  
				+= DynamicEntities[0].DynamicObject.transform.right * Speed;
			}
			//we need a playergrounded condition with dot product
			if (Input.GetKey (KeyCode.Space) && IsPlayerGrounded) 
			{
				//Debug.Log ("bumm");
				DynamicEntities[0].Velocity 
				+= DynamicEntities[0].DynamicObject.transform.up * 10
					+ DynamicEntities[0].Velocity * 2
					//+ GameObjectsCreation.Player.transform.right * Speed;
					;
			}
		}
	}

	void MouseInput()
	{
		float MouseX = Input.GetAxis ("Mouse X");
		DynamicEntities[0].DynamicObject.transform.Rotate 
		(DynamicEntities[0].DynamicObject.transform.up * 5 * MouseX);

		float MouseY = Input.GetAxis ("Mouse Y");

		Camera.main.transform.position += VectorRotPlayerMovement.RotCam 
			(
				DynamicEntities[0].DynamicObject.transform.position,
				Camera.main.transform.position,
				DynamicEntities[0].DynamicObject.transform.right,
				MouseY * 1
			);

		Camera.main.transform.LookAt (DynamicEntities[0].DynamicObject.transform.position);
	}

	void GetWorldPos(int DynamicObjIndex)
	{
		DynamicEntities[DynamicObjIndex].WorldIndex = 
			//(int)
			(
				(int)(DynamicEntities[DynamicObjIndex].Pos.x / DrawCubeScale)//X
				* ProceduralMapPhysics.WorldCubeMap.Pitch2 
				+
				(int)(DynamicEntities[DynamicObjIndex].Pos.y / DrawCubeScale)//Y
				* ProceduralMapPhysics.WorldCubeMap.Pitch 
				+
				(int)(DynamicEntities[DynamicObjIndex].Pos.z / DrawCubeScale)//Z
			);
		//return DynamicObjIndex;
	}

	void SetWorldPos(int DynamicObjIndex)
	{
		ProceduralMapPhysics.WorldCubeMap.WorldIndeces 
		[
			DynamicEntities[DynamicObjIndex].WorldIndex
		] 
		= DynamicObjIndex;
	}


	//                 /\Reflection       Mov   Reflect*
	//                 ||                   \ | /
	//               * -- *--> Mov           \|/
	//               |    |                   o
	// - - - -Drag<--* -- * - - - - - - - - - - - -
	//                 ||
	//                 \/Coll
	//
	// The vector reflection formula is enough to get good results,
	// but in case of rolling objects the move component needs to be kept separate,
	// otherwise bounciness goes out of control.

	Vector3 CollAtomizeVector
	(
		Vector3 CollObjLoc, 
		Vector3 MovObjLoc, 
		int VelocityIndex,
		float CollCompDamp,
		float MovCompDamp
	)
	{
		NumOfColls++;

		Vector3 ObjLocDiff = CollObjLoc - MovObjLoc;
		Vector3 CollComponent;

		//here you deleted something collcnormal

		CollComponent = Vector3.Normalize (ObjLocDiff);
		//GameObjectsCreation.CollNormal = CollComponent;

		CollComponent *= CollCompDamp;
		Vector3 MovComponent = DynamicEntities[VelocityIndex].Velocity;

		MovComponent *= MovCompDamp;

		//NOTE: Preventing violation of preservation of force
		DynamicEntities[VelocityIndex].Velocity *= 1 - MovCompDamp;

		Vector3 Result = 
			MovComponent - 2 * //2 makes it bounce, 1 makes it slide
			Vector3.Dot 
			(
				MovComponent,
				CollComponent
			) 
			* CollComponent
			;
		//NOTE: Corrects sinkage into collider
		Result -= CollComponent //* 0.02f;
			* (1.6f - Vector3.Magnitude(ObjLocDiff));//to prevent sinking//10.1

		return Result;
	}

	void CollCheckCell
	(
		int CellIndexRelToPlayer, 
		int DynamicObjIndex
		//,Vector3 PosBuff
	)
	{
		//you dont need to calculate this
		//coz you know from the rel index how far away is the cubes center
		//but you need to convert bakc to sphere collider
		//Debug.Log(DynamicEntities[CellIndexRelToPlayer].WorldIndex);
		Vector3 PosBuff = ProceduralMapPhysics.CalcWorldPos
			(
				//DynamicEntities[DynamicObjIndex].WorldIndex
				//+ (CellIndexRelToPlayer - DynamicEntities[DynamicObjIndex].WorldIndex)
				CellIndexRelToPlayer
				//you need - coz when you come in you add ... try ot bring in not added data
			);
		//Debug.Log ("PlayerPos: " + DynamicEntities[DynamicObjIndex].Pos);
		//Debug.Log ("CollPos: " + PosBuff);

		if(
			//n != 0 && //not colliding iwth ourself //n != m &&
			Vector3.Distance
			(
				DynamicEntities[DynamicObjIndex].Pos,//n
				PosBuff
			)
			< 1.6f
			&& ProceduralMapPhysics.WorldCubeMap.WorldIndeces[CellIndexRelToPlayer] == 1
		)
		{
			//Debug.Log ("coll");

			DynamicEntities[DynamicObjIndex].IsColliding =  true;

			CollMove += 
				CollAtomizeVector 
				(
					PosBuff,  
					DynamicEntities[DynamicObjIndex].Pos, 
					DynamicObjIndex,
					0.16f,//0.4//0.3
					0.001f//0.01
				);
		}
	}

	/*void CollCheckPlayerGrounded()
	{
		int PlayerCellPos = DynamicObjCellPos [0];
		for(int n = 0; n < GameObjectsCreation.EntityCells[PlayerCellPos].StaticIndecesCursor; n++)
		{
			Vector3 PosBuff = GameObjectsCreation.StaticColls
				[GameObjectsCreation.EntityCells[PlayerCellPos].CellStaticIndeces[n]]
				;

			float DotPlayerUpGround = 
				Vector3.Dot (DynamicEntities[0].DynamicObject.transform.up, DynamicEntities[0].Pos -
					PosBuff);

			if(
				//n != 0 && //not colliding iwth ourself //n != m &&
				Vector3.Distance
				(
					DynamicEntities[0].Pos,//n
					PosBuff
				)
				< 10f
				&& 
				DotPlayerUpGround > 0.8f
			)
			{
				IsPlayerGrounded =  true;
			}
		}

	}*/

	/*void EndOfRopeCollision()
	{

		if(
			//n != 0 && //not colliding iwth ourself //n != m &&
			Vector3.Distance
			(
				GameObjectsCreation.Player.transform.position,//n
				GameObjectsCreation.MagicOrb.transform.position

			)
			> 16f
		)
		{
			GameObjectsCreation.RopeCollMove += 
				CollAtomizeVector 
				(
					GameObjectsCreation.Player.transform.position, 
					GameObjectsCreation.MagicOrb.transform.position, 
					1,
					0.03f,//0.016//03
					0.09f//0.016//03
				);	
		}
	}*/

	void CollisionHandling(int DynamicObjIndex)
	{
		//Check 27 cells
		//
		// * * * - * * * - * * *
		// * * * - * * * - * * *
		// * * * - * * * - * * *
		//

		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex
			,DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, 0, 0)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + 1
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, 0, 1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - 1
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, 0, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, 0, 0)
		);
		CollCheckCell
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, 0, 0)
		);

		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 + 1
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, 0, 1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 - 1
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, 0, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 - 1
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, 0, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 + 1
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, 0, 1)
		);



		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, 1, 0)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + 1 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, 1, 1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - 1 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, 1, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, 1, 0)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, 1, 0)
		);

		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 + 1 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, 1, 1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 - 1 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, 1, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 - 1 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, 1, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 + 1 + ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, 1, 1)
		);



		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, -1, 0)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + 1 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, -1, 1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - 1 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (0, -1, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, -1, 0)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, -1, 0)
		);

		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 + 1 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, -1, 1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 - 1 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, -1, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex + ProceduralMapPhysics.WorldCubeMap.Pitch2 - 1 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (1, -1, -1)
		);
		CollCheckCell 
		(
			DynamicEntities[DynamicObjIndex].WorldIndex - ProceduralMapPhysics.WorldCubeMap.Pitch2 + 1 - ProceduralMapPhysics.WorldCubeMap.Pitch
			, DynamicObjIndex
			//,DynamicEntities[DynamicObjIndex].Pos + new Vector3 (-1, -1, 1)
		);
		//!!!!!!!
		//!!!!!!
		//
		//

	}

	void ApplyMovement(int DynamicObjIndex)
	{

		if (NumOfColls > 0) 
		{
			CollMove /= NumOfColls;
		}
		NumOfColls = 0;

		PlayerMove ();
		//NOTE: Gravity
		if(DynamicEntities[DynamicObjIndex].Velocity.y > -2f)
		{
			DynamicEntities[DynamicObjIndex].Velocity += new Vector3(0, -0.01f, 0);//1
		}

		//Jelenleg az iscollidingnak nincs értelme...
		//talán később ha így lehet összeragadni? de lehet akkor se kell bool
		//if (GameObjectsCreation.IsColliding [DynamicObjIndex]) 
		{
			DynamicEntities[DynamicObjIndex].Velocity += 
				CollMove;

			DynamicEntities[DynamicObjIndex].Pos +=
				DynamicEntities[DynamicObjIndex].Velocity;

			DynamicEntities[DynamicObjIndex].Velocity *= 
				DynamicEntities[DynamicObjIndex].Friction
				;//0.4f
			CollMove *=
				0.00f
				;//0.02f
		} 

		DynamicEntities[DynamicObjIndex].IsColliding = false;
	}

	void GetTransforms()
	{
		for (int n = 0; n < DynamicEntityCount; n++) 
		{
			DynamicEntities[n].Pos = 
				DynamicEntities[n].DynamicObject.transform.position;
		}
	}

	void SetTransforms()
	{
		for (int n = 0; n < DynamicEntityCount; n++) 
		{
			DynamicEntities[n].DynamicObject.transform.position = DynamicEntities[n].Pos;
		}

	}

	void MoveDrawCubeToPlayer()
	{
		if
			(
				DynamicEntities[0].Pos.z - ProceduralMapPhysics.DrawCubeMap.PosZ > 7
			)
		{
			ProceduralMapPhysics.ControlDrawCubeForward ();
		}

		if
			(
				DynamicEntities[0].Pos.z - ProceduralMapPhysics.DrawCubeMap.PosZ < 5
			)
		{
			ProceduralMapPhysics.ControlDrawCubeBack ();
		}

		if
			(
				DynamicEntities[0].Pos.x - ProceduralMapPhysics.DrawCubeMap.PosX > 7
			)
		{
			ProceduralMapPhysics.ControlDrawCubeRight ();
		}

		if
			(
				DynamicEntities[0].Pos.x - ProceduralMapPhysics.DrawCubeMap.PosX < 5
			)
		{
			ProceduralMapPhysics.ControlDrawCubeLeft ();
		}

		if
			(
				DynamicEntities[0].Pos.y - ProceduralMapPhysics.DrawCubeMap.PosY > 7
			)
		{
			ProceduralMapPhysics.ControlDrawCubeUp ();
		}

		if
			(
				DynamicEntities[0].Pos.y - ProceduralMapPhysics.DrawCubeMap.PosY < 5
			)
		{
			ProceduralMapPhysics.ControlDrawCubeDown ();
		}

	}
	/**********************************************************/

	void FixedUpdate()
	{
		//NOTE: Not necessary only once in start
		//GetTransforms ();
		/***************************/

		//PlayerMove ();
		MouseInput ();

		for(int n = 0; n < DynamicEntityCount; n++)
		{
			GetWorldPos (n);
			//SetWorldPos (n);

			CollisionHandling (n);

			/*if(n == 1)
				{
					EndOfRopeCollision ();

					CollMove +=RopeCollMove;

					RopeCollMove *= 0;
				}*/

			//CollCheckPlayerGrounded ();
			ApplyMovement (n);
			IsPlayerGrounded = false;
		}			
		/***************************/
		//AnimsColl.Idle();
		//AnimsColl.GetVertexGroups ();
		/***************************/
		SetTransforms ();

		MoveDrawCubeToPlayer ();

	}
	/***********************************************************************************************START_FUNCTIONS*/
	/*void InitializeAnimBodyParts()
	{
		AnimsColl.RightShoulder = GameObject.Find ("RightShoulder");
		AnimsColl.RightUpperArm = GameObject.Find ("RightUpperArm");
		AnimsColl.RightLowerArm = GameObject.Find ("RightLowerArm");

		AnimsColl.LeftShoulder = GameObject.Find ("LeftShoulder");
		AnimsColl.LeftUpperArm = GameObject.Find ("LeftUpperArm");
		AnimsColl.LeftLowerArm = GameObject.Find ("LeftLowerArm");

		AnimsColl.MeshChar = GameObject.Find ("LittleSamuraiMesh").GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}*/

	public GameObject CreateEntity
	(
		string ModelPath,
		Vector3 Position, 
		Vector3 Scale,
		Material ObjMaterial
	)
	{
		GameObject EntityToCreate;
		//
		if (ModelPath == null) 
		{
			EntityToCreate = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			EntityToCreate.transform.localScale = Scale;
			EntityToCreate.GetComponent<MeshRenderer> ().material = ObjMaterial;
			EntityToCreate.transform.position = Position;
		}
		else 
		{
			EntityToCreate = Instantiate 
				(
					Resources.Load 
					(ModelPath) 
					as GameObject 
				);
			EntityToCreate.transform.position = 
				Position;// - new Vector3(0, 5f, 0);
			EntityToCreate.transform.localScale = new Vector3 (1f, 1f, 1f);

			/*GameObject CollDebug;
			CollDebug = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			CollDebug.transform.localScale = Scale;
			CollDebug.GetComponent<MeshRenderer> ().material = ObjMaterial;
			CollDebug.transform.position = Position;
			CollDebug.transform.parent = EntityToCreate.transform;*/
		}

		Destroy (EntityToCreate.GetComponent<SphereCollider>());
		EntityToCreate.transform.rotation = Quaternion.identity;

		return EntityToCreate;

	}

	void InitializeDynamicEntities()
	{
		DynamicEntities = new DynamicEntity[DynamicEntityCount];

		for(int n = 0; n < DynamicEntityCount; n++)
		{
			
			GameObject Obj = 
				CreateEntity
				(
					null,
					new Vector3 (100, 10, 100), 
					new Vector3 (1, 1, 1),
					Resources.Load
					("Graphics/Materials/Player") as Material
				);
			
			DynamicEntities [n] = 
				new DynamicEntity 
				(
					/*Obj,
					0,
					DynamicEntities [n].DynamicObject.transform.position,
					Vector3.zero,
					5,
					false,
					10f,
					0.64f*/
				);
			DynamicEntities [n].DynamicObject = Obj;
			DynamicEntities [n].WorldIndex = 0;
			DynamicEntities [n].Pos = DynamicEntities [n].DynamicObject.transform.position;
			DynamicEntities [n].Velocity = Vector3.zero;
			DynamicEntities [n].DynamicCollSize = 5;
			DynamicEntities [n].IsColliding = false;
			DynamicEntities [n].Mass = 10f;
			DynamicEntities [n].Friction = 0.97f;//64

			//GetWorldPos (n);
		}
		GetTransforms ();
	}

	void Start()
	{		
		DrawCubeScale = 1;
		DynamicEntityCount = 1;

		if (this.gameObject.GetComponent<ProceduralMap> ()) 
		{
			ProceduralMapPhysics = this.gameObject.GetComponent<ProceduralMap> ();
		} 
		else Debug.Log ("No ProceduralMap Attached to main object");

		InitializeDynamicEntities ();

		Camera.main.transform.parent = DynamicEntities[0].DynamicObject.transform;
		Camera.main.transform.position = 
			DynamicEntities[0].DynamicObject.transform.position + new Vector3 (0, 0, -5f);//0,30,-40
		Camera.main.transform.localEulerAngles = new Vector3 (2,0,0);

		//ProceduralMapPhysics.UpdateDrawCube (DynamicEntities [0].Pos);

		VectorRotPlayerMovement = new VectorRot();
		Cursor.lockState = CursorLockMode.Locked;

		//AnimsColl = new Anims ();
		//InitializeAnimBodyParts ();
	}
}
