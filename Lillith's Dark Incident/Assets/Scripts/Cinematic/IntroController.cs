using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
	[SerializeField] private Image[] images;
	[SerializeField] private float delay = 0.75f;
	private int currentIndex = 0;
	[SerializeField] private SceneTransition sceneTransition;

	private void Start()
	{
		for (int i = 1; i < images.Length; i++)
		{
			images[i].gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0))
		{
			ShowNextImage();
		}
		
		if (images[currentIndex] == images[images.Length - 1])
		{
			if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				sceneTransition.CallTransition();
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1))
		{
			sceneTransition.CallTransition();
		}
	}

	public void ShowNextImage()
	{
		Image currentImage = images[currentIndex];
		StartCoroutine(FadeImage(currentImage, 1.0f, 0.0f, delay / 2.0f));
		currentIndex = (currentIndex + 1) % images.Length;		
		Image nextImage = images[currentIndex];
		nextImage.gameObject.SetActive(true);
		nextImage.color = new Color(nextImage.color.r, nextImage.color.g, nextImage.color.b, 0.0f); // set alpha to 0
		StartCoroutine(FadeImage(nextImage, 0.0f, 1.0f, delay / 2.0f));
		
	}

	private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
	{
		image.color = new Color(image.color.r, image.color.g, image.color.b, startAlpha);
		float rate = 1.0f / duration;
		float elapsedTime = 0.0f;

		while (elapsedTime < duration)
		{
			float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime * rate);
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		image.color = new Color(image.color.r, image.color.g, image.color.b, endAlpha);
	}
}
