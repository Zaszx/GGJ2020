using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[HideInInspector]
	public int health;
	public int damage;

	public Scene scene;
	
    public virtual void Start()
    {
	}

	public virtual void OnSpawn()
	{

	}

	public virtual void OnHit(Tower hittingTower)
	{

	}

	public virtual void OnDeath()
	{

	}

	public virtual void Update()
    {
        
    }
}
