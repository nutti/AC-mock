using UnityEngine;
using System.Collections;

public class PlayerController : CustomizedMonoBehavior
{
	public float SpinHSpeed = 50.0f;
	public float MaxForwardSpeed = 5.0f;

	private World world;
	private float forwardSpeed = 0.0f;

	void Start ()
	{
		world = World.Instance;
	}

	private void updateForwardSpeed()
	{
		if (Input.GetAxisRaw("Booster") > 0) {
			forwardSpeed += Acceleration * Time.deltaTime;
		}
		forwardSpeed -= world.AirResistance * forwardSpeed;
		forwardSpeed = (MaxForwardSpeed < forwardSpeed) ? MaxForwardSpeed : forwardSpeed;
		forwardSpeed = (forwardSpeed < world.StopThreshold) ? 0.0f : forwardSpeed;
	}

	private void updateTransform()
	{
		float spinHAngle = Input.GetAxisRaw("Horizontal") * Time.deltaTime * SpinHSpeed;
		float transDir = 0.0f;
		if (Input.GetAxisRaw("Translation Left") > 0.0f) {
			transDir = 1.0f;
		}
		else if (Input.GetAxisRaw("Translation Right") > 0.0f) {
			transDir = -1.0f;
		}
		float transSpeed = transDir * Time.deltaTime * 100.0f;
		transform.Rotate(0, spinHAngle, 0);
		transform.Translate(transSpeed, 0, speed * Time.deltaTime);
	}

	void Update()
	{
		updateForwardSpeed();
		updateTransform();
	}
}
