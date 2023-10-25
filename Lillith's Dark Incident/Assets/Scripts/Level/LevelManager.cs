using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
	public GameObject player;
	public GameObject pausePanel;
	public Button defaultButtonPause;
	public PlayerInput playerInput;
	private bool isPaused = false;
	[SerializeField] private SceneTransition sceneTransition;

	void Update()
	{
		if (player == null)
		{
			sceneTransition.CallTransition();
		}
		else if (Keyboard.current.escapeKey.wasPressedThisFrame || playerInput.actions["Pause"].triggered)
		{
			if (!pausePanel.activeSelf && !isPaused)
			{
				isPaused = true;
				pausePanel.SetActive(true);
				if (defaultButtonPause != null)
				{
					defaultButtonPause.Select();
				}
				Time.timeScale = 0f;
			}
			else if (pausePanel.activeSelf && isPaused)
			{
				isPaused = false;
				pausePanel.SetActive(false);
				defaultButtonPause = null;
				Time.timeScale = 1f;
			}
		}
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenu()
	{
		Time.timeScale = 1f;
		// SceneManager.LoadScene("MainMenu");
	}
}
