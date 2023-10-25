using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TMP_Return : MonoBehaviour
{
	[SerializeField] private SceneTransition sceneTransition;
	void Update()
	{
		if (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current.allControls.Any(control => control.IsActuated()))
		{
			sceneTransition.CallTransition();
		}	
	}
}
