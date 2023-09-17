using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
	[Header("Main Menu Buttons")]
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _loadGameButton;
	[SerializeField] private Button _optionsButton;
	[SerializeField] private Button _exitButton;
	private Button selectedButton;
	
	[Header("Menu Background")]
	[SerializeField] private Image newGameBackground;
	[SerializeField] private Image loadGameBackground;
	[SerializeField] private Image optionsBackground;
	[SerializeField] private Image exitBackground;
	Dictionary<Button, Image> buttonBackgrounds;
	

	private void Awake()
	{
		buttonBackgrounds = new Dictionary<Button, Image>()
		{
			{_newGameButton, newGameBackground},
			{_loadGameButton, loadGameBackground},
			{_optionsButton, optionsBackground},
			{_exitButton, exitBackground}
		};
	}	
	
	private void Update()
	{
		// Detect selected button
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
		}
		
		DetectSelectedButton();
	}
	
	private void DetectSelectedButton()
	{
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
			
			foreach (var button in buttonBackgrounds.Keys)
			{
				buttonBackgrounds[button].enabled = (button == selectedButton);
			}
		}
		Debug.Log(selectedButton.name);
	}
}
