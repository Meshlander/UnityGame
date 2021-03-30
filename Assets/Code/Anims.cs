using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anims //: MonoBehaviour 
{

	int AnimPhaseTimer = 0;
	byte AnimPhase = 0;

	public GameObject RightShoulder;
	public GameObject RightUpperArm;
	public GameObject RightLowerArm;


	public GameObject LeftShoulder;
	public GameObject LeftUpperArm;
	public GameObject LeftLowerArm;

	public Mesh MeshChar;

	public void Idle()
	{
		if(AnimPhase == 0)
		{
			AnimPhase = 1;
		}

		if (AnimPhase == 1) 
		{
			RightShoulder.transform.position -= RightShoulder.transform.right * 0.01f;
			LeftShoulder.transform.position += LeftShoulder.transform.right * 0.01f;

			AnimPhaseTimer += 1;
			if(AnimPhaseTimer == 40)
			{
				AnimPhase = 2;
				AnimPhaseTimer = 0;
			}
		} 
		else if (AnimPhase == 2) 
		{
			RightShoulder.transform.position += RightShoulder.transform.right * 0.01f;
			LeftShoulder.transform.position -= LeftShoulder.transform.right * 0.01f;

			AnimPhaseTimer += 1;
			if(AnimPhaseTimer == 40)
			{
				AnimPhase = 1;
				AnimPhaseTimer = 0;
			}
		} 
		/*else if (AnimPhase == 2) 
		{
			
		} 
		else if (AnimPhase == 3) 
		{
			
		}
		else if(AnimPhase == 4)
		{
			
		}*/

	}

	public void GetVertexGroups()
	{
		/*Vector3[] VerticesBuff = new Vector3[MeshChar.vertices.Length];
		for(int n = 0; n < MeshChar.vertices.Length; n++)
		{
			VerticesBuff [n] = MeshChar.vertices [n];
		}*/
		Vector3[] VerticesBuff = MeshChar.vertices;
		VerticesBuff [0] = new Vector3 (0.3f, 0.3f, 0.3f);
		MeshChar.vertices = VerticesBuff;

	}

	void Start () 
	{

	}

	void FixedUpdate () 
	{
		
	}
}
