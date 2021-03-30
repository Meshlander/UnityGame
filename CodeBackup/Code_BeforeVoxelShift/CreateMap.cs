using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour 
{
	/// <summary>
	/// Here we are going to generate the map maybe using chunks as big as cells...
	/// </summary>
	GameObject MapObject;
	Mesh MapMesh;
	MeshRenderer MapMeshRenderer;
	MeshFilter MapMeshFilter;

	public Vector3 MapGenerationCursor;

	public void CreateDrawArray3D
	(
		int WorldPitch, 
		int DrawPitch,
		int DrawPosX,
		int DrawPosY,
		int DrawPosZ
	)
	{
		//store these in memory
		int WorldPitch2 = WorldPitch * WorldPitch;
		int WorldPitch3 = WorldPitch2 * WorldPitch;
		int DrawPitch2 = DrawPitch * DrawPitch;
		int DrawPitch3 = DrawPitch2 * DrawPitch;

		for(int x = 0; x < DrawPitch; x++)
		{
			for(int y = 0; y < DrawPitch; x++)
			{
				for(int z = 0; z < DrawPitch; x++)
				{

				}
			}
		}
	}

	public void CreateDrawArray2D
	(
		
		int WorldPitch, 
		int DrawPitch,
		int DrawPosX,
		int DrawPosY
	)
	{
		//store these in memory
		int WorldPitch2 = WorldPitch * WorldPitch;
		int DrawPitch2 = DrawPitch * DrawPitch;

		int [] WorldArray = new int[16000];
		int [] DrawArray = new int[1600];

		for(int x = 0; x < DrawPitch; x++)
		{
			for(int y = 0; y < DrawPitch; x++)
			{
				//we still have to account for the cursors
				DrawArray [x * DrawPitch + y] = 
					WorldArray
					[
						DrawPosX * WorldPitch
						+ x * WorldPitch
						+ DrawPosY
						+ y
					];
			}
		}
	}

	public void CreatePlatform (Vector3[] CollsPos, int PlatformPitch)
	{
		/*
		 * 000
		 * 100
		 * 010
		 * 110
		 * interesting ABCD pattern there
		*/
		Vector3[] vertices = new Vector3[CollsPos.Length];
		//-Vector3.forward;
		Vector3[] normals = new Vector3[CollsPos.Length];
		/*
		 * new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
			this has to do with how we wind the triangles
		*/
		Vector2[] uv = new Vector2[CollsPos.Length];
		/*
		 * // lower left triangle
		0, 2, 1,
		// upper right triangle
		2, 3, 1
		*/
		int[] tris = new int[(CollsPos.Length + CollsPos.Length/2) * 4];

		//we have to reconstruct the mesh in a for loop representing
		//the for loop we created the plane in
		//or line up the quads in te for loop we create the platform

		for (int n = 0; n < CollsPos.Length; n += 4)
		{
			vertices [n] = CollsPos [n/4];//0
			vertices [n+1] = CollsPos [n/4 + PlatformPitch];//1
			vertices [n+2] = CollsPos [n/4 + 1];//2
			vertices [n+3] = CollsPos [n/4 + 1 + PlatformPitch];//3

			normals [n] = Vector3.up;
			normals [n+1] = Vector3.up;
			normals [n+2] = Vector3.up;
			normals [n+3] = Vector3.up;

			uv [n] = new Vector2(0, 0);
			uv [n+1] = new Vector2(1, 0);
			uv [n+2] = new Vector2(0, 1);
			uv [n+3] = new Vector2(1, 1);
		}

		int DrawCursor = 0;
		//int DrawSize = CollsPos.Length / 4;
		//int DrawPitch = 2;
		int VertCount = 0;

		for (int n = 0; n < PlatformPitch/2; n ++)//putch/2
		{
			for(int m = 0; m < PlatformPitch/2; m ++)
			{

				vertices [VertCount] = CollsPos [DrawCursor];//0
				vertices [VertCount+1] = CollsPos [DrawCursor + PlatformPitch];//1
				vertices [VertCount+2] = CollsPos [DrawCursor + 1];//2
				vertices [VertCount+3] = CollsPos [DrawCursor + 1 + PlatformPitch];//3

				normals [VertCount] = Vector3.up;
				normals [VertCount+1] = Vector3.up;
				normals [VertCount+2] = Vector3.up;
				normals [VertCount+3] = Vector3.up;

				uv [VertCount] = new Vector2(0, 0);
				uv [VertCount+1] = new Vector2(1, 0);
				uv [VertCount+2] = new Vector2(0, 1);
				uv [VertCount+3] = new Vector2(1, 1);

				VertCount += 4;
				DrawCursor += 2;
			}
			DrawCursor += PlatformPitch;//pitch - drawpitch
		}

		//we shouldnt go by 6 we should go by 4
		int TriCount = 0;
		for (int n = 0; n < CollsPos.Length; n += 4)
		{
			
			//021 231
			tris [TriCount] = n;
			tris [TriCount+1] = n + 2;
			tris [TriCount+2] = n + 1;
			//
			tris [TriCount+3] = n + 2;
			tris [TriCount+4] = n + 3;
			tris [TriCount+5] = n + 1;
			//
			TriCount += 6;

			if
			(
				n + 6 < CollsPos.Length 
				&& (n+4) % 80 != 0 // n != 76
			)
			{
				//021 231
				tris [TriCount] = n + 4;
				tris [TriCount+1] = n + 3;
				tris [TriCount+2] = n + 2;
				//
				tris [TriCount+3] = n + 4;
				tris [TriCount + 4] = n + 5;
				tris [TriCount+5] = n + 3;

				TriCount += 6;
			}

			if(n + 4 + PlatformPitch *2 < CollsPos.Length)
			{
				//021 231
				tris [TriCount] = n + 5;
				tris [TriCount+1] = n + 3 + PlatformPitch *2;
				tris [TriCount+2] = n + 3;
				//
				tris [TriCount + 3] = n + 5;// + 3!!!!!!!!!!!!!!!!!!
				tris [TriCount + 4] = n + 4 + PlatformPitch *2;//!!!
				tris [TriCount + 5] = n + 2 + PlatformPitch *2;

				TriCount += 6;
			}
		}

		MapMesh.vertices = vertices;
		MapMesh.triangles = tris;
		MapMesh.normals = normals;
		MapMesh.uv = uv;

		MapMeshFilter.mesh = MapMesh;
	}

	public void CreateWall(Vector3[] CollsPos, Vector3 Normal)
	{
		
	}

	public void InitializeCreateMap()
	{
		MapObject = new GameObject ();
		MapMeshRenderer = gameObject.AddComponent<MeshRenderer>();
		MapMeshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
		MapMeshFilter = gameObject.AddComponent<MeshFilter>();
		MapMesh = new Mesh();
	}

	void Start () 
	{
		/*MapObject = new GameObject ();
		MapMeshRenderer = gameObject.AddComponent<MeshRenderer>();
		MapMeshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
		MapMeshFilter = gameObject.AddComponent<MeshFilter>();
		MapMesh = new Mesh();*/

		/*Vector3[] vertices = new Vector3[4]
		{
			new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(1, 1, 0)
		};
		MapMesh.vertices = vertices;

		int[] tris = new int[6]
		{
			// lower left triangle
			0, 2, 1,
			// upper right triangle
			2, 3, 1
		};
		MapMesh.triangles = tris;

		Vector3[] normals = new Vector3[4]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		MapMesh.normals = normals;

		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
		};
		MapMesh.uv = uv;

		MapMeshFilter.mesh = MapMesh;*/
	}

	void FixedUpdate () 
	{
		
	}
}
