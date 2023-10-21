using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreText;
	private float score;
	
	[SerializeField] private GameObject player;
	[SerializeField] private PlayerHealth playerHealth;
	private float elapsedTime;
	private float regen = 30f;
	
	private void Update()
	{
		if (player != null)
		{
			score += Time.deltaTime * 25;
			scoreText.text = ((int)score).ToString();
			
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= regen)
			{
				elapsedTime = 0f;
				playerHealth.health += 1;
				Debug.Log("Regen");
			}
			
			if ((int)score % 5000 == 0 && (int)score != 0)
			{
				playerHealth.health += 1;
			}
		}
		
		if (score < 0)
		{
			score = 0;
		}
	}
	
	public void Bonus()	
	{
		score += 35;
	}
	
	public void Penalty()
	{
		score -= 550;
	}
}
