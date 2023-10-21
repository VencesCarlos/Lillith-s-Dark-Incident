using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloeraHealthBar : MonoBehaviour
{
	[SerializeField] private Image healthBar;
	[SerializeField] private BossHealthTemporal bossHealth;
	private float currentHealth;
	private float maxHealth;
	
	void Update()
	{
		currentHealth = bossHealth.currentHealth;
		maxHealth = bossHealth.maxHealth;
		healthBar.fillAmount = currentHealth / maxHealth;	
	}
}
