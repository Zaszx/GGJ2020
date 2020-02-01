using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public int health;
	private int maxHealth;
	public int damage;
	public float range;
    public float attackRate;
	public Transform target;
	public Healthbar healthbar;
    public GameObject ProjectilePrefab;

    public Scene scene;

	public virtual void Start()
    {
		scene = GameObject.FindObjectOfType<Scene>();
		healthbar.Init(health);

		maxHealth = health;
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

	public virtual void OnRepaired(int repairAmount)
	{
		health = Mathf.Min(maxHealth, health + repairAmount);
		healthbar.SetHealth(health);
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
        if (target != null)
        {
            GameObject projectile = GameObject.Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            var proj = projectile.GetComponent<Projectile>();
            proj.targetEnemy = target;
            proj.targetSpeed = target.GetComponent<Enemy>().movespeed;
            proj.targetDirection = target.GetComponent<Enemy>().direction;
        }
    }
}
