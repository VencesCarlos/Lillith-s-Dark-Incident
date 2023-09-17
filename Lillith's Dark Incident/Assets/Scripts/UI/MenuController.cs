using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[Header("Main Menu Buttons")]
	[SerializeField] private Button _defaultButton;
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		
		if (_defaultButton != null)
		{
			_defaultButton.Select();
		}
	}
}
