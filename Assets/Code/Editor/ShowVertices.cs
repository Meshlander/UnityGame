using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShowVertices : MonoBehaviour
{
    public GameObject TestRoom;
    public Mesh TestRoomMesh;
    // Use this for initialization
    private bool IsInitialized;

    private void CreateNumberFromFile(string Name, Vector3 Pos)
    {
        GameObject Sprite;
        Sprite = new GameObject();
        Sprite.name = Name + "Sprite";

        Sprite.transform.position = Pos;
        Sprite.transform.rotation = Camera.main.transform.rotation;

        SpriteRenderer SpriteRend = Sprite.AddComponent<SpriteRenderer>();
        SpriteRend.sprite = Resources.Load<Sprite>("MSalphabet/MSA" + Name) as Sprite;
    }

    private void DrawVertsInNormalsDir()
    {
        //TestRoomMesh = this.gameObject.GetComponent<MeshFilter>().sharedMesh;
        for (int a = 0; a < TestRoomMesh.vertices.Length; a++)
        {
            Vector3 VToDraw =
                (
                  /*TestRoom.transform.rotation **/
                  new Vector3
                  (
                      TestRoomMesh.vertices[a].x * TestRoom.transform.localScale.x,
                      TestRoomMesh.vertices[a].y * TestRoom.transform.localScale.y,
                      TestRoomMesh.vertices[a].z * TestRoom.transform.localScale.z
                  )                 
                ) + TestRoom.transform.position;			


            Debug.DrawRay(VToDraw,
                /*Quaternion.Euler(-90, 0, 0) **/ /*TestRoom.transform.rotation **/
                (TestRoomMesh.normals[a] * 1.5f)
                , Color.red, 0.0f);



            if (!IsInitialized)
            {
                //CreateNumberFromFile(a.ToString(), VToDraw);
            }
            //Text Text1 = TestRoom.AddComponent<Text>();
            //Text1.text = a.ToString();
        }
        IsInitialized = true;
    }

    void Start ()
    {
        TestRoom = this.gameObject;
        TestRoomMesh = this.gameObject.GetComponent<MeshFilter>().sharedMesh;
    }
	
	// Update is called once per frame
	void Update ()
    {
        DrawVertsInNormalsDir ();

		/*SceneView.currentDrawingSceneView.camera.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) * 
			Matrix4x4.Translate(Vector3.forward * -99999 / 2f) * 
			Camera.main.worldToCameraMatrix;*/
    }
}
