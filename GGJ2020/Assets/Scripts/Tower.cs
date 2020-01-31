using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public Scene scene;
	public Healthbar healthbar;

	public int health;
	public int damage;

	public virtual void Start()
    {
		scene = GameObject.FindObjectOfType<Scene>();
		healthbar.Init(health);
    }

    public virtual void Update()
    {
        
    }

	public virtual void OnDamageTaken(Enemy enemy)
	{
		health = health - enemy.damage;
		if(health <= 0)
		{
			scene.OnTowerDestroyed(this);
		}
		else
		{
			healthbar.SetHealth(health);
		}
	}
}
