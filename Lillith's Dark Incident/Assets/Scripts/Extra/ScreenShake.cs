using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	private float timeLeft;
	private float intensity;
	private float time;
	private float rotation;
	private float rotationQuantity;
	private float intensityQuantity;

	private Vector3 initialPos;
	private bool shake;

	private static ScreenShake _instance;
	public static ScreenShake Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void Start()
	{
		shake = false;
	}

	private void LateUpdate()
	{
		if (shake)
		{
			if (timeLeft > 0)
			{
				timeLeft -= Time.deltaTime;
				float xQuantity = initialPos.x + UnityEngine.Random.Range(-intensityQuantity, intensityQuantity) * intensity;
				float yQuantity = initialPos.y + UnityEngine.Random.Range(-intensityQuantity, intensityQuantity) * intensity;
				xQuantity = Mathf.MoveTowards(xQuantity, initialPos.x, time * Time.deltaTime);
				yQuantity = Mathf.MoveTowards(yQuantity, initialPos.y, time * Time.deltaTime);
				transform.position = new Vector3(xQuantity, yQuantity, initialPos.z);

				rotation = Mathf.MoveTowards(rotation, 0, time * rotationQuantity * Time.deltaTime);
				transform.rotation = Quaternion.Euler(0, 0, rotation * UnityEngine.Random.Range(-1f, 1f));
			}
			else
			{
				transform.position = initialPos;
				shake = false;
			}
		}
	}

	public void Shake(float time, float intensity)
	{
		initialPos = transform.position;
		shake = true;
		timeLeft = time;
		this.intensity = intensity;

		// Calcular el valor correcto para la rotación (puedes ajustar el factor según sea necesario)
		rotation = UnityEngine.Random.Range(-intensity * 0.5f, intensity * 0.5f);

		// Calcular el valor correcto para el tiempo
		this.time = time / intensity;
	}
}
