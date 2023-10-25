using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloeraPresentation : MonoBehaviour
{
	public GameObject objectToActivate;
	public float moveDuration = 1f;
	public float fadeDuration = 1f;
	public TextMeshProUGUI panel;

	// Start is called before the first frame update
	void Start()
	{
		// Move the object to the new position
		StartCoroutine(MoveObjectSmoothly(transform.position, new Vector3(0, 2, 0), moveDuration));

		// Increase the opacity of the panel and then fade it out
		StartCoroutine(FadePanelIn(panel, fadeDuration, 0f));
		StartCoroutine(FadePanelOut(panel, fadeDuration));

		// Wait for the delay plus the fade duration
		StartCoroutine(ActivateObjectAfterDelay(fadeDuration * 2f));
	}

	// Coroutine to move the object smoothly
	IEnumerator MoveObjectSmoothly(Vector3 startPosition, Vector3 endPosition, float duration)
	{
		float startTime = Time.time;
		float endTime = startTime + duration;

		while (Time.time < endTime)
		{
			float t = (Time.time - startTime) / duration;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		transform.position = endPosition;
	}

	// Coroutine to fade in the panel
	IEnumerator FadePanelIn(TextMeshProUGUI text, float duration, float delay)
	{
		Color startColor = text.color;
		startColor.a = 0f;
		text.color = startColor;
		yield return new WaitForSeconds(delay);

		float startTime = Time.time;
		float endTime = startTime + duration;

		while (Time.time < endTime)
		{
			float t = (Time.time - startTime) / duration;
			Color color = text.color;
			color.a = Mathf.Lerp(0f, 1f, t);
			text.color = color;
			yield return null;
		}

		Color endColor = text.color;
		endColor.a = 1f;
		text.color = endColor;
	}

	// Coroutine to fade out the panel and activate the object
	IEnumerator FadePanelOut(TextMeshProUGUI text, float duration)
	{
		float startTime = Time.time;
		float endTime = startTime + duration;

		while (Time.time < endTime)
		{
			float t = (Time.time - startTime) / duration;
			Color color = text.color;
			color.a = Mathf.Lerp(1f, 0f, t);
			text.color = color;
			yield return null;
		}

		Color endColor = text.color;
		endColor.a = 0f;
		text.color = endColor;

		// Activate the object after the panel has faded out
		objectToActivate.SetActive(true);
		gameObject.SetActive(false);
	}

	// Coroutine to activate the object after a delay
	IEnumerator ActivateObjectAfterDelay(float delay)
	{
		// Fade out the panel before activating the object
		StartCoroutine(FadePanelOut(panel, fadeDuration));

		yield return new WaitForSeconds(fadeDuration);
	}
}