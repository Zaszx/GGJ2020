using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float hitTreshold;
    [HideInInspector]
    public Transform targetEnemy;
    [HideInInspector]
    public float targetSpeed;
    [HideInInspector]
    public Vector3 targetDirection;
    [HideInInspector]
    public Vector3 direction;
    private float _distance;
    private Vector3 destination;
    public virtual void Start()
    {
        destination = CollisionCourse.CalculateInterceptionPoint(targetEnemy.position ,transform.position, targetDirection*targetSpeed, speed);
        direction = (destination - transform.position).normalized;
    }

    public virtual void Update()
    {
        if (targetEnemy != null)
        {
            Move();
            if (_distance < hitTreshold)
            {
                targetEnemy.GetComponent<Enemy>().OnDamageTaken(this);
                GameObject.Destroy(this.gameObject);
            }
        }
        else if (_distance > -2) Move();
        else GameObject.Destroy(this.gameObject);
    }

    public void Move()
    {
        _distance = (destination - transform.position).magnitude;
        transform.position += speed * direction * Time.deltaTime;
    }
}
