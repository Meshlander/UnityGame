using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public CreateGameObjects GameObjectsCreation;
	Collisions PlayerMovementCollisions;//TODO:Never used, move to main...


	VectorRot VectorRotPlayerMovement;

	float Speed = 0.4f;
	float Gravity = -9f;
	float GravAnti = -4f;
		

	void Start () 
	{
		//this.gameObject.AddComponent<CreateMap> ();
		//this.gameObject.GetComponent<CreateMap> ().InitializeCreateMap ();
		GameObjectsCreation = this.gameObject.AddComponent<CreateGameObjects>();
		PlayerMovementCollisions = this.gameObject.AddComponent<Collisions>();
		GameObjectsCreation.InitializeGameObjects ();
		//this.gameObject.AddComponent<CPSCamera>();
		VectorRotPlayerMovement = new VectorRot();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void AnimIdle()
	{
		GameObjectsCreation.PlayerAnimation.Play("Idle");
	}

	void AnimRunForward()
	{
		GameObjectsCreation.PlayerAnimation.Play("Run");
	}

	void AnimRunBackward()
	{
		GameObjectsCreation.PlayerAnimation.Play("Run_Back");
	}

	void AnimRunRight()
	{
		GameObjectsCreation.PlayerAnimation.Play("Right");
	}

	void AnimRunLeft()
	{
		GameObjectsCreation.PlayerAnimation.Play("Left");
	}

	void MouseInput()
	{
		float MouseX = Input.GetAxis ("Mouse X");
		GameObjectsCreation.Player.transform.Rotate 
		(GameObjectsCreation.Player.transform.up * 3 * MouseX);

		float MouseY = Input.GetAxis ("Mouse Y");
		//Camera.main.transform.position += Camera.main.transform.up * -MouseY * 2;

		//Camera.main.transform.forward = Vector3.up;

		//Camera.main.transform.LookAt (GameObjectsCreation.Player.transform.position);

		Camera.main.transform.position += VectorRotPlayerMovement.RotCam 
			(
				GameObjectsCreation.Player.transform.position,
				Camera.main.transform.position,
				GameObjectsCreation.Player.transform.right,
				MouseY * 3
			);

		Camera.main.transform.LookAt (GameObjectsCreation.Player.transform.position);

		/*if(MouseX > 0.2f)
		{
			GameObjectsCreation.Player.transform.Rotate 
			(GameObjectsCreation.Player.transform.up * 3 * MouseX);
		}

		if(Input.GetAxis("Mouse X") < -0.2f)
		{
			GameObjectsCreation.Player.transform.Rotate 
			(-GameObjectsCreation.Player.transform.up * 3);
		}*/
	}
	void GameInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			AnimRunForward ();

			GameObjectsCreation.Velocities[0] 
				+= GameObjectsCreation.Player.transform.forward * Speed
				//+ GameObjectsCreation.Player.transform.up * GravAnti

				;

		}
		if(Input.GetKey(KeyCode.S))
		{
			AnimRunBackward ();

			GameObjectsCreation.Velocities[0]  
			+= -GameObjectsCreation.Player.transform.forward * Speed;
		}
		if(Input.GetKey(KeyCode.A))
		{
			AnimRunLeft ();

			GameObjectsCreation.Velocities[0] 
			+= -GameObjectsCreation.Player.transform.right * Speed;
		}
		if(Input.GetKey(KeyCode.D))
		{
			AnimRunRight ();

			GameObjectsCreation.Velocities[0]  
			+= GameObjectsCreation.Player.transform.right * Speed;
		}
		/*if (Input.GetKey (KeyCode.Space) && GameObjectsCreation.IsPlayerColliding) 
		{
			Debug.Log ("bumm");
			GameObjectsCreation.Velocities[0]  
			+= GameObjectsCreation.Player.transform.up * 4;
		}*/
	}

	/*void ApplyMovement()
	{
		
		if (GameObjectsCreation.IsPlayerColliding) {
			GameObjectsCreation.Velocity [0] = 
				GameObjectsCreation.CollMove;
		} 
		else//if(!GameObjectsCreation.IsPlayerColliding) 
		{
			GameObjectsCreation.Velocity [0] += new Vector3(0, -0.1f, 0);
		}

		GameObjectsCreation.Player.transform.localPosition +=
			GameObjectsCreation.Velocity [0];
		GameObjectsCreation.Velocity [0] *= 0.5f;

		GameObjectsCreation.IsPlayerColliding = false;
	}*/

	void Shoot ()
	{
		if (Input.GetKey (KeyCode.Space)) 
		{
		}
	}

	void FixedUpdate ()
	{
		//GameInput ();
		MouseInput ();
		Shoot ();
		//ApplyMovement ();

	}
}
