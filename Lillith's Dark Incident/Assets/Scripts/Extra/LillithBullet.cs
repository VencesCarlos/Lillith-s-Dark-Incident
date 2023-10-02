using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillithBullet : MonoBehaviour
{
	[Header("Bullet")]
	[SerializeField] private float bulletSpeed;
	
	[Header("Components")]
	private Rigidbody2D rb2D;
	private CapsuleCollider2D capsuleCollider2D;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		capsuleCollider2D = GetComponent<CapsuleCollider2D>();
	}

	private void Update()
	{
		rb2D.velocity = Vector2.up * bulletSpeed;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Pool") || other.CompareTag("Enemy"))
		{
			this.gameObject.SetActive(false);
		}
	}
}
