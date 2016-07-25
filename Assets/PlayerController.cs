using UnityEngine;
using System.Collections;

public class PlayerController : CustomizedMonoBehavior
{
	public float SpinHSpeed = 50.0f;
	public float MaxForwardSpeed = 10.0f;
	public float MaxTranslationSpeed = 3.0f;
	public float Acceleration = 12000.0f;

	private World world;
	private float forwardSpeed = 0.0f;
	private float translationSpeed = 0.0f;

	void Start ()
	{
		world = World.Instance;
	}

	private void updateForwardSpeed()
	{
		if (Input.GetAxisRaw("Booster") > 0) {
			forwardSpeed += Acceleration * Time.deltaTime;
		}
		forwardSpeed -= world.AirResistance * forwardSpeed * Time.deltaTime;
		forwardSpeed = Mathf.Min(MaxForwardSpeed, forwardSpeed);
		forwardSpeed = (forwardSpeed < world.StopThreshold) ? 0.0f : forwardSpeed;
	}

	private void updateTranslationSpeed()
	{
		translationSpeed = Input.GetAxisRaw("Move Translation") * MaxTranslationSpeed;
		if (translationSpeed > 0.0f) {
			translationSpeed -= world.AirResistance * translationSpeed * Time.deltaTime;
			translationSpeed = Mathf.Min(MaxTranslationSpeed, translationSpeed);
			translationSpeed = (translationSpeed < world.StopThreshold) ? 0.0f : translationSpeed;
		}
		else if (translationSpeed < 0.0f) {
			translationSpeed -= world.AirResistance * translationSpeed * Time.deltaTime;
			translationSpeed = Mathf.Max(-MaxTranslationSpeed, translationSpeed);
			translationSpeed = (translationSpeed > -world.StopThreshold) ? 0.0f : translationSpeed;
		}
	}

	private void updateTransform()
	{
		float spinHAngle = Input.GetAxisRaw("Camera Horizontal") * Time.deltaTime * SpinHSpeed;
		transform.Rotate(0, spinHAngle, 0);
		transform.Translate(translationSpeed, 0, forwardSpeed * Time.deltaTime);
	}

	void Update()
	{
		updateForwardSpeed();
		updateTranslationSpeed();
		updateTransform();
	}
}
