using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCell
{
	public Vector3 CellPos;
	public int[] CellStaticIndeces;
	public int[] CellDynamicIndeces;
	public int StaticIndecesCursor;
	public int DynamicIndecesCursor;
	/*public GameObject[] DynamicObjects;//ilyenekt tároljon egy cella
	public Animation[] Animations;
	public Vector3[] Velocities;
	public Vector3[] DynamicColls;
	public GameObject[] StaticObjects;
	public Vector3[] StaticColls;*/

	///
	/// elég inteket tároni h hanyadik elemek vannnak itt benn
	///

	/*public EntityCell
	(
		GameObject[] dynamicObjects,
		Animation[] animations,
		Vector3[] velocities,
		Vector3[] dynamicColls,
		GameObject[] staticObjects,
		Vector3[] staticColls
	)
	{
		DynamicObjects = dynamicObjects;
		Animations = animations;
		Velocities = velocities;
		DynamicColls = dynamicColls;
		StaticObjects = staticObjects;
		StaticColls = staticColls;
	}*/
}
