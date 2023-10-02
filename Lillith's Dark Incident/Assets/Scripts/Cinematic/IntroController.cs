using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
	[SerializeField] private String _sceneName;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Q)) {
			SceneManager.LoadScene(_sceneName);
		}
	}
}
