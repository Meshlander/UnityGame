using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity
{
	public Vector3 EntityCollider;
	public Vector3 EntityVelocity;
	public GameObject EntityModel;
	public Animation EntityAnimation;
	public bool IsColliding;

	public GameEntity(Vector3 entityCollider, Vector3 entityVelocity, GameObject entityModel, Animation entityAnimation)
	{
		EntityCollider = entityCollider;
		EntityModel = entityModel;
		EntityAnimation = entityAnimation;
		EntityVelocity = entityVelocity;

	}
}
