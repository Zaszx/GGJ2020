using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Transform targetEnemy;
    public Vector3 direction;
    public virtual void Start()
    {
        direction = (targetEnemy.transform.position - transform.position).normalized;
    }

    public virtual void Update()
    {
        direction = (targetEnemy.transform.position - transform.position).normalized;
        transform.position += direction * Time.deltaTime;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        GameObject coll = collision.collider.gameObject;
        if (coll.tag == "Enemy")
        {
            Enemy enemy = coll.GetComponent<Enemy>();
            enemy.OnDamageTaken(this);
        }
        Destroy(this.gameObject);
    }

}
