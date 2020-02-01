using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float hitTreshold;
    public float missDistance;
    [HideInInspector]
    public Transform targetEnemy;
    [HideInInspector]
    public float targetSpeed;
    [HideInInspector]
    public Vector3 targetDirection;
    [HideInInspector]
    public Vector3 direction;
    private float _distance;
    private float _distanceToTarget;
    private Vector3 destination;
    private bool pastPoint;
    public virtual void Start()
    {
        pastPoint = false;
        destination = CollisionCourse.CalculateInterceptionPoint(targetEnemy.position ,transform.position, targetDirection*targetSpeed, speed);
        direction = (destination - transform.position).normalized;
    }

    public virtual void Update()
    {
        Move();
        if (targetEnemy != null)
        {
            _distanceToTarget = Vector3.Distance(targetEnemy.position, transform.position);
            if (_distanceToTarget < hitTreshold)
            {
                targetEnemy.GetComponent<Enemy>().OnDamageTaken(this);
                GameObject.Destroy(this.gameObject);
            }
        }
        if (pastPoint && _distance > 1) GameObject.Destroy(this.gameObject);
    }

    public void Move()
    {
        _distance = (destination - transform.position).magnitude;
        if (_distance < 0.03f) pastPoint = true;
        transform.position += speed * direction * Time.deltaTime;
    }
}
