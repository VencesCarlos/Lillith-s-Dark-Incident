using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	private Animator animator;
	[SerializeField] private AnimationClip transitionFinal;
	[SerializeField] private String sceneToLoad;
	
	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	public void CallTransition()
	{
		StartCoroutine(Transition());
	}
	
	private IEnumerator Transition()
	{
		animator.SetTrigger("Start");
		yield return new WaitForSeconds(transitionFinal.length);
		SceneManager.LoadScene(sceneToLoad);
	}
}
