﻿using UnityEngine;
using System.Collections;

public class PlayerCameraController : MonoBehaviour
{
	public static float PI = 3.1415926535f;

	public float AccelerationV = 0.5f;

	public GameObject player;

	private float speedV = 0.0f;
	private float positionV = 1.0f;

	void Start ()
	{
	}

	void Update()
	{
		if (!player) {
			Debug.LogWarning("Player must be set to use this script");
		}

		Transform parentTrans = player.transform;
		float rotRadius = 2.0f;

		// Vertical angle
		float dir = Input.GetAxisRaw("Vertical");
		if (dir > 0.1f) {
			speedV += AccelerationV * Time.deltaTime;
		}
		else if (dir < -0.1f) {
			speedV -= AccelerationV * Time.deltaTime;
		}
		else {
			speedV = 0.0f;
		}
		speedV = (5.0f < speedV) ? 5.0f : speedV;
		speedV = (-5.0f > speedV) ? -5.0f : speedV;
		positionV += speedV;
		positionV = (2.0f < positionV) ? 2.0f : positionV;
		positionV = (-2.0f > positionV) ? -2.0f : positionV;

		transform.position = parentTrans.position - parentTrans.forward * rotRadius;
		transform.LookAt(parentTrans);
		transform.Translate(0.0f, positionV, 0.0f);
		transform.LookAt(parentTrans);
	}
}
