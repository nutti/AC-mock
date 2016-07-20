using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float SpinHSpeed = 50.0f;
	public float Acceleration = 13.0f;
	public float AirResistance = 0.03f;
	public float MaxSpeed = 5.0f;
	public float StopThreshold = 0.0001f;

	private World world;
	private float speed = 0.0f;

	void Start ()
	{
		world = GameObject.Find("World").GetComponent<World>();
	}

	private void updateSpeed()
	{
		if (Input.GetAxisRaw("Booster") > 0) {
			speed += Acceleration * Time.deltaTime;
		}
		speed -= world.AirResistance * speed;
		speed = (MaxSpeed < speed) ? MaxSpeed : speed;
		speed = (speed < world.StopThreshold) ? 0.0f : speed;
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
		updateSpeed();
		updateTransform();
	}
}
