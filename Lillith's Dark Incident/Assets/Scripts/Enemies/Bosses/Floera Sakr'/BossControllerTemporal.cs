using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControllerTemporal : MonoBehaviour
{
	public float stateChangeInterval = 3f;
	public float[] stateProbabilities = { 0.4f, 0.3f, 0.3f };
	private int currentState;
	private float timer = 0f;
	public float moveSpeed = 5f;
	private Vector3 initialPosition;
	private Vector3 moveDestination;
	public float amplitude = 4f;
	public float frequency = 1f;
	private bool continueHorizontalMovement = true;
	public Transform player;
	private bool isRushing = false;
	private Vector3 center = new Vector3(0, 0, 0);
	
	
	[SerializeField] private GameObject bulletHellSpawnerMove;
	[SerializeField] private GameObject bulletHellSpawnerRush;
	[SerializeField] private GameObject bulletHellSpawnerHell;
	private BulletHellSpawner bulletHellSpawnerMoveScript;
	private BulletHellSpawner bulletHellSpawnerRushScript;
	private BulletHellSpawner bulletHellSpawnerHellScript;
	
	void Awake() 
	{
		bulletHellSpawnerMoveScript = bulletHellSpawnerMove.GetComponent<BulletHellSpawner>();
		bulletHellSpawnerRushScript = bulletHellSpawnerRush.GetComponent<BulletHellSpawner>();
		bulletHellSpawnerHellScript = bulletHellSpawnerHell.GetComponent<BulletHellSpawner>();
		
		bulletHellSpawnerHell.SetActive(true);
		bulletHellSpawnerRush.SetActive(true);
		bulletHellSpawnerMove.SetActive(true);	
	}
	
	private void Start()
	{
		initialPosition = transform.position;
		moveDestination = new Vector3(0f, 3f, 0f);
		
		Move();
	}
	
	private void Update()
	{
		timer += Time.deltaTime;

		// Verificar si es tiempo de cambiar de estado
		if (timer >= stateChangeInterval)
		{
			// Reiniciar el temporizador
			timer = 0f;

			// Determinar el nuevo estado utilizando el método Choose()
			currentState = (int)Choose(stateProbabilities);

			// Llamar al método correspondiente al estado actual
			switch (currentState)
			{
				case 0:
					Move();
					break;
				case 1:
					Rush();
					break;
				case 2:
					BulletHell();
					break;
			}
		}
	}
	
	private float Choose(float[] probs)
	{
		float total = 0;
		foreach (float elem in probs)
		{
			total += elem;
		}

		float randomPoint = Random.value * total;

		for (int i = 0; i < probs.Length; i++)
		{
			if (randomPoint < probs[i])
			{
				return i;
			}
			else
			{
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}
	
	private void Move()
	{
		Debug.Log("Estado Move() cumplido");
		
		// Si el jefe estaba en el estado 2 (BulletHell), desactivar el spawner de BulletHell y activar el spawner de movimiento
		bulletHellSpawnerHellScript.ClearParticles();
		bulletHellSpawnerHell.SetActive(false);
		bulletHellSpawnerRushScript.ClearParticles();
		bulletHellSpawnerRush.SetActive(false);
		bulletHellSpawnerMoveScript.ClearParticles();
		bulletHellSpawnerMove.SetActive(true);

		continueHorizontalMovement = true;

		if (isRushing)
		{
		// Si el jefe estaba en el estado 1 (Rush), iniciar la corrutina MoveToDestination para regresar a la posición de destino original
			StopAllCoroutines();
			StartCoroutine(MoveToDestination(moveDestination));
		}

		// Iniciar la interpolación lineal desde la posición actual hacia la posición de destino
		StartCoroutine(MoveToDestination(moveDestination));
	}
	
	private void Rush()
	{   
		bulletHellSpawnerMoveScript.ClearParticles();
		bulletHellSpawnerMove.SetActive(false);
		bulletHellSpawnerHellScript.ClearParticles();
		bulletHellSpawnerHell.SetActive(false);
		bulletHellSpawnerRushScript.ClearParticles();
		bulletHellSpawnerRush.SetActive(true);

		Debug.Log("Estado Rush() cumplido");
		StopAllCoroutines();
		continueHorizontalMovement = false;
		isRushing = false;

		StartCoroutine(RushTowardsPlayer());
	}
	
	private void BulletHell()
	{
		bulletHellSpawnerMoveScript.ClearParticles();
		bulletHellSpawnerMove.SetActive(false);
		bulletHellSpawnerRushScript.ClearParticles();
		bulletHellSpawnerRush.SetActive(false);
		bulletHellSpawnerHellScript.ClearParticles();
		bulletHellSpawnerHell.SetActive(true);
	
		Debug.Log("Estado BulletHell() cumplido");
		StopAllCoroutines();
		continueHorizontalMovement = false;
		
		StartCoroutine(MoveToDestination(center));
	}
	
	private IEnumerator MoveToDestination(Vector3 destination)
	{
		Vector3 startPosition = transform.position;
		float distance = Vector3.Distance(startPosition, destination);
		float duration = distance / moveSpeed * 0.5f;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration; // Calcular la posición intermedia en función del tiempo transcurrido y la velocidad
			Vector3 newPosition = Vector3.Lerp(startPosition, destination, t);

			transform.position = newPosition; // Actualizar la posición del objeto

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = destination; // Asegurarse de que la posición final sea exactamente la posición de destino

		StartCoroutine(HorizontalMovement()); // Agregar movimiento horizontal
	}
	
	private IEnumerator HorizontalMovement()
	{
		Vector3 startPosition = transform.position;
		float localElapsedTime = 0f;

		while (continueHorizontalMovement)
		{
			localElapsedTime += Time.deltaTime;

			float horizontalOffset = amplitude * Mathf.Sin(2f * Mathf.PI * frequency * localElapsedTime); // Calcular la posición horizontal en función del tiempo transcurrido

			Vector3 newPosition = new Vector3(startPosition.x + horizontalOffset, startPosition.y, startPosition.z); // Calcular la nueva posición del objeto

			transform.position = newPosition; // Actualizar la posición del objeto

			yield return null;
		}
	}
	
	private Vector3 GetPlayerPosition()
	{
		return player.position;
	}

	private IEnumerator RushTowardsPlayer()
	{
		isRushing = true; // Establecer la bandera de movimiento "Rush" en true

		while (isRushing)
		{
		   Vector3 playerPosition = GetPlayerPosition();
		   yield return StartCoroutine(MoveToDestination(playerPosition));
		   yield return new WaitForSeconds(0.4f);
		}
	}
}
