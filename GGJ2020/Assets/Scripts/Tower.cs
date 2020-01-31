using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public int health;
	public int damage;
	public float range;
	public Transform target;
	public Healthbar healthbar;

    public Scene scene;

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
			healthbar.SetHealth(0);
			scene.OnTowerDestroyed(this);
		}
		else
		{
			healthbar.SetHealth(health);
		}
	}

    public virtual void SetTarget()
    {
        float min_distance = 0;
        foreach (Transform e in scene.enemiesParent)
        {
            float dist = Vector3.Distance(e.position, this.transform.position);
            if (min_distance < dist && dist < range)
            {
                min_distance = dist;
                this.target = e;
            }

        }
    }

    public virtual void Attack()
    {
        if (target == null)
        {
            Debug.Log("No Target in range for " + this.transform.name);
            return;
        }
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.health -= damage;
        return;
    }
}
