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
	public Button defaultButtonOver;
	public Button defaultButtonPause;
	public PlayerInput playerInput;

	void Update()
	{
		if (player == null)
		{
			SceneManager.LoadScene("GameOver");
		}
		
		if (playerInput.actions["Pause"].ReadValue<float>() == 1&& !pausePanel.activeSelf)
		{
			pausePanel.SetActive(true);
			if (defaultButtonPause != null)
			{
				defaultButtonPause.Select();
			}
			Time.timeScale = 0f;
		}
		else if (playerInput.actions["Pause"].ReadValue<float>() == 1 && pausePanel.activeSelf)
		{
			pausePanel.SetActive(false);
			defaultButtonPause = null;
			Time.timeScale = 1f;
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
		SceneManager.LoadScene("MainMenu");
	}
}
