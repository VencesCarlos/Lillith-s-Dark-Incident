using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
	[SerializeField] private SceneTransition sceneTransition;

	void Update()
	{
		if (GameObject.FindGameObjectWithTag("Enemy") == null)
		{	
			sceneTransition.CallTransition();
		}
	}
}
