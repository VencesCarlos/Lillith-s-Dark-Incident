using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	[SerializeField] private Button defaultButton;
	[SerializeField] private SceneTransition sceneTransitionMenu;
	[SerializeField] private SceneTransition sceneTransitionRestart;

	private void Start()
	{
		defaultButton.Select();
	}
	
	public void MainMenu()
	{
		sceneTransitionMenu.CallTransition();
	}
	
	public void Restart()
	{
		sceneTransitionRestart.CallTransition();
	}
}
