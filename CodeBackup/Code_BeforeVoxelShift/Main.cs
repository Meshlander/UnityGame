using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour 
{
	
	PlayerMovement PlayerMovementMain;
	Collisions CollisionsMain;
	CreateGameObjects CreateGameObjectsMain;

	void Start () 
	{
		CreateGameObjectsMain = this.gameObject.AddComponent<CreateGameObjects> ();
		CollisionsMain = this.gameObject.AddComponent<Collisions> ();
		CollisionsMain.GameObjectsCreation = this.gameObject.GetComponent<CreateGameObjects> ();//CreateGameObjectsMain;
		PlayerMovementMain = this.gameObject.AddComponent<PlayerMovement> ();
		PlayerMovementMain.GameObjectsCreation = this.gameObject.GetComponent<CreateGameObjects> ();//CreateGameObjectsMain;
		//CreateGameObjectsMAin = this.gameObject.AddComponent<CreateGameObjects> ();
	}

	void FixedUpdate ()
	{
		
	}
}
