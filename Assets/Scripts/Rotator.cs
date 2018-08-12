using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public Vector3 Axis;
	public float Speed;
	public bool LocalSpace = true;

	void Update ()
	{
		transform.Rotate(Axis, Speed * Time.deltaTime, LocalSpace ? Space.Self : Space.World);
	}
}
