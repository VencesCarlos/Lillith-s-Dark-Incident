using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
	void Update()
	{
		if (GameObject.FindGameObjectWithTag("Enemy") == null)
		{	
			SceneManager.LoadScene("Victory");
		}
	}
}
