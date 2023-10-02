using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
	private Button selectedButton;
	[Header("Main Menu Buttons")]
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _loadGameButton;
	[SerializeField] private Button _optionsButton;
	[SerializeField] private Button _exitButton;


	[Header("Menu Background")]
	[SerializeField] private Image _newGameBackground;
	[SerializeField] private Image _loadGameBackground;
	[SerializeField] private Image _optionsBackground;
	[SerializeField] private Image _exitBackground;
	Dictionary<Button, Image> buttonBackgrounds;

	[Header("Load Backgrounds")]
	[SerializeField] private Image _treeBackground;
	[SerializeField] private Image _squidBackground;
	[SerializeField] private Image _cloudBackground;

	[Header("Transition")]
	[SerializeField] private float transitionTime = 0.5f;
	[SerializeField] private float transitionTimer = 2f;
	[SerializeField] private bool transitionComplete = false;

	private void Awake()
	{
		buttonBackgrounds = new Dictionary<Button, Image>()
		{
			{_newGameButton, _newGameBackground},
			{_loadGameButton, _loadGameBackground},
			{_optionsButton, _optionsBackground},
			{_exitButton, _exitBackground},
		};

		foreach (var image in buttonBackgrounds.Keys)
		{
			Color color = buttonBackgrounds[image].color;
			color.a = 0f;
			buttonBackgrounds[image].color = color;
		}
	}
	


	private void Update()
	{
		Button prevButton = selectedButton;

		DetectSelectedButton();

		if (selectedButton != null)
		{
			if (selectedButton != prevButton)
			{
				transitionTimer = 0f;
				transitionComplete = false;
			}
			TransitionBackground();
		}

		switch (GameManager.Instance.currentLevel)
		{
			case 1:
				buttonBackgrounds[_loadGameButton] = _treeBackground;
				_treeBackground.enabled = true;
				_squidBackground.enabled = false;
				_cloudBackground.enabled = false;
				break;
			case 2:
				buttonBackgrounds[_loadGameButton] = _squidBackground;
				_treeBackground.enabled = false;
				_squidBackground.enabled = true;
				_cloudBackground.enabled = false;
				break;
			case 3:
				buttonBackgrounds[_loadGameButton] = _cloudBackground;
				_treeBackground.enabled = false;
				_squidBackground.enabled = false;
				_cloudBackground.enabled = true;
				break;
		}
	}

	private void DetectSelectedButton()
	{
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
		}
	}

	private void TransitionBackground()
	{
		if (selectedButton != null && !transitionComplete)
		{
			transitionTimer += Time.deltaTime;
			foreach (var button in buttonBackgrounds.Keys)
			{
				Color color = buttonBackgrounds[button].color;
				if (button == selectedButton)
				{
					color.a = Mathf.Lerp(0f, 1f, transitionTimer / transitionTime);
					color.a = Mathf.Clamp01(color.a);


					if (color.a >= 1f)
					{
						transitionComplete = false;
					}
				}
				else
				{
					if (color.a > 0f)
					{
						color.a = Mathf.Lerp(color.a, 0f, transitionTimer / transitionTime);
						color.a = Mathf.Clamp01(color.a);
					}
				}
				buttonBackgrounds[button].color = color;
			}
			if (transitionComplete)
			{
				if (transitionTimer >= transitionTime)
				{
					transitionTimer = 0f;
					transitionComplete = false;
				}
			}
		}
	}
}