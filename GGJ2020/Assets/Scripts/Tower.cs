using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public float Range { get; set; }
    public Transform Target { get; set; }
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
            if (min_distance < dist && dist < Range)
            {
                min_distance = dist;
                this.Target = e;
            }

        }
    }

    public virtual void Attack()
    {
        if (Target == null)
        {
            Debug.Log("No Target in range for " + this.transform.name);
            return;
        }
        Enemy enemy = Target.GetComponent<Enemy>();
        enemy.health -= Damage;
        return;
    }
}
