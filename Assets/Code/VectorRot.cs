using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorRot //: MonoBehaviour 
{

	float WantedRadius = 5;

	public Vector3 RotCam
	(
		Vector3 PivotPos,
		Vector3 ObjPos,
		Vector3 Axis,
		float MouseY
	)
	{
		
		Vector3 TangentialForce;

		Vector3 RadiusIn = PivotPos - ObjPos;
		//Debug.Log (RadiusIn.magnitude);
		Vector3 RadiusInNormal = Vector3.Normalize (RadiusIn);
		float RadiusDiff = RadiusIn.magnitude - WantedRadius;

		TangentialForce = Vector3.Cross (Axis, RadiusInNormal) * MouseY;
		TangentialForce += RadiusInNormal * RadiusDiff;

		return TangentialForce;

	}

}
