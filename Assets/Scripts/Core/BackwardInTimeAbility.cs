using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardInTimeAbility : MonoBehaviour
{
	public Tracing tracing;
	public float travelBackTime = 5;
	public TrailRenderer trail;

	public void TravelBackward()
	{
		transform.position = tracing.GetPointAtPast(travelBackTime).position;
		tracing.ResetTrace();
		trail.Clear();
	}
}
