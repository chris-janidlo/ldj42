using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
	public Vector2 FrequencyRange;
	public float Amplitude, MaxStartingPhase;

	float phase, yCenter;

	void Start ()
	{
		phase = Random.value * MaxStartingPhase;
		yCenter = transform.position.y;
	}

	void Update ()
	{
		float frequency = Random.Range(FrequencyRange.x, FrequencyRange.y);
		phase += (Time.deltaTime * frequency);

		transform.position = new Vector3
		(
			transform.position.x,
			yCenter + Mathf.Sin(phase) * Amplitude,
			transform.position.z
		);
	}
}
