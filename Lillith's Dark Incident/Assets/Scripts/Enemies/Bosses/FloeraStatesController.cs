using System.Collections;
using System.Collections.Generic;
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
	
	[Header("Move Attack")]
	[SerializeField] private float frequency;
	[SerializeField] private float amplitude;
	private bool continueMoving;
	
	[Header("Rush Attack")]
	[SerializeField] private Transform playerPosition;
	[SerializeField] private bool isRushing;
	
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
	
	# region Utility
	private IEnumerator ChangeState()
	{
		returnTime = 25f;
		hasArrived = false;
		continueMoving = false;
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
	# endregion
	
	# region MoveAttack
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
		else
		{
			Debug.Log("Move Attack");
			continueMoving = true;
			StartCoroutine(HorizontalAttack());
		}
	}
	
	private IEnumerator HorizontalAttack()
	{
		Vector3 startPosition = transform.position;
		float elapsedTime = 0f;
		
		while (continueMoving)
		{
			elapsedTime += Time.deltaTime;
			float horizontalOffset = amplitude * Mathf.Sin(2f * Mathf.PI * frequency * elapsedTime);
			Vector3 newPosition = new Vector3(startPosition.x + horizontalOffset, startPosition.y, startPosition.z);
			transform.position = newPosition;
			yield return null;
		}
	}
	# endregion
	
	# region RushAttack
	private void RushAttack()
	{
		isRushing = false;
		StartCoroutine(RushToPlayer());
	}
	
	private IEnumerator RushToPlayer()
	{
		isRushing = true;
		
		while (isRushing)
		{
			Debug.Log("Rush Attack");
			Vector3 rushPosition = GetPlayerPosition();
			yield return StartCoroutine(MoveToDestination(rushPosition));
			yield return new WaitForSeconds(0.3f);
		}
		
		isRushing = false;
	}
	
	private Vector3 GetPlayerPosition()
	{
		return playerPosition.position;
	}
	# endregion
	
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
