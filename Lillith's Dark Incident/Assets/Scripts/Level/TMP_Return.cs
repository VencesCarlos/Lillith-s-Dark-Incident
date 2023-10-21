using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TMP_Return : MonoBehaviour
{
	void Update()
	{
		if (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current.allControls.Any(control => control.IsActuated()))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		}	
	}
}
