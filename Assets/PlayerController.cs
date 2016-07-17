using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static float PI = 3.1415926535f;

	public float CameraVSpeed = 10.0f;
	public float SpinHSpeed = 50.0f;
	public float Acceleration = 13.0f;
	public float AirResistance = 0.03f;
	public float MaxSpeed = 5.0f;
	public float StopThreshold = 0.0001f;

	private float speed = 0.0f;

	void Start ()
	{
	}

	float Deg2Rad(float degree)
	{
		return degree * PI / 180.0f;
	}

	float Rad2Deg(float radian)
	{
		return radian * 180.0f / PI;
	}

	private void updateSpeed()
	{
		if (Input.GetAxisRaw("Booster") > 0) {
			speed += Acceleration * Time.deltaTime;
		}
		speed -= AirResistance * speed;
		speed = (MaxSpeed < speed) ? MaxSpeed : speed;
		speed = (speed < StopThreshold) ? 0.0f : speed;
	}

	private void updateTransform()
	{
		float spinHAngle = Input.GetAxisRaw("Horizontal") * Time.deltaTime * SpinHSpeed;
		float cameraVAngle = Input.GetAxisRaw("Vertical") * Time.deltaTime * CameraVSpeed;
		transform.Rotate(0, spinHAngle, 0);
		transform.Translate(0, 0, speed * Time.deltaTime);
	}

	void Update()
	{
		Camera cam = transform.FindChild("Camera").GetComponent<Camera>();

		updateSpeed();
	}
}
