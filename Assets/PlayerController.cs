using UnityEngine;
using System.Collections;

public class PlayerController : CustomizedMonoBehavior
{
	public float SpinHSpeed = 50.0f;
	public float MaxForwardSpeed = 10.0f;
	public float MaxTranslationSpeed = 3.0f;
	public float Acceleration = 100.0f;

	public float QBCoolTime = 0.8f;		// 2s

	private World world;
	private float forwardSpeed = 0.0f;
	private float translationSpeed = 0.0f;
	private float restTimeQB = 0.0f;

	void Start ()
	{
		world = World.Instance;
		restTimeQB = 0.0f;
	}

	private void updateForwardSpeed()
	{
		float dir = Input.GetAxisRaw("Move Forward");
		forwardSpeed += Acceleration * Time.deltaTime * dir / 100.0f;
		if (Input.GetAxisRaw("Booster") > 0) {
			forwardSpeed += Acceleration * Time.deltaTime * dir;
		}
		if (Input.GetAxisRaw("Quick Booster") > 0) {
			if (restTimeQB < 0.0f) {
				forwardSpeed += Acceleration * 100.0f * Time.deltaTime * dir;
				restTimeQB = QBCoolTime;
			}
		}
		if (forwardSpeed > 0.0f) {
			forwardSpeed -= world.AirResistance * forwardSpeed * Time.deltaTime;
			forwardSpeed = (forwardSpeed < world.StopThreshold) ? 0.0f : forwardSpeed;
		}
		else if (forwardSpeed < 0.0f) {
			forwardSpeed -= world.AirResistance * forwardSpeed * Time.deltaTime;
			forwardSpeed = (forwardSpeed > -world.StopThreshold) ? 0.0f : forwardSpeed;
		}
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

	private void updateCoolTime()
	{
		restTimeQB -= Time.deltaTime;
	}

	void Update()
	{
		updateForwardSpeed();
		updateTranslationSpeed();
		updateTransform();
		updateCoolTime();
	}
}
