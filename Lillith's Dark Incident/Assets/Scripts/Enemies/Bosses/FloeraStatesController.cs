using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class FloeraStatesController : MonoBehaviour
{
	[Header("States")]
	[SerializeField] private States state;
	enum States
	{
		MoveAttack,
		RushAttack,
		HellAttack,
	}
	[SerializeField] private float[] probs;
	
	[Header("Rush Attack")]
	[SerializeField] private Transform playerPosition; 
	
	[Header("Returning")]
	private IEnumerator returnCoroutine;
	[SerializeField] private float returnTime;
	private Vector2 currentPosition;
	private Vector2 returnPosition;
	[SerializeField] private bool hasArrived;
	
	[Header("Components")]
	
	private Rigidbody2D rb2D;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		StartCoroutine(ChangeState());
	}

	private void Update()
	{
		switch (state)
		{
			case States.MoveAttack:
				MoveAttack();
				break;
			case States.RushAttack:
				RushAttack();
				break;
			case States.HellAttack:
				HellAttack();
				break;
		}
	}
	
	private IEnumerator ChangeState()
	{
		hasArrived = false;
		state = (States)Choose(probs);
		yield return new WaitForSeconds(3f);
		StartCoroutine(ChangeState());
	}
	
	private IEnumerator MoveToDestination(Vector2 destination)
	{
		float elapsedTime = 0f;
		currentPosition = transform.position;
		while (elapsedTime < returnTime && !hasArrived)
		{
			if (hasArrived)
			{
				yield break;
			}
			float t = Mathf.Clamp01(elapsedTime / returnTime);
			transform.position = Vector2.Lerp(currentPosition, destination, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.position = destination;
		
		hasArrived = true;
	}
	
	private void MoveAttack()
	{
		if (!hasArrived)
		{
			hasArrived = false;
			returnPosition = new Vector2(0, 3);
			if ((Vector2)transform.position != returnPosition)
			{
				returnCoroutine = MoveToDestination(returnPosition);
				StartCoroutine(returnCoroutine);
			}
			else
			{
				hasArrived = true;
				StopCoroutine(returnCoroutine);
			}
		}
	}
	
	private void RushAttack()
	{
		if (!hasArrived)
		{
			hasArrived = false;
			returnPosition = playerPosition.position;
			InvokeRepeating("Rushing", 0f, 0.5f);
		}
	}
	
	private IEnumerator Rushing()
	{
		returnPosition = playerPosition.position;
		StartCoroutine(MoveToDestination(returnPosition));
		yield return new WaitForSeconds(2f);
	}
	
	private void HellAttack()
	{
		returnPosition = new Vector2(0, 0);
		StartCoroutine(MoveToDestination(returnPosition));
		if ((Vector2)transform.position != returnPosition)
		{
			hasArrived = false;
			returnCoroutine = MoveToDestination(returnPosition);
			StartCoroutine(returnCoroutine);
		}
		else
		{
			hasArrived = true;
			StopCoroutine(returnCoroutine);
		}
	}
	
	
	float Choose (float[] probs) {

		float total = 0;

		foreach (float elem in probs) {
			total += elem;
		}

		float randomPoint = Random.value * total;

		for (int i= 0; i < probs.Length; i++) {
			if (randomPoint < probs[i]) {
				return i;
			}
			else {
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}
	
	
}
