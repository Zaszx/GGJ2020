using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int health;
	public int damage;

	public Scene scene;
	
    public virtual void Start()
    {
	}

	public virtual void OnSpawn()
	{

	}

	public virtual void OnDamageTaken(Tower hittingTower)
	{

	}

	public virtual void OnDeath()
	{

	}

	public virtual void Update()
    {
        
    }
}
