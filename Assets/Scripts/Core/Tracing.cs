using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracing : MonoBehaviour
{
	public Transform target;

	public bool recording = true;
	public int maxPoint = 500;

	[SerializeField]
	int currentPointID = 0;

	[SerializeField]
	TimePointData[] points;

	public int PointCount => points.Length;

	private void Awake()
	{
		points = new TimePointData[maxPoint];
	}

	private void FixedUpdate()
	{
		if (recording && target != null)
		{
			SaveTimePointData();
		}
	}

	private void SaveTimePointData()
	{
		int id = currentPointID % PointCount;

		points[id].time = Time.time;
		points[id].position = target.position;
		points[id].rotation = target.rotation;
		points[id].velocity = 1;

		currentPointID += 1;
	}

	public void ResetTrace()
	{
		currentPointID = 0;
		SaveTimePointData();
		for (int i = 1; i < PointCount; i++)
			points[i] = points[0];
	}

	public TimePointData GetPointAtPast(float backTime)
	{
		TimePointData result;
		int nearestPointID = 0;
		float requireTimePoint = Mathf.Clamp(Time.time - backTime, 0, Mathf.Infinity);

		int limit = Mathf.Clamp(currentPointID - PointCount, 0, currentPointID);
		for (int i = currentPointID; i >= limit; i--)
		{
			if (points[i % PointCount].time < requireTimePoint)
			{
				nearestPointID = i;
				break;
			}
		}

		result = points[nearestPointID % PointCount];
		return result;
	}
}
