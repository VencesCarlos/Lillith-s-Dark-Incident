using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MenuController : MonoBehaviour
{
	[Header("Main Menu Buttons")]
	[SerializeField] private Button _defaultButton;
	
	[Header("Levels To Load")]
	[SerializeField] private GameObject _noSavedGame = null;
	[SerializeField] private string _newGameLevel;
	private string levelToLoad;
	
	[Header("Volume Settings")]
	[SerializeField] private TMP_Text volumeTextValue = null;
	[SerializeField] private Slider volumeSlider = null;
	[SerializeField] private float defaultVolume = 0.5f;
	
	[Header("Confirmation Prompt")]
	[SerializeField] private GameObject confirmationPrompt = null;
	
	private void Start()
	{
		if (_defaultButton != null)
		{
			_defaultButton.Select();
		}
	}
	
	public void NewGameYes()
	{
		SceneManager.LoadScene(_newGameLevel);
	}
	
	public void LoadGameYes()
	{
		if (PlayerPrefs.HasKey("SavedLevel"))
		{
			levelToLoad = PlayerPrefs.GetString("SavedLevel");
			SceneManager.LoadScene(levelToLoad);
		}
		else
		{
			_noSavedGame.SetActive(true);
		}
	}
	
	public void Exit()
	{
		Application.Quit();
	}
	
	public void setVolume(float volume)
	{
		float fixedVolume = Mathf.Clamp(20 * Mathf.Log10(volume), -80, 0);
		AudioListener.volume = (float)Math.Pow(10, fixedVolume / 20);
		volumeTextValue.text = (volume * 100).ToString("0");
	}
	
	public void VolumeApply()
	{
		PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
		StartCoroutine(ConfirmationBox());
	}
	
	public void ResetVolume(string menuType)
	{
		if (menuType == "Audio")
		{
			AudioListener.volume = defaultVolume;
			volumeSlider.value = defaultVolume;
			volumeTextValue.text = (defaultVolume * 100).ToString("0");
			VolumeApply();
		}
	}
	
	public IEnumerator ConfirmationBox()
	{
		confirmationPrompt.SetActive(true);
		yield return new WaitForSeconds(2f);
		confirmationPrompt.SetActive(false);
	}
}
