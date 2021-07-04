using UnityEngine;

public class GhostTrace : MonoBehaviour
{
	public BackwardInTimeAbility ability;
	public Animator animator;

	private void Update()
	{
		if (ability != null)
		{
			TimePointData pointData = ability.tracing.GetPointAtPast(ability.travelBackTime);

			animator.SetFloat("MoveSpeed", pointData.velocity);
			transform.rotation = pointData.rotation;
			transform.position = pointData.position;
		}
	}
}
