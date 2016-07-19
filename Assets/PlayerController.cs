using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static float PI = 3.1415926535f;

	//public float CameraVSpeed = 10.0f;
	public float SpinHSpeed = 50.0f;
	public float Acceleration = 13.0f;
	public float AirResistance = 0.03f;
	public float MaxSpeed = 5.0f;
	public float StopThreshold = 0.0001f;

	public float CameraVAcceleration = 0.5f;

	private float speed = 0.0f;
	private float cameraVSpeed = 0.0f;
	private float cameraV = 1.0f;

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

	private void updateCamera()
	{
		Transform camTrans = transform.FindChild("Camera").GetComponent<Transform>();
		float rotRadius = 2.0f;

		// Vertical angle
		float dir = Input.GetAxisRaw("Vertical");
		if (dir > 0.1f) {
			cameraVSpeed += CameraVAcceleration * Time.deltaTime;
		}
		else if (dir < -0.1f) {
			cameraVSpeed -= CameraVAcceleration * Time.deltaTime;
		}
		else {
			cameraVSpeed = 0.0f;
		}
		cameraVSpeed = (5.0f < cameraVSpeed) ? 5.0f : cameraVSpeed;
		cameraVSpeed = (-5.0f > cameraVSpeed) ? -5.0f : cameraVSpeed;
		cameraV += cameraVSpeed;
		cameraV = (2.0f < cameraV) ? 2.0f : cameraV;
		cameraV = (-2.0f > cameraV) ? -2.0f : cameraV;

		camTrans.position = transform.position - transform.forward * rotRadius;
		camTrans.LookAt(transform);
		camTrans.Translate(0.0f, cameraV, 0.0f);
		camTrans.LookAt(transform);
	}

	void Update()
	{
		updateSpeed();
		updateTransform();
		updateCamera();
	}
}
