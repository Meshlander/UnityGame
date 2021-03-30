using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEntity
{
	public GameObject DynamicObject;
	public int WorldIndex;
	public Vector3 Pos;
	public Vector3 Velocity;
	public float DynamicCollSize;
	public bool IsColliding;
	public float Mass;
	public float Friction;

	public DynamicEntity
	(
		GameObject dynamicObject,
		int worldIndex,
		Vector3 pos,
		Vector3 velocity,
		float dynamicCollSize,
		bool isColliding,
		float mass,
		float friction
	)
	{
		DynamicObject = dynamicObject;
		WorldIndex = worldIndex;
		Pos = pos;
		Velocity = velocity;
		DynamicCollSize = dynamicCollSize;
		IsColliding = isColliding;
		Mass = mass;
		Friction = friction;
	}
}
