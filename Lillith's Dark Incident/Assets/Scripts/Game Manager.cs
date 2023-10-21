using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }
	
	public int currentLevel = 1;
	
	private static int fpsLimit = 60;
	
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}
	
	private void Start()
	{
		Application.targetFrameRate = fpsLimit;
		Cursor.lockState = CursorLockMode.Locked;
	}
}