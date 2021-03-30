using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this code there is a WorldCube: 
/// a byte array sliced up virtually using an int value (pitch), into 3 dimensions
/// A DrawCube has a GlobalPosition, and its own pitch value, forming a smaller cube
/// inside of the WorldCube.
/// In the FixedUpdate function there is a function that refreshes the Perimeters
/// of the DrawCube so that it travels the WorldCube and can access the information
/// in the byte array. 
/// The perimeters of the DrawCube are used for refreshing a Mesh.
/// </summary>

public class MeshQuad
{
	public Vector3[] Verts;
	public Vector3[] Normals;
	public Vector2[] UVs;
}

public class MeshCube
{
	public Vector3 Pos;
	public int WorldIndexPos;
	public int DrawIndexPos;

	public MeshQuad A;//front
	public MeshQuad B;//right
	public MeshQuad C;//back
	public MeshQuad D;//left
	public MeshQuad E;//bottom
	public MeshQuad F;//top
}

public class WorldCube
{
	public int Pitch;
	public int Pitch2;
	public int Pitch3;

	public byte[] WorldIndeces;
}

public class DrawCube
{
	public MeshCube[] Meshcubes;

	public int Pitch;
	public int Pitch2;
	public int Pitch3;

	public int[] WorldIndexValAtDrawIndex;

	public int PosX;
	public int PosY;
	public int PosZ;
}

public class DrawCubeCursor
{
	public int XPlaneToDo;
	public int YPlaneToDo;
	public int ZPlaneToDo;	
}

public class ProceduralMesh
{
	public GameObject MapObject;
	public Mesh MapMesh;
	public MeshRenderer MapMeshRenderer;
	public MeshFilter MapMeshFilter;
}

public class ProceduralMeshCursor
{
	public Vector3[] VertBuff;
	public Vector3[] NormBuff;
	public Vector2[] UVBuff;
	public int[] TriBuff;
	public int VertCursor;
	public int TriCursor;
}

public class ProceduralMap : MonoBehaviour 
{
	WorldCube WorldCubeMap;

	DrawCube DrawCubeMap;
	DrawCubeCursor DrawCubeCursorMap;

	ProceduralMesh ProceduralMeshMap;
	ProceduralMeshCursor ProceduralMeshCursorMap;

	/*****************************************************************************START_FUNCTIONS*/
	void CreateProceduralMesh()
	{
		ProceduralMeshMap = new ProceduralMesh ();
		ProceduralMeshMap.MapObject = this.gameObject;
		ProceduralMeshMap.MapMeshRenderer = this.gameObject.AddComponent<MeshRenderer>();
		ProceduralMeshMap.MapMeshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
		ProceduralMeshMap.MapMeshFilter = this.gameObject.AddComponent<MeshFilter>();
		ProceduralMeshMap.MapMesh = new Mesh();

		//There is 24 vertices / 1 Index
		int NumOfVerts = DrawCubeMap.Pitch3 * 24;
		int NumOfTris = NumOfVerts + NumOfVerts / 2;

		ProceduralMeshMap.MapMesh.vertices = new Vector3[NumOfVerts];
		ProceduralMeshMap.MapMesh.triangles = new int[NumOfTris];
		ProceduralMeshMap.MapMesh.normals = new Vector3[NumOfVerts];
		ProceduralMeshMap.MapMesh.uv = new Vector2[NumOfVerts];
		ProceduralMeshMap.MapMeshFilter.mesh = ProceduralMeshMap.MapMesh;

		ProceduralMeshCursorMap = new ProceduralMeshCursor ();
		ProceduralMeshCursorMap.VertBuff = new Vector3[NumOfVerts];
		ProceduralMeshCursorMap.TriBuff = new int[NumOfTris];
		ProceduralMeshCursorMap.NormBuff = new Vector3[NumOfVerts];
		ProceduralMeshCursorMap.UVBuff = new Vector2[NumOfVerts];
		ProceduralMeshCursorMap.VertCursor = 0;
		ProceduralMeshCursorMap.TriCursor = 0;
	}

	// D         C
	//
	//      x---->Normal
	//
	// AxPos     B
	MeshQuad CreateQuad
	(
		Vector3 Pos, 
		Vector3 Normal,
		Vector3 BDir,
		//Vector3 CDir,
		Vector3 DDir

	)//returns quad for cube
	{
		MeshQuad QuadOut = new MeshQuad();

		Vector3 A = Pos + Normal;
		Vector3 B = Pos + Normal + BDir; //+ new Vector3(1 , 0, 0);
		Vector3 C = Pos + Normal + (BDir + DDir);//CDir; //+ new Vector3(1 , 1, 0);
		Vector3 D = Pos + Normal + DDir; //+ new Vector3(0 , 1, 0);

		QuadOut.Verts = new Vector3[4];
		QuadOut.Normals = new Vector3[4];
		QuadOut.UVs = new Vector2[4];

		QuadOut.Verts [0] = A;
		QuadOut.Verts [1] = B;
		QuadOut.Verts [2] = C;
		QuadOut.Verts [3] = D;

		ProceduralMeshCursorMap.VertBuff [ProceduralMeshCursorMap.VertCursor] = A;
		ProceduralMeshCursorMap.VertBuff [ProceduralMeshCursorMap.VertCursor + 1] = B;
		ProceduralMeshCursorMap.VertBuff [ProceduralMeshCursorMap.VertCursor + 2] = C;
		ProceduralMeshCursorMap.VertBuff [ProceduralMeshCursorMap.VertCursor + 3] = D;

		ProceduralMeshCursorMap.TriBuff [ProceduralMeshCursorMap.TriCursor] = ProceduralMeshCursorMap.VertCursor;
		ProceduralMeshCursorMap.TriBuff [ProceduralMeshCursorMap.TriCursor + 1] = ProceduralMeshCursorMap.VertCursor + 3;
		ProceduralMeshCursorMap.TriBuff [ProceduralMeshCursorMap.TriCursor + 2] = ProceduralMeshCursorMap.VertCursor + 1;
		ProceduralMeshCursorMap.TriBuff [ProceduralMeshCursorMap.TriCursor + 3] = ProceduralMeshCursorMap.VertCursor + 3;
		ProceduralMeshCursorMap.TriBuff [ProceduralMeshCursorMap.TriCursor + 4] = ProceduralMeshCursorMap.VertCursor + 2;
		ProceduralMeshCursorMap.TriBuff [ProceduralMeshCursorMap.TriCursor + 5] = ProceduralMeshCursorMap.VertCursor + 1;

		//here smoothing comes into question vs hard edges
		QuadOut.Normals [0] = Normal;
		QuadOut.Normals [1] = Normal;
		QuadOut.Normals [2] = Normal;
		QuadOut.Normals [3] = Normal;

		ProceduralMeshCursorMap.NormBuff [ProceduralMeshCursorMap.VertCursor] = Normal;
		ProceduralMeshCursorMap.NormBuff [ProceduralMeshCursorMap.VertCursor + 1] = Normal;
		ProceduralMeshCursorMap.NormBuff [ProceduralMeshCursorMap.VertCursor + 2] = Normal;
		ProceduralMeshCursorMap.NormBuff [ProceduralMeshCursorMap.VertCursor + 3] = Normal;

		QuadOut.UVs [0] = new Vector2(0, 0);
		QuadOut.UVs [1] = new Vector2(1, 0);
		QuadOut.UVs [2] = new Vector2(1, 1);
		QuadOut.UVs [3] = new Vector2(0, 1);

		ProceduralMeshCursorMap.UVBuff [ProceduralMeshCursorMap.VertCursor] = new Vector2(0, 0);
		ProceduralMeshCursorMap.UVBuff [ProceduralMeshCursorMap.VertCursor + 1] = new Vector2(1, 0);
		ProceduralMeshCursorMap.UVBuff [ProceduralMeshCursorMap.VertCursor + 2] = new Vector2(1, 1);
		ProceduralMeshCursorMap.UVBuff [ProceduralMeshCursorMap.VertCursor + 3] = new Vector2(0, 1);

		ProceduralMeshCursorMap.VertCursor += 4;
		ProceduralMeshCursorMap.TriCursor += 6;

		return QuadOut;
	}

	// x    Quad x
	//      |
	// Quad-xPos-Quad + 2 quads -> FrontLeftVertexIsPos
	//      |
	// xPos Quad x
	MeshCube CreateCube(Vector3 Pos)
	{
		MeshCube MeshCubeOut = new MeshCube();
		MeshCubeOut.Pos = Pos;

		MeshCubeOut.A = CreateQuad 
			(
				Pos, 
				-Vector3.forward,
				Vector3.right,
				Vector3.up 
			);

		MeshCubeOut.B = CreateQuad 
			(
				Pos - Vector3.forward, //You need a -Z offset coz you have to wind the quad forward
				Vector3.right,
				Vector3.forward,
				Vector3.up 
			);

		MeshCubeOut.C = CreateQuad 
			(
				Pos - Vector3.forward + Vector3.right,
				Vector3.forward,
				- Vector3.right,
				Vector3.up 
			);

		MeshCubeOut.D = CreateQuad 
			(
				Pos + Vector3.right, 
				-Vector3.right,
				-Vector3.forward,
				Vector3.up 
			);

		MeshCubeOut.E = CreateQuad 
			(
				Pos + Vector3.up,
				-Vector3.up,
				Vector3.right,
				-Vector3.forward
			);

		MeshCubeOut.F = CreateQuad 
			(
				Pos, 
				Vector3.up - Vector3.forward,
				Vector3.right,
				Vector3.forward
			);

		return MeshCubeOut;
	}

	void CreateWorldCube()
	{
		WorldCubeMap = new WorldCube();
		WorldCubeMap.Pitch = 200;
		WorldCubeMap.Pitch2 = WorldCubeMap.Pitch * WorldCubeMap.Pitch;
		WorldCubeMap.Pitch3 = WorldCubeMap.Pitch2 * WorldCubeMap.Pitch;

		WorldCubeMap.WorldIndeces = new byte[WorldCubeMap.Pitch3];

		int Pitch = 0;
		for(int X = 0; X < WorldCubeMap.Pitch; X++)
		{
			for(int Y = 0; Y < WorldCubeMap.Pitch; Y++)
			{
				for(int Z = 0; Z < WorldCubeMap.Pitch; Z++)
				{
					if (Y < 2) 
					{
						WorldCubeMap.WorldIndeces [Z + Pitch] = 1;
					}
					else 
					{
						WorldCubeMap.WorldIndeces [Z + Pitch] = 0;
					}
					if (X == 20 && Z == 10 /*&& Y == 20*/) 
					{
						WorldCubeMap.WorldIndeces [Z + Pitch] = 1;
						DebugDrawWorldPos(Z + Pitch, 1000);
					}
					////DebugDrawWorldPos (Z + Pitch);
				}
				Pitch += WorldCubeMap.Pitch;
			}
		}
		//test columns

	}

	void CreateDrawCube()
	{
		DrawCubeMap = new DrawCube();
		DrawCubeMap.Pitch = 12;
		DrawCubeMap.Pitch2 = DrawCubeMap.Pitch * DrawCubeMap.Pitch;
		DrawCubeMap.Pitch3 = DrawCubeMap.Pitch2 * DrawCubeMap.Pitch;

		//DrawCubeMap.WorldIndexValAtDrawIndex = new byte[DrawCubeMap.Pitch3];

		/*for(int n = 0; n < DrawCubeMap.Pitch3; n++)
		{
			DrawCubeMap.WorldIndexValAtDrawIndex [n] = WorldCubeMap.WorldIndeces [n];
		}*/

		DrawCubeMap.PosX = 0;//DrawCubeMap.Pitch;
		DrawCubeMap.PosY = 0;
		DrawCubeMap.PosZ = 0;
	}

	void CreateDrawCubeCursor()
	{
		DrawCubeCursorMap = new DrawCubeCursor ();
		DrawCubeCursorMap.XPlaneToDo = 0;
		DrawCubeCursorMap.YPlaneToDo = 0;
		DrawCubeCursorMap.ZPlaneToDo = 0;
	}

	void CreateDrawCubeMeshCubes()
	{
		DrawCubeMap.Meshcubes = new MeshCube[DrawCubeMap.Pitch3];

		DrawCubeMap.WorldIndexValAtDrawIndex = new int[DrawCubeMap.Pitch3];

		int Pitch = 0;
		int WorldPitch = 0;
		for(int X = 0; X < DrawCubeMap.Pitch; X++)
		{
			for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
			{
				for(int Z = 0; Z < DrawCubeMap.Pitch; Z++)
				{
					DrawCubeMap.Meshcubes [Z + Pitch] = CreateCube(new Vector3(X,Y,Z));
					//DebugDrawWorldPos (Z + Pitch);
					DrawCubeMap.WorldIndexValAtDrawIndex[Z + Pitch] = 
						X * WorldCubeMap.Pitch2
						+ Y * WorldCubeMap.Pitch
						+ Z
						;
					//Debug.Log (Z + Pitch);
					//Debug.Log (Z + WorldPitch);
				}
				Pitch += DrawCubeMap.Pitch;
				WorldPitch += WorldCubeMap.Pitch;
			}
		}

	}

	void AssembleProceduralMesh()
	{
		ProceduralMeshMap.MapMesh.vertices = ProceduralMeshCursorMap.VertBuff;
		ProceduralMeshMap.MapMesh.triangles = ProceduralMeshCursorMap.TriBuff;
		ProceduralMeshMap.MapMesh.normals = ProceduralMeshCursorMap.NormBuff;
		ProceduralMeshMap.MapMesh.uv = ProceduralMeshCursorMap.UVBuff;
		ProceduralMeshMap.MapMeshFilter.mesh = ProceduralMeshMap.MapMesh;
	}

	/*********************************/
	void Start () 
	{
		Camera.main.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) * 
			Matrix4x4.Translate(Vector3.forward * -99999 / 2f) * 
			Camera.main.worldToCameraMatrix;

		if(Camera.current != null)
		{
			Camera.current.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) * 
				Matrix4x4.Translate(Vector3.forward * -99999 / 2f) * 
				Camera.main.worldToCameraMatrix;
		}

		CreateWorldCube ();
		CreateDrawCube ();
		CreateProceduralMesh ();
		CreateDrawCubeCursor ();
		CreateDrawCubeMeshCubes ();
		AssembleProceduralMesh ();
		//
		//UpdateDrawCubePerimeter ();
		//
	}

	/************************************************************************************UPDATED_FUNCTIONS*/
	Vector3 CalcDrawPos(int Index)
	{
		Vector3 DrawPos;
		float IndexRPitch2 = Index % DrawCubeMap.Pitch2;
		DrawPos.x = Index / DrawCubeMap.Pitch2;
		DrawPos.y = IndexRPitch2 / DrawCubeMap.Pitch;
		DrawPos.z = IndexRPitch2 % DrawCubeMap.Pitch;
		return DrawPos;
	}

	void DebugDrawPos(int Index, float DurationOfRay)
	{
		Vector3 DrawPos;
		float IndexRPitch2 = Index % DrawCubeMap.Pitch2;
		DrawPos.x = Index / DrawCubeMap.Pitch2;
		DrawPos.y = IndexRPitch2 / DrawCubeMap.Pitch;
		DrawPos.z = IndexRPitch2 % DrawCubeMap.Pitch;
		Debug.DrawRay(DrawPos, Vector3.up * 0.5f, Color.green, DurationOfRay);
	}
		
	Vector3 CalcWorldPos(int Index)
	{
		Vector3 WorldPos;
		float IndexRPitch2 = Index % WorldCubeMap.Pitch2;
		WorldPos.x = (int)(Index / WorldCubeMap.Pitch2);
		WorldPos.y = (int)(IndexRPitch2 / WorldCubeMap.Pitch);
		WorldPos.z = (int)(IndexRPitch2 % WorldCubeMap.Pitch);
		return WorldPos;
	}

	void DebugDrawWorldPos(int Index, float DurationOfRay)
	{
		Vector3 WorldPos;
		float IndexRPitch2 = Index % WorldCubeMap.Pitch2;
		WorldPos.x = Index / WorldCubeMap.Pitch2;
		WorldPos.y = IndexRPitch2 / WorldCubeMap.Pitch;
		WorldPos.z = IndexRPitch2 % WorldCubeMap.Pitch;
		Debug.DrawRay(WorldPos, Vector3.up * 0.5f, Color.cyan, DurationOfRay);
	}

	/*void CalcDrawIndexToWorldIndex(int DrawIndex)//drawcube pos plays a role too
	{
		
	}*/
	void MovQuadVerts
	(
		Vector3 Pos, 
		Vector3 Normal,
		Vector3 BDir,
		Vector3 DDir,
		int Index
	)
	{
		Vector3 A = Pos + Normal;
		Vector3 B = Pos + Normal + BDir; //+ new Vector3(1 , 0, 0);
		Vector3 C = Pos + Normal + (BDir + DDir);//CDir; //+ new Vector3(1 , 1, 0);
		Vector3 D = Pos + Normal + DDir; //+ new Vector3(0 , 1, 0);

		ProceduralMeshCursorMap.VertBuff [Index] = A;
		ProceduralMeshCursorMap.VertBuff [Index + 1] = B;
		ProceduralMeshCursorMap.VertBuff [Index + 2] = C;
		ProceduralMeshCursorMap.VertBuff [Index + 3] = D;
	}

	void MovCubeVerts(int Index)
	{
		int IndexToDo = Index * 24;

		MovQuadVerts 
		(
			DrawCubeMap.Meshcubes[Index].Pos, 
			-Vector3.forward,
			Vector3.right,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;
	
		MovQuadVerts 
		(
			DrawCubeMap.Meshcubes[Index].Pos - Vector3.forward, 
			Vector3.right,
			Vector3.forward,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;

		MovQuadVerts 
		(
			DrawCubeMap.Meshcubes[Index].Pos - Vector3.forward + Vector3.right, 
			Vector3.forward,
			- Vector3.right,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;

		MovQuadVerts 
		(
			DrawCubeMap.Meshcubes[Index].Pos + Vector3.right, 
			-Vector3.right,
			-Vector3.forward,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;	

		MovQuadVerts 
		(
			DrawCubeMap.Meshcubes[Index].Pos + Vector3.up, 
			-Vector3.up,
			Vector3.right,
			-Vector3.forward,
			IndexToDo
		);
		IndexToDo += 4;	

		MovQuadVerts 
		(
			DrawCubeMap.Meshcubes[Index].Pos, 
			Vector3.up - Vector3.forward,
			Vector3.right,
			Vector3.forward,
			IndexToDo
		);
		IndexToDo += 4;	
		
	}

	void MoveCubeVertsToNull(int Index)
	{
		int IndexToDo = Index * 24;

		MovQuadVerts 
		(
			Vector3.zero, 
			-Vector3.forward,
			Vector3.right,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;

		MovQuadVerts 
		(
			Vector3.zero, 
			Vector3.right,
			Vector3.forward,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;

		MovQuadVerts 
		(
			Vector3.zero, 
			Vector3.forward,
			- Vector3.right,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;

		MovQuadVerts 
		(
			Vector3.zero, 
			-Vector3.right,
			-Vector3.forward,
			Vector3.up, 
			IndexToDo
		);
		IndexToDo += 4;	

		MovQuadVerts 
		(
			Vector3.zero, 
			-Vector3.up,
			Vector3.right,
			-Vector3.forward,
			IndexToDo
		);
		IndexToDo += 4;	

		MovQuadVerts 
		(
			Vector3.zero, 
			Vector3.up - Vector3.forward,
			Vector3.right,
			Vector3.forward,
			IndexToDo
		);
		IndexToDo += 4;	

	}

	void RotateNormalsToCamofUnseenQuads()
	{
		
	}

	void RefreshProceduralMeshVerts()
	{
		ProceduralMeshMap.MapMesh.vertices = ProceduralMeshCursorMap.VertBuff;
	}

	// IndexToDrawFrom:
	// Forward: o Pitch <--- Way to swipe through a perimeter on the drawcube
	//          |            without leaving out an index.
	//          |  
	//          o- - -o Pitch2 -> x = Start (CursorZ * 1); x < Start + Pitch3 - Pitch2; x += Pitch2
	//                         -> Start += Pitch
	//
	//                        Pitch = 4 -> 0 16 32 48 - 4 20 36 52 
	// Rigth: Pitch, 1
	// Up: Pitch2, 1 (1, 16)

	//IndexToDrawTo
	//
	// 16 -> 20 !!In World Terms -> worldpicth not 4!! + WorldPosZ * 1                             on Z ->
	// We want to make the index jump perpendicular to the plane -> !16! -> 17 - 18 - 19 - 20 -> !21!
	//                                                          

	void UpdateDrawCubePerimeterForward()
	{
		int StartIndex = 0;//This is really Y

		for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
		{
			for(int X = StartIndex; X < StartIndex + DrawCubeMap.Pitch3; X += DrawCubeMap.Pitch2)
			{
				//Where to draw from
				int IndexToDrawFrom = //DrawIndex
					X
					+ DrawCubeCursorMap.ZPlaneToDo // Z DIMENSION *1
					;

				//Where to draw to
				if(IndexToDrawFrom > DrawCubeMap.WorldIndexValAtDrawIndex.Length)
				{
					return;//prevent going out of bounds error
				}
				int WorldIndexToDrawTo =
					DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom]
					+ DrawCubeMap.Pitch
					;

				DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom] 
					+= DrawCubeMap.Pitch
					;

				DrawCubeMap.Meshcubes [IndexToDrawFrom].Pos =
					CalcWorldPos (WorldIndexToDrawTo);

				if (WorldCubeMap.WorldIndeces [WorldIndexToDrawTo] == 1) 
				{
					MovCubeVerts (IndexToDrawFrom);
				} 
				else MoveCubeVertsToNull (IndexToDrawFrom);
			}
			StartIndex += DrawCubeMap.Pitch;//Y dimesnion
		}			
	}

	void UpdateDrawCubePerimeterBack()
	{
		int StartIndex = 0;//This is really Y -> 1 coz Z DIMENSION - 1

		for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
		{
			for(int X = StartIndex; X < StartIndex + DrawCubeMap.Pitch3; X += DrawCubeMap.Pitch2)
			{
				//Where to draw from
				int IndexToDrawFrom = //DrawIndex
					X
					+ DrawCubeCursorMap.ZPlaneToDo - 1 // Z DIMENSION *1
					;

				//Debug.Log (DrawCubeCursorMap.ZPlaneToDo);

				if(IndexToDrawFrom > DrawCubeMap.WorldIndexValAtDrawIndex.Length)
				{
					Debug.Log (DrawCubeCursorMap.ZPlaneToDo);
					return;
				}
				if(IndexToDrawFrom < 0)
				{
					Debug.Log (IndexToDrawFrom);
					Debug.Log (DrawCubeCursorMap.ZPlaneToDo);
					//DrawCubeCursorMap.ZPlaneToDo = 1;
					//return;
				}
				//Where to draw to
				int WorldIndexToDrawTo =
					DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom]
					- DrawCubeMap.Pitch
					;

				DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom] +=
					- DrawCubeMap.Pitch
					;

				DrawCubeMap.Meshcubes [IndexToDrawFrom].Pos =
					CalcWorldPos (WorldIndexToDrawTo);

				/***********************************************************/
				if(WorldCubeMap.WorldIndeces[WorldIndexToDrawTo] == 1)
				{
					MovCubeVerts (IndexToDrawFrom);
				}
				else MoveCubeVertsToNull (IndexToDrawFrom);
				/**********************************************************/
			}
			StartIndex += DrawCubeMap.Pitch;//Y dimesnion
		}			
	}

	void UpdateDrawCubePerimeterRight()//Right and left are somehow not bunnyhopping correctly still they work kinda
	{
		int StartIndex = 0;

		for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
		{
			for(int Z = StartIndex; Z < StartIndex + DrawCubeMap.Pitch3; Z += DrawCubeMap.Pitch)//!!-->Z
			{
				int IndexToDrawFrom = //DrawIndex
					Z
					//+ DrawCubeCursorMap.XPlaneToDo // X DIMENSION -> *PITCH2
					;

				if(IndexToDrawFrom > DrawCubeMap.WorldIndexValAtDrawIndex.Length)
				{
					return;
				}
				int WorldIndexToDrawTo =
					DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom]
					+ WorldCubeMap.Pitch2 //Bunny hop over world X

					;

				DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom] +=
					//
					WorldCubeMap.Pitch2 //Bunny hop over world X

					;

				DrawCubeMap.Meshcubes [IndexToDrawFrom].Pos =
					CalcWorldPos (WorldIndexToDrawTo);

				/**********************************************************/
				if(WorldCubeMap.WorldIndeces[WorldIndexToDrawTo] == 1)
				{
					MovCubeVerts (IndexToDrawFrom);
				}
				else MoveCubeVertsToNull (IndexToDrawFrom);
				/***********************************************************/
			}
			StartIndex += 1;//Z dimesnion
		}
	}

	void UpdateDrawCubePerimeterLeft()
	{
		int StartIndex = 0;

		for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
		{
			for(int Z = StartIndex; Z < StartIndex + DrawCubeMap.Pitch3; Z += DrawCubeMap.Pitch)//!!-->Z
			{
				int IndexToDrawFrom = //DrawIndex
					Z
					//+ DrawCubeCursorMap.XPlaneToDo - DrawCubeMap.Pitch // X DIMENSION -> *PITCH2
					;

				if(IndexToDrawFrom > DrawCubeMap.WorldIndexValAtDrawIndex.Length)
				{
					return;
				}
				int WorldIndexToDrawTo =
					DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom]
					- WorldCubeMap.Pitch2 //Bunny hop over world X

					;

				DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom] +=
					//
					- WorldCubeMap.Pitch2 //Bunny hop over world X

					;

				DrawCubeMap.Meshcubes [IndexToDrawFrom].Pos =
					CalcWorldPos (WorldIndexToDrawTo);
				
				if(WorldCubeMap.WorldIndeces[WorldIndexToDrawTo] == 1)
				{
					MovCubeVerts (IndexToDrawFrom);
				}
				else MoveCubeVertsToNull (IndexToDrawFrom);
			}
			StartIndex += 1;//Z dimesnion
		}
	}

	//^ Z:1
	// \
	//  - - - -
	//   \     \                 ^ Y:Pitch
	//    \     \                |
	//     - - - - -> X:Pitch2    Move
	//
	void UpdateDrawCubePerimeterUp()
	{
		int StartIndex = 0;//Z Dimension

		for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
		{
			for
				(
					int X = StartIndex; 
					X < StartIndex + DrawCubeMap.Pitch3; 
					X += DrawCubeMap.Pitch2
				)//X Dimension
			{
				//Where to draw from
				int IndexToDrawFrom = //DrawIndex
					X
					+ DrawCubeCursorMap.YPlaneToDo //+1 is strange //+ DrawCubeMap.Pitch2; // Y DIMENSION *1
					;

				if(IndexToDrawFrom > DrawCubeMap.WorldIndexValAtDrawIndex.Length)
				{
					return;
				}
				//Where to draw to
				int WorldIndexToDrawTo =
					DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom]
					+ WorldCubeMap.Pitch * DrawCubeMap.Pitch
					;

				DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom] +=
					WorldCubeMap.Pitch * DrawCubeMap.Pitch
					;

				DrawCubeMap.Meshcubes [IndexToDrawFrom].Pos =
					CalcWorldPos (WorldIndexToDrawTo);

				if(WorldCubeMap.WorldIndeces[WorldIndexToDrawTo] == 1)
				{
					MovCubeVerts (IndexToDrawFrom);
				}
				else MoveCubeVertsToNull (IndexToDrawFrom);
			}
			StartIndex += 1;//Y dimesnion //not pitch2
		}			
	}

	void UpdateDrawCubePerimeterDown()
	{
		int StartIndex = 0;//Z Dimension

		for(int Y = 0; Y < DrawCubeMap.Pitch; Y++)
		{
			for
				(
					int X = StartIndex; 
					X < StartIndex + DrawCubeMap.Pitch3; 
					X += DrawCubeMap.Pitch2
				)//X Dimension
			{
				//Where to draw from
				int IndexToDrawFrom = //DrawIndex
					X
					+ DrawCubeCursorMap.YPlaneToDo - DrawCubeMap.Pitch //+1 is strange //+ DrawCubeMap.Pitch2; // Y DIMENSION *1
					;

				if(IndexToDrawFrom > DrawCubeMap.WorldIndexValAtDrawIndex.Length)
				{
					return;
				}
				//Where to draw to

				//We get exception out of range with back and left which is resolved if we move forward
				//or up a little bit
				int WorldIndexToDrawTo =
					DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom]
					- WorldCubeMap.Pitch * DrawCubeMap.Pitch
					;

				DrawCubeMap.WorldIndexValAtDrawIndex [IndexToDrawFrom] +=
					- WorldCubeMap.Pitch * DrawCubeMap.Pitch
					;

				DrawCubeMap.Meshcubes [IndexToDrawFrom].Pos =
					CalcWorldPos (WorldIndexToDrawTo);

				if(WorldCubeMap.WorldIndeces[WorldIndexToDrawTo] == 1)
				{
					MovCubeVerts (IndexToDrawFrom);
				}
				else MoveCubeVertsToNull (IndexToDrawFrom);
			}
			StartIndex += 1;//Y dimesnion //not pitch2
		}			
	}



	void ControlDrawCube()
	{
		if(Input.GetKey(KeyCode.W))
		{
			//DrawCubeMap.PosZ += 1;

			UpdateDrawCubePerimeterForward ();
			DrawCubeCursorMap.ZPlaneToDo += 1;
			if(DrawCubeCursorMap.ZPlaneToDo > DrawCubeMap.Pitch - 1)
			{
				DrawCubeCursorMap.ZPlaneToDo = 0;
				DrawCubeMap.PosZ += DrawCubeMap.Pitch;
			}
		}

		if(Input.GetKey(KeyCode.S))
		{
			//DrawCubeMap.PosZ += 1;

			//UpdateDrawCubePerimeterBack ();
			//DrawCubeCursorMap.ZPlaneToDo -= 1;
			if(DrawCubeCursorMap.ZPlaneToDo < 1)
			{
				DrawCubeCursorMap.ZPlaneToDo = DrawCubeMap.Pitch;
				DrawCubeMap.PosZ -= DrawCubeMap.Pitch;
			}
			UpdateDrawCubePerimeterBack ();
			DrawCubeCursorMap.ZPlaneToDo -= 1;
		}

		if(Input.GetKey(KeyCode.D))
		{
			UpdateDrawCubePerimeterRight ();
			DrawCubeCursorMap.XPlaneToDo += DrawCubeMap.Pitch;
			if(DrawCubeCursorMap.XPlaneToDo > DrawCubeMap.Pitch - 1)
			{
				DrawCubeCursorMap.XPlaneToDo = 0;
				DrawCubeMap.PosX += DrawCubeMap.Pitch;
			}
		}

		if(Input.GetKey(KeyCode.A))
		{
			UpdateDrawCubePerimeterLeft ();
			DrawCubeCursorMap.XPlaneToDo -= DrawCubeMap.Pitch;
			if(DrawCubeCursorMap.XPlaneToDo < DrawCubeMap.Pitch + 1)
			{
				DrawCubeCursorMap.XPlaneToDo = DrawCubeMap.Pitch;
				DrawCubeMap.PosX -= DrawCubeMap.Pitch;
			}
		}

		if(Input.GetKey(KeyCode.Space))
		{
			UpdateDrawCubePerimeterUp ();
			DrawCubeCursorMap.YPlaneToDo += DrawCubeMap.Pitch;
			if(DrawCubeCursorMap.YPlaneToDo > DrawCubeMap.Pitch2 - DrawCubeMap.Pitch)
			{
				DrawCubeCursorMap.YPlaneToDo = 0;
				DrawCubeMap.PosY += DrawCubeMap.Pitch;
			}
		}

		if(Input.GetKey(KeyCode.C))
		{
			//UpdateDrawCubePerimeterDown ();
			//DrawCubeCursorMap.YPlaneToDo -= DrawCubeMap.Pitch;
			if(DrawCubeCursorMap.YPlaneToDo < DrawCubeMap.Pitch)
			{
				DrawCubeCursorMap.YPlaneToDo = DrawCubeMap.Pitch2;
				DrawCubeMap.PosY -= DrawCubeMap.Pitch;
			}
			UpdateDrawCubePerimeterDown ();
			DrawCubeCursorMap.YPlaneToDo -= DrawCubeMap.Pitch;
		}
	}
	/*************************************************/
	void DebugCamBounds()
	{
		Camera.current.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) * 
			Matrix4x4.Translate(Vector3.forward * -99999 / 2f) * 
			Camera.main.worldToCameraMatrix;
		Camera.current.nearClipPlane = 0.01f;
	}
	void FixedUpdate ()
	{
		//DebugCamBounds ();

		ControlDrawCube ();
		RefreshProceduralMeshVerts ();
	}
	/************************************************/
}
