using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController :  MonoBehaviour, PlayerControls.IPlayerActions
{
	[Header("Settings")]
	[Range(0, 5)]
    public float moveSpeed = 5f;
	[Range(0.5f, 1.5f)]
	public float animMoveSpeedModifier = 1;

	[Range(5, 40)]
	public float turnRate = 25f;

	[Header("Ability Mapping")]
	public UnityEvent onAbility1 = new UnityEvent();
	public UnityEvent onAbility2 = new UnityEvent();

	[Header("Ref")]
	public CharacterController characterController;
	public Animator animator;
	public Transform cam;

    private Vector3 inputValue = Vector3.zero;
	private PlayerControls controls;

	private void OnEnable()
	{
		controls = new PlayerControls();
		controls.Player.SetCallbacks(this);
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		Vector2 input2D = context.ReadValue<Vector2>();
		inputValue.x = input2D.x;
		inputValue.z = input2D.y;
	}

	public void OnAbility1(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			onAbility1.Invoke();
		}
	}

	public void OnAbility2(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			onAbility2.Invoke();
		}
	}

	private void FixedUpdate()
	{
		DoMove();
	}

	//protected override float InitMoveSpeed() => moveSpeed;
	//protected override void OnMoveSpeedChange(float value) => moveSpeed = value;
	//protected override void StopMove() { }
	private void DoMove()
	{
		//Vector3 forward = cam.forward;
		//forward.y = 0;
		//Vector3 move = cam.right * inputValue.x + forward * inputValue.z;
		//characterController.Move(move * moveSpeed * Time.deltaTime);

		if (inputValue.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(inputValue.x, inputValue.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

			animator.SetFloat("MoveSpeed", animMoveSpeedModifier * moveSpeed);
			LookAtDirection(moveDir);
		} else
		{
			animator.SetFloat("MoveSpeed", 0);
		}
	}
	private void LookAtDirection(Vector3 direction)
	{
		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			Quaternion.LookRotation(direction),
			Time.deltaTime * turnRate);
	}
}
