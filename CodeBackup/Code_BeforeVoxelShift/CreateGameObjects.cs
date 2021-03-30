using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameObjects : MonoBehaviour 
{
	//CreateMap CreateMapCreateGameObjects;
	//THESE SOHULD REMAIN ... myplayershould be special
	public GameObject Player;
	public Animation PlayerAnimation;
	public bool IsPlayerGrounded;

	public GameObject MagicOrb;
	/*****************************************************************/
	/// <summary>
	/// THIS PART WILL BEEE THE GAME ENTITY......
	/// a lot of these will be moving to collision or some place they are used THE MOST
	/// </summary>
	public GameObject[] DynamicObjects;//ilyenekt tároljon egy cella
	public Animation[] Animations;

	public Vector3[] Velocities;
	public Vector3[] CollVelocities;
	// When there is a circle collider resting inbetween two other circle colliders,
	// The gravitational force is distributed so that the force is divided into two
	// equal parts pointing towards each other on the perpendicular dimension. 
	// The sum of these forces is the original force, if there is no dampening force.

	//           ||
	//           ||
	//       \-> \/ <-/

	public Vector3[] DynamicColls;
	public float[] DynamicCollsSizes;

	public bool[] IsColliding;
	public float[] Mass;
	//NOTE: This is not really friction it is how much we wanna slow something, and for example
	// the rope we wanna slow a little bit more coz we also wanna apply more of the bouncy and move 
	//bounce forces on it
	public float[] Friction;
	/*******************************************************************/
	public GameObject[] StaticObjects;
	public Vector3[] StaticColls;
	public float[] StaticCollsSizes;

	public EntityCell[] EntityCells;
	public int EntitiesCursor;

	public int StaticEntitiesSize = 1600;//32000;//400
	public int StaticEntitiesPitch = 40;//TRY TO USE THIS IN MESH CREATION
	public int DynamicEntitiesSize = 64;

	public int EntityCellsSize = 262144;
	public int EntityCellsPitch = 64;//ezeket majd ki tudjuk számolni
	public int EntityCellsPitch2 = 4096;
	public int EntityCellsPitch3 = 262144;
	public int CellScale = 40;
	/****************************************************************/
	public Vector3 CollMove;
	public Vector3 RopeCollMove;
	public Vector3 CollNormal;

	public int PlayersSize = 1;//17;
	public int MagicOrbSize = 1;
	public int MapElementsSize = 40;
	public int BulletsSize = 32;

	void OnGUI()
	{
		//GUI.Label(new Rect(50,110,200,30), "Velocity: " + Velocity[0].ToString());
		//GUI.Label(new Rect(50,90,200,30), EntityCells[0].StaticIndecesCursor.ToString());
		//GUI.Label(new Rect(50,70,200,30), EntityCells[0].CellStaticIndeces[6].ToString());
		//GUI.Label(new Rect(50,90,200,30), EntityCells[0].StaticIndecesCursor.ToString());

	}		

	int GetStaticEntityCellPos(int n)//GetCellPos
	{
		int CellPos = 
			//(int)
			(
				(int)(StaticObjects[n].transform.position.x /
					CellScale)
				* EntityCellsPitch2 +
				(int)(StaticObjects[n].transform.position.y /
					CellScale)
				* EntityCellsPitch +
				(int)(StaticObjects[n].transform.position.z /
					CellScale)
			);
		return CellPos;
	}

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
			EntityToCreate.transform.localScale = new Vector3 (2.5f,2.5f,2.5f) * 10;

			/*GameObject CollDebug;
			CollDebug = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			CollDebug.transform.localScale = Scale;
			CollDebug.GetComponent<MeshRenderer> ().material = ObjMaterial;
			CollDebug.transform.position = Position;
			CollDebug.transform.parent = EntityToCreate.transform;*/
		}

		Destroy (EntityToCreate.GetComponent<SphereCollider>());
		EntityToCreate.transform.rotation = Quaternion.identity;

		if(EntityToCreate.GetComponent<Animator>() != null)
		{
			Destroy(EntityToCreate.GetComponent<Animator> ());

			if(EntityToCreate.GetComponent<Animation>() != null)
			{
				PlayerAnimation = EntityToCreate.GetComponent<Animation> ();
				PlayerAnimation.enabled = true;
			}

			//Animations[EntitiesCursor] = GameObjects[EntitiesCursor].GetComponent<Animation> ();
			//Animations[EntitiesCursor].enabled = true;
		}
		return EntityToCreate;

	}

	void CreateStaticEntities()
	{
		StaticObjects = new GameObject[StaticEntitiesSize];
		StaticColls = new Vector3[StaticEntitiesSize];
		StaticCollsSizes = new float[StaticEntitiesSize];

		int StaticEntitiesPitch = 40;
	
		Vector3[] PlatformCollsPos = new Vector3[StaticEntitiesSize];
		//This being a Draw array does not need that amount of size
		//it just has te be wound correctly


		//int PlatformIndex = 0;
		for(int n = 0; n < StaticEntitiesPitch; n++)//nm -> xy
		{
			for(int m = 0; m < StaticEntitiesPitch; m++)
			{
				int Index = n * StaticEntitiesPitch + m;
				StaticObjects[Index] = CreateEntity
				(
					null,
					new Vector3
					(n*3.6f, -5, m*3.6f)+ new Vector3(100,100,100),
					new Vector3(10,10,10),
					Resources.Load
					("Graphics/Materials/Player") as Material
				);	
				//setting up static colls
				StaticCollsSizes [Index] = 5;
				StaticColls [Index] = StaticObjects[Index].transform.position;

				///
				/// TODO: Here generate MAP
				///
				PlatformCollsPos [Index] = StaticColls [Index]; 
				//PlatformIndex += 1;

				//Destroy (StaticObjects[Index].GetComponent<MeshRenderer>());

				//assignstaticentitytocell
				int CellToAdd = GetStaticEntityCellPos(Index);
				//Debug.Log (CellToAdd);
				EntityCells
				[CellToAdd].CellStaticIndeces
				[EntityCells[CellToAdd].StaticIndecesCursor] = Index;
				EntityCells[CellToAdd].StaticIndecesCursor += 1;
				//
			}
		}
		StaticObjects [400].transform.position += new Vector3 (0,5,0);
		StaticObjects [20].transform.position += new Vector3 (0,5,0);
		StaticObjects [600].transform.position += new Vector3 (0,5,0);

		for(int m = 0; m < StaticEntitiesPitch * StaticEntitiesPitch; m ++)
		{
			//this should be moved into the for loop too maybe to align them better
			StaticColls [m] = StaticObjects[m].transform.position;
			PlatformCollsPos [m] = StaticColls [m]; 
		}
	}

	void CreateDynamicEntities()
	{
		DynamicObjects = new GameObject[DynamicEntitiesSize];
		Animations = new Animation[DynamicEntitiesSize];
		Velocities = new Vector3[DynamicEntitiesSize];
		DynamicColls = new Vector3[DynamicEntitiesSize];
		DynamicCollsSizes = new float[DynamicEntitiesSize];
		IsColliding =  new bool[DynamicEntitiesSize];
		//IsGrounded =  new bool[DynamicEntitiesSize];
		Friction = new float[DynamicEntitiesSize];
		Mass =  new float[DynamicEntitiesSize];
		for(int n = 0; n < DynamicEntitiesSize; n++)
		{
			DynamicObjects[n] = new GameObject();
			Animations[n] = new Animation();
			Velocities[n] = new Vector3();
			DynamicColls[n] = new Vector3();
			DynamicCollsSizes [n] = 5;
			IsColliding [n] = false;
			//IsGrounded [n] = false;
			Friction[n] = 0.96f;//56
			Mass [n] = 1;
		}

		/**************************************************************/
		Player = CreateEntity
		(
			//"Graphics/Models/FreeAnimatedSpaceMan/Prefab/space_man_model",
			"Graphics/Models/Character",
				new Vector3(80,15,80) + new Vector3(100,100,100),//5//10,x,10
			new Vector3(10,10,10),
			Resources.Load("Graphics/Materials/Player") as Material
		);				

		Camera.main.transform.parent = Player.transform;
		Camera.main.transform.position = 
			Player.transform.position + new Vector3 (0, 0, -50);//0,30,-40
		Camera.main.transform.localEulerAngles = new Vector3 (20,0,0);
		/*****************************************************************/
		MagicOrb = CreateEntity
			(
				null,
				new Vector3(100,15,100) + new Vector3(100,100,100),//5
				new Vector3(10,10,10),
				Resources.Load("Graphics/Materials/Player") as Material
			);
		/*****************************************************************/
		DynamicObjects [0] = Player;
		Friction [0] = 0.8f;//64
		DynamicObjects [1] = MagicOrb;
		Friction [1] = 0.98f;//0.9
		Mass [1] = 0.2f;

		for(int n = 2; n < 4; n++)
		{
			DynamicObjects[n] = CreateEntity
				(
					"Graphics/Models/FreeAnimatedSpaceMan/Prefab/space_man_model",
					new Vector3(10 + n * 10,15,10 + n * 10) + new Vector3(100,100,100),//5
					new Vector3(10,10,10),
					Resources.Load("Graphics/Materials/Player") as Material
				);			
		}

		for (int n = 0; n < 4; n++) 
		{
			DynamicColls [n]= 
				DynamicObjects [n].transform.position;
		}
	}

	void CreateEntityCells()
	{
		EntityCells = new EntityCell[EntityCellsSize];

		EntityCellsPitch2 = EntityCellsPitch * EntityCellsPitch;
		EntityCellsPitch3 = EntityCellsPitch2 * EntityCellsPitch;
		EntityCellsSize = EntityCellsPitch3;


		//int Pitch = 0;
		for(int x = 0; x < EntityCellsPitch; x++)//entitycellspitch
		{
			for(int y = 0; y < EntityCellsPitch; y++)
			{
				for(int z = 0; z < EntityCellsPitch; z++)
				{
					EntityCells[x * EntityCellsPitch2 + y * EntityCellsPitch + z] = new EntityCell ();
					EntityCells[x * EntityCellsPitch2 + y * EntityCellsPitch + z].CellPos = 
						new Vector3 (x*CellScale, y*CellScale, z*CellScale);

					EntityCells[x * EntityCellsPitch2 + y * EntityCellsPitch + z].CellStaticIndeces = new int[400];
					EntityCells[x * EntityCellsPitch2 + y * EntityCellsPitch + z].CellDynamicIndeces = new int[16];
					EntityCells [x * EntityCellsPitch2 + y * EntityCellsPitch + z].StaticIndecesCursor = 0;
					EntityCells [x * EntityCellsPitch2 + y * EntityCellsPitch + z].DynamicIndecesCursor = 0;
				}
			}
		}
	}

	public void InitializeGameObjects()
	{		
		//CreateMapCreateGameObjects = this.gameObject.GetComponent<CreateMap> ();
		CreateEntityCells ();
		CreateStaticEntities ();
		CreateDynamicEntities ();
		//CreateMapCreateGameObjects = this.gameObject.GetComponent<CreateMap> ();

	}

}
