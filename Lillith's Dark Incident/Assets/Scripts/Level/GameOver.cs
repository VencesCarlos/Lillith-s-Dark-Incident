using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	[SerializeField] private Button defaultButton;
	
	private void Start()
	{
		defaultButton.Select();
	}
	
	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
	public void Restart()
	{
		SceneManager.LoadScene("FloeraLevel");
	}
}
