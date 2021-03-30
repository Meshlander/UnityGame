using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour 
{
	public CreateGameObjects GameObjectsCreation;
	public Anims AnimsColl;
	//public int EntityArraySize;

	int PlayerCellPos;
	float Velocity;
	float Speed = 0.1f;
	//public bool IsPlayerColliding;

	//In Unity .transform is slow should be called once only every frame
	//PUT THESE IN GAMEOVJECTSCREATION AND ALIGN THEIR SIZE WITH THEO BJECTS
	//Transform[] TransformsBuff;

	//Vector3[] PositionsBuff;
	//Vector3[] StaticCollsPos;
	//Vector3[] DynamicCollsPos;

	public int[] DynamicObjCellPos;
	int[] DynamicObjCellPosWas;

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
		if(GameObjectsCreation.IsColliding [0])
		{
			if(Input.GetKey(KeyCode.W))
			{
				GameObjectsCreation.Velocities[0] 
				+= GameObjectsCreation.Player.transform.forward * Speed;
			}
			if(Input.GetKey(KeyCode.S))
			{
				GameObjectsCreation.Velocities[0]  
				+= -GameObjectsCreation.Player.transform.forward * Speed;
			}
			if(Input.GetKey(KeyCode.A))
			{
				GameObjectsCreation.Velocities[0] 
				+= -GameObjectsCreation.Player.transform.right * Speed;
			}
			if(Input.GetKey(KeyCode.D))
			{
				GameObjectsCreation.Velocities[0]  
				+= GameObjectsCreation.Player.transform.right * Speed;
			}
			//we need a playergrounded condition with dot product
			if (Input.GetKey (KeyCode.Space) && GameObjectsCreation.IsPlayerGrounded) 
			{
				//Debug.Log ("bumm");
				GameObjectsCreation.Velocities[0]  
				+= GameObjectsCreation.Player.transform.up * 10
					+ GameObjectsCreation.Velocities [0] * 2
					//+ GameObjectsCreation.Player.transform.right * Speed;
					;
			}
		}
	}

	void GravityEvent()
	{
		
	}

	void GetDynamicEntityCellPos(int DynamicObjIndex)
	{		

		DynamicObjCellPos[DynamicObjIndex] = 
			//(int)
			(
				(int)(GameObjectsCreation.DynamicColls[DynamicObjIndex].x /
					GameObjectsCreation.CellScale)
				* GameObjectsCreation.EntityCellsPitch2 +
				(int)(GameObjectsCreation.DynamicColls[DynamicObjIndex].y /
					GameObjectsCreation.CellScale)
				* GameObjectsCreation.EntityCellsPitch +
				(int)(GameObjectsCreation.DynamicColls[DynamicObjIndex].z /
					GameObjectsCreation.CellScale)
			);
	}

	void SetDynamicEntityCellPos(int DynamicObjIndex)
	{

		GetDynamicEntityCellPos (DynamicObjIndex);

		if(DynamicObjCellPos[DynamicObjIndex] != DynamicObjCellPosWas[DynamicObjIndex])
		{
			int DynamicIndecesCursorBuff = 
				GameObjectsCreation.EntityCells [DynamicObjCellPosWas [DynamicObjIndex]].DynamicIndecesCursor;
			
			GameObjectsCreation.EntityCells
			[DynamicObjCellPosWas[DynamicObjIndex]].DynamicIndecesCursor -= 1;

			GameObjectsCreation.EntityCells
			[DynamicObjCellPos[DynamicObjIndex]].CellDynamicIndeces[DynamicIndecesCursorBuff] =
				DynamicObjIndex
				;
			//!!
			GameObjectsCreation.EntityCells
			[DynamicObjCellPos[DynamicObjIndex]].DynamicIndecesCursor += 1;
			//Debug.Log ("Cell Is " + GameObjectsCreation.EntityCells
				//[DynamicObjCellPos[DynamicObjIndex]].DynamicIndecesCursor);
			//Debug.Log ("CellPosIs " + DynamicObjCellPos[DynamicObjIndex]);
			//Debug.Log ("CellPosWas " + DynamicObjCellPosWas[DynamicObjIndex]);
		}

		DynamicObjCellPosWas[DynamicObjIndex] = DynamicObjCellPos[DynamicObjIndex];
	}

	Vector3 CollAtomizeVector
	(
		Vector3 CollObjLoc, 
		Vector3 MovObjLoc, 
		int VelocityIndex,
		float CollCompDamp,
		float MovCompDamp
	)
	{
		//!!!
		//NumOfColls++;

		Vector3 ObjLocDiff = CollObjLoc - MovObjLoc;
		Vector3 CollComponent; //= 
			//CollObjLoc - GameObjectsCreation.Player.transform.position;
		CollComponent = Vector3.Normalize (ObjLocDiff);
		GameObjectsCreation.CollNormal = CollComponent;
		//I need more of this.. this is bounciness
		//DAMPEN
		CollComponent *= CollCompDamp;//0.4//0.2//0.3
		Vector3 MovComponent = GameObjectsCreation.Velocities [VelocityIndex];

		//DAMPEN:
		//this is the true dampener needs to be lower the less velocity we lose the more we roll
		MovComponent *= MovCompDamp;//0.2//0.1//0.01//Them ove component can be our actual velocity?

		//dont violate preservation of force
		// YOU NEED THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		GameObjectsCreation.Velocities [VelocityIndex] *= 1 - MovCompDamp;

		//!!!
		//GameObjectsCreation.Velocities [VelocityIndex] *= 1 - CollCompDamp;
		//!!!

		Vector3 Result = 
			//2 makes it bounce, 1 makes it slide
			MovComponent - 2 * //2 makes it bounce, 1 makes it slide
			Vector3.Dot 
			(
				MovComponent,
				CollComponent
			) 
			* CollComponent
			;
		//correct sinkage into collider
		Result -= CollComponent //* 0.02f;
			* (10.0f - Vector3.Magnitude(ObjLocDiff));//to prevent sinking//10.1

		return Result;
	}

	void CollCheckCellDynamic
	(
		int CellIndexRelToPlayer, 
		int DynamicObjIndex
	)
	{
		for(int n = 0; n < GameObjectsCreation.EntityCells[CellIndexRelToPlayer].DynamicIndecesCursor; n++)
		{
			/*Vector3 PosBuff = GameObjectsCreation.DynamicObjects
				[GameObjectsCreation.EntityCells[CellIndexRelToPlayer].CellDynamicIndeces[n]]
				.transform.position;*/

			Vector3 PosBuff = GameObjectsCreation.DynamicColls
				[GameObjectsCreation.EntityCells[CellIndexRelToPlayer].CellDynamicIndeces[n]]
				;

			//CollCheckCellDynamic ();//!!!!!!!!

			if(
				//n != 0 && //not colliding iwth ourself //n != m &&
				Vector3.Distance
				(
					/*GameObjectsCreation.DynamicObjects[DynamicObjIndex].transform.position,//n
					PosBuff*/

					GameObjectsCreation.DynamicColls[DynamicObjIndex],//n
					PosBuff
				)
				< 10f
			)
			{
				//Debug.Log ("Colliding");
				GameObjectsCreation.IsColliding [DynamicObjIndex] =  true;

				//GameObjectsCreation.CollVelocities [DynamicObjIndex] =
				GameObjectsCreation.CollMove += 
					CollAtomizeVector 
					(
						PosBuff, 
						//GameObjectsCreation.DynamicObjects[DynamicObjIndex].transform.position, 
						GameObjectsCreation.DynamicColls[DynamicObjIndex], 
						DynamicObjIndex,
						0.3f,
						0.01f
					);				
			}
		}
	}

	void CollCheckCell
	(
		int CellIndexRelToPlayer, 
		int DynamicObjIndex
	)
	{
		for(int n = 0; n < GameObjectsCreation.EntityCells[CellIndexRelToPlayer].StaticIndecesCursor; n++)
		{
			/*Vector3 PosBuff = GameObjectsCreation.StaticObjects
				[GameObjectsCreation.EntityCells[CellIndexRelToPlayer].CellStaticIndeces[n]]
				.transform.position;*/
			Vector3 PosBuff = GameObjectsCreation.StaticColls
				[GameObjectsCreation.EntityCells[CellIndexRelToPlayer].CellStaticIndeces[n]]
				;

			//CollCheckCellDynamic ();//!!!!!!!!
			//yea, somehow this distance chekc should be done only once not thrice now
			//well not quite it hss to be done many times

			if(
				//n != 0 && //not colliding iwth ourself //n != m &&
				Vector3.Distance
				(
					//GameObjectsCreation.DynamicObjects[DynamicObjIndex].transform.position,//n
					GameObjectsCreation.DynamicColls[DynamicObjIndex],//n
					PosBuff
				)
				< 10f
			)
			{
				//Debug.Log ("Colliding");
				GameObjectsCreation.IsColliding [DynamicObjIndex] =  true;

				//GameObjectsCreation.Velocities [DynamicObjIndex] =
				//GameObjectsCreation.CollVelocities [DynamicObjIndex] =
				GameObjectsCreation.CollMove += 
					CollAtomizeVector 
					(
						PosBuff, 
						//GameObjectsCreation.DynamicObjects[DynamicObjIndex].transform.position, 
						GameObjectsCreation.DynamicColls[DynamicObjIndex], 
						DynamicObjIndex,
						0.3f,//0.4//0.3
						0.01f//0.01
					);
			}
		}
	}

	void CollCheckPlayerGrounded()
	{
		int PlayerCellPos = DynamicObjCellPos [0];
		for(int n = 0; n < GameObjectsCreation.EntityCells[PlayerCellPos].StaticIndecesCursor; n++)
		{
			/*Vector3 PosBuff = GameObjectsCreation.StaticObjects
				[GameObjectsCreation.EntityCells[PlayerCellPos].CellStaticIndeces[n]]
				.transform.position;*/
			Vector3 PosBuff = GameObjectsCreation.StaticColls
				[GameObjectsCreation.EntityCells[PlayerCellPos].CellStaticIndeces[n]]
				;

			/*float DotPlayerUpGround = 
				Vector3.Dot (GameObjectsCreation.Player.transform.up, GameObjectsCreation.DynamicObjects [0].transform.position -
				PosBuff);*/

			float DotPlayerUpGround = 
				Vector3.Dot (GameObjectsCreation.Player.transform.up, GameObjectsCreation.DynamicColls [0] -
					PosBuff);

			//CollCheckCellDynamic ();//!!!!!!!!

			if(
				//n != 0 && //not colliding iwth ourself //n != m &&
				Vector3.Distance
				(
					/*GameObjectsCreation.DynamicObjects[0].transform.position,//n
					PosBuff*/

					GameObjectsCreation.DynamicColls[0],//n
					PosBuff
				)
				< 10f
				&& 
				DotPlayerUpGround > 0.8f
			)
			{
				//Debug.Log ("grounded");
				GameObjectsCreation.IsPlayerGrounded =  true;
			}
		}

	}

	void CollisionHandling(int DynamicObjIndex)
	{
		//AssignEntityToCell (PLAYER!!!);
		///
		/// You need ot take all the dynamic objexcts which are in the vicinity of the player
		/// and do these calculations with them too !!!Dynamic - static coll!!!
		/// then you also need to do a a !!! dynamic - dynamic !!! coll
		///
		SetDynamicEntityCellPos(DynamicObjIndex);
			
		//Check 27 cells
		//
		// * * * - * * * - * * *
		// * * * - * * * - * * *
		// * * * - * * * - * * *
		//
		//
		//

		CollCheckCell (DynamicObjCellPos[DynamicObjIndex], DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] + 1, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] - 1, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] + GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] - GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] + 1 + GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] + 1 - GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] - 1 + GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] - 1 - GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);



		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] + GameObjectsCreation.EntityCellsPitch, DynamicObjIndex);
		CollCheckCell (DynamicObjCellPos[DynamicObjIndex] - GameObjectsCreation.EntityCellsPitch, DynamicObjIndex);

		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex], DynamicObjIndex);
		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex] + 1, DynamicObjIndex);
		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex] - 1, DynamicObjIndex);
		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex] + GameObjectsCreation.EntityCellsPitch, DynamicObjIndex);
		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex] - GameObjectsCreation.EntityCellsPitch, DynamicObjIndex);
		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex] + GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);
		CollCheckCellDynamic (DynamicObjCellPos[DynamicObjIndex] - GameObjectsCreation.EntityCellsPitch2, DynamicObjIndex);

	}

	void EndOfRopeCollision()
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
	}

	void ApplyMovement(int DynamicObjIndex)
	{

		//Velocity = GameObjectsCreation.Velocities [DynamicObjIndex].magnitude;		

		PlayerMove ();
		//Gravity
		if(GameObjectsCreation.Velocities [DynamicObjIndex].y > -2f)
		{
			GameObjectsCreation.Velocities [DynamicObjIndex] += new Vector3(0, -1f, 0);
		}

		//Jelenleg az iscollidingnak nincs értelme...
		//talán később ha így lehet összeragadni? de lehet akkor se kell bool
		//if (GameObjectsCreation.IsColliding [DynamicObjIndex]) 
		{
			//GameObjectsCreation.CollMove /= NumOfColls;
			//Debug.Log(NumOfColls);
			//NumOfColls = 0;

			GameObjectsCreation.Velocities [DynamicObjIndex] += 
				GameObjectsCreation.CollMove;

			/*if(GameObjectsCreation.Velocities [DynamicObjIndex].magnitude > 0.01f)
			{
				GameObjectsCreation.DynamicColls[DynamicObjIndex] +=
					GameObjectsCreation.Velocities [DynamicObjIndex];
			}*/
			GameObjectsCreation.DynamicColls[DynamicObjIndex] +=
				GameObjectsCreation.Velocities [DynamicObjIndex];

			GameObjectsCreation.Velocities [DynamicObjIndex] *= 
				//0
				GameObjectsCreation.Friction[DynamicObjIndex]
				;//0.4f
			GameObjectsCreation.CollMove *=
				//GameObjectsCreation.Friction[DynamicObjIndex]
				0.00f
				;//0.02f
		} 
			
		GameObjectsCreation.IsColliding [DynamicObjIndex] = false;
	}

	void GetTransforms()
	{
		for (int n = 0; n < GameObjectsCreation.DynamicEntitiesSize; n++) 
		{
			GameObjectsCreation.DynamicColls [n]= 
				GameObjectsCreation.DynamicObjects [n].transform.position;
		}
	}

	void SetTransforms()
	{
		//has to be done only once for static
		//GameObjectsCreation.StaticObjects [0].transform.position = GameObjectsCreation.StaticColls [0];
		//then we move only the colliders

		for (int n = 0; n < GameObjectsCreation.DynamicEntitiesSize; n++) 
		{
			GameObjectsCreation.DynamicObjects [n].transform.position = GameObjectsCreation.DynamicColls [n];
		}

	}

	void FixedUpdate()
	{
		//not necessary only once in start
		//GetTransforms ();
		/***************************/
		///
		/// A playerl esz a központ, és a dynamic objectecet is csak akkor kezeljük, ha a környező cellákban benne vannak
		/// ide jön a dynamic indeces cursor stb
		///
		for(int n = 0; n < 4; n++)
		{
			//!!!
			//if(n != 1)
			{

				CollisionHandling (n);

				if(n == 1)
				{
					EndOfRopeCollision ();

					GameObjectsCreation.CollMove += GameObjectsCreation.RopeCollMove;

					GameObjectsCreation.RopeCollMove *= 0;
				}

				CollCheckPlayerGrounded ();
				ApplyMovement (n);
				GameObjectsCreation.IsPlayerGrounded = false;
			}
		}			
		/***************************/
		AnimsColl.Idle();
		AnimsColl.GetVertexGroups ();
		/***************************/
		SetTransforms ();

	}

	void InitializeDynamicEntitiesInCells()//you need separate init and asign coz of ispo-wasposw
	{

		for(int n = 0; n < 3 /*GameObjectsCreation.DynamicEntitiesSize*/; n++)
		{
			GetDynamicEntityCellPos(n);//This will be rather set
			//Debug.Log (DynamicObjCellPos[n]);
			GameObjectsCreation.EntityCells
			[DynamicObjCellPos[n]].CellDynamicIndeces
			[GameObjectsCreation.EntityCells[DynamicObjCellPos[n]].DynamicIndecesCursor] = n;
			GameObjectsCreation.EntityCells[DynamicObjCellPos[n]].DynamicIndecesCursor += 1;

			// CELLPOS IS NOT DERIVED CORRECTLY USING THE FuNCTiON
			//DynamicObjCellPos = GetDynamicEntityCellPos(n);
			Debug.Log(DynamicObjCellPos[n]);
			DynamicObjCellPosWas[n] = DynamicObjCellPos[n];
			Debug.Log(DynamicObjCellPosWas[n]);

		}
	}

	void InitializeAnimBodyParts()
	{
		AnimsColl.RightShoulder = GameObject.Find ("RightShoulder");
		AnimsColl.RightUpperArm = GameObject.Find ("RightUpperArm");
		AnimsColl.RightLowerArm = GameObject.Find ("RightLowerArm");

		AnimsColl.LeftShoulder = GameObject.Find ("LeftShoulder");
		AnimsColl.LeftUpperArm = GameObject.Find ("LeftUpperArm");
		AnimsColl.LeftLowerArm = GameObject.Find ("LeftLowerArm");

		AnimsColl.MeshChar = GameObject.Find ("LittleSamuraiMesh").GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}
		
	void Start()
	{
		GameObjectsCreation = this.gameObject.GetComponent<CreateGameObjects> ();
		AnimsColl = new Anims ();
		//TransformsBuff = new Transform[64];
		//PositionsBuff = new Vector3[64];
		DynamicObjCellPos = new int[GameObjectsCreation.DynamicEntitiesSize];
		DynamicObjCellPosWas = new int[GameObjectsCreation.DynamicEntitiesSize]; 

		InitializeDynamicEntitiesInCells ();
		InitializeAnimBodyParts ();

		//Test();

	}

	/*unsafe void Test()
	{
		int  var = 20;   // actual variable declaration 
		int  *ip;        // pointer variable declaration 

		ip = &var;  // store address of var in pointer variable

		//float *X;
		//X = &GameObjectsCreation.Player.transform.position.x;

		fixed (float* X = &GameObjectsCreation.Player.transform.position.x){}

		///
		/// MAke a custom feature to unity where to DRAW the models and dont evne bother 
		/// with the gameobject transform
		/// Try to draw models without gameobjects altogether........
		///

		//check how to use unity ocmponents
	}*/
}
