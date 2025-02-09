using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthTemporal : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;
	public ScoreManager scoreManager;
	
	private void Start()
	{
		currentHealth = maxHealth;
	}
	
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerHealth>().TakeDamage(transform.position);
		}	
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("PlayerBullet"))
		{
			scoreManager.Bonus();
			StartCoroutine(Hit());
			TakeDamage(25);
		}	
	}
	
	private IEnumerator Hit()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}
