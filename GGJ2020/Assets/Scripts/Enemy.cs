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
		this.gameObject.tag = "Enemy";
	}

	public virtual void OnSpawn()
	{
	}

	public virtual void OnDamageTaken(Projectile hittingProjectile)
	{
		health = health - hittingProjectile.damage;
		if(health <= 0)
		{
			scene.OnEnemyKilled(this);
		}
	}

	public virtual void OnDeath()
	{
		Destroy(this.gameObject);
	}

	public virtual void Update()
    {
        
    }
}
