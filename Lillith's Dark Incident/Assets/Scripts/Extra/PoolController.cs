using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{

	[SerializeField] private GameObject bullet;
	[SerializeField] private List<GameObject> bullets;
	[SerializeField] private int poolSize;
	
	private static PoolController _instance;
	public static PoolController Instance { get { return _instance;}}
	
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void NewBullet(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			GameObject bulletObj = Instantiate(bullet);
			bulletObj.SetActive(false);
			bullets.Add(bulletObj);
			bulletObj.transform.parent = this.transform;
		}
	}
	
	public GameObject ShootBullet()
	{
		for (int i = 0; i < bullets.Count; i++)
		{
			if (!bullets[i].activeInHierarchy)
			{
				bullets[i].SetActive(true);
				return bullets[i];
			}
		}
		NewBullet(1);
		bullets[bullets.Count - 1].SetActive(true);
		return bullets[bullets.Count - 1];
	}
}
