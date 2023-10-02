using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	
	// Number Variables
	[SerializeField] private float moveSpeed;
	[SerializeField] [Range(0f, 0.2f)] private float smoothMove;
	
	// Vector Variables
	private Vector2 input;
	private Vector2 finalInput;
	private Vector2 speed = Vector2.zero;
	
	[Header("Shoot")]
	
	[SerializeField] [Range(0f, 0.05f)] private float bulletCooldown;
	private float bulletCooldownTimer;
	
	[Header("Components")]
	
	private Rigidbody2D rb2D;
	private PlayerInput playerInput;
	private Animator animator;
	
	[Header("Taking Damage")]
	[SerializeField] private Vector2 knockbackForce;
	public bool canMove = true;
		
	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		bulletCooldownTimer += Time.deltaTime;
		
		animator.SetFloat("xSpeed", rb2D.velocity.x);
	
		ReadInput();
		Shoot();
	}
	
	private void FixedUpdate()
	{
		if (canMove)
		{
			Move();	
		}
	}
	
	private void ReadInput()
	{
		input = playerInput.actions["Move"].ReadValue<Vector2>().normalized;
	}
	
	private void Move()
	{
		finalInput = Vector2.SmoothDamp(finalInput, input, ref speed, smoothMove);
		rb2D.velocity = finalInput * moveSpeed;
	}
	
	private void Shoot()
	{
		if ((playerInput.actions["Shoot"].ReadValue<float>() != 0) && (bulletCooldownTimer > bulletCooldown))
		{
			bulletCooldownTimer = 0;
			GameObject bullet = PoolController.Instance.ShootBullet();
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
		}
	}
	
	public void KnockBack(Vector2 pointOfHit)
	{	
		Vector2 direction = (Vector2.zero - pointOfHit).normalized;
		rb2D.velocity = (direction * knockbackForce.magnitude) * -1;
	}
}
