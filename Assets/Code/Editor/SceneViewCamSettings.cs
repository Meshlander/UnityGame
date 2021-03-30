//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneViewCamSettings : Editor
{

	void OnSceneGUI()
	{
		SceneView.currentDrawingSceneView.camera.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) * 
			Matrix4x4.Translate(Vector3.forward * -99999 / 2f) * 
			Camera.main.worldToCameraMatrix;
	}
	// Use this for initialization
	void Start () 
	{
		SceneView.currentDrawingSceneView.camera.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) * 
			Matrix4x4.Translate(Vector3.forward * -99999 / 2f) * 
			Camera.main.worldToCameraMatrix;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
