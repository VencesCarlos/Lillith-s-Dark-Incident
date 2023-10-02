using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private int health;
	[SerializeField] private int numberOfStars;
	[SerializeField] private Image[] stars;
	
	[Header("Sprites")]
	[SerializeField] private Sprite fullStar;
	[SerializeField] private Sprite emptyStar;
	
	[Header("Taking Damage")]
	[SerializeField] private float noControlTime;
	[SerializeField] private float noCollisionTime;
	private PlayerController playerController;
	
	
	private void Start()
	{
		playerController = GetComponent<PlayerController>();
	}
	
	private void Update()
	{
		if (health > numberOfStars)
		{
			health = numberOfStars;
		}

		for (int i = 0; i < stars.Length; i++)
		{
			if (i < health)
			{
				stars[i].sprite = fullStar;
			}
			else
			{
				stars[i].sprite = emptyStar;	
			}
		
			if (i < numberOfStars)
			{
				stars[i].enabled = true;
			}
			else
			{
				stars[i].enabled = false;
			}
		}
		
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	public void TakeDamage(Vector2 position)
	{
		health--;
		StartCoroutine(NoControl());
		StartCoroutine(NoCollision());
		ScreenShake.Instance.Shake(0.1f, 5f);
		playerController.KnockBack(position);
	}
	
	void OnParticleCollision(GameObject other) {
		TakeDamage(this.transform.position);
	}
	
	private IEnumerator NoControl()
	{
		playerController.canMove = false;
		yield return new WaitForSeconds(noControlTime);
		playerController.canMove = true;
	}
	
	private IEnumerator NoCollision()
	{
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.75f);
		yield return new WaitForSeconds(noCollisionTime);
		GetComponent<CircleCollider2D>().enabled = true;
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
	}
}
