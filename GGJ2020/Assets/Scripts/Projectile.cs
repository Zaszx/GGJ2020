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
    public Vector3 direction;
    private float _distance;
    public virtual void Start()
    {
        direction = (targetEnemy.transform.position - transform.position).normalized;
    }

    public virtual void Update()
    {
        if (targetEnemy != null)
        {
            Vector3 difference = targetEnemy.transform.position - transform.position;
            _distance = difference.magnitude;
            direction = difference.normalized;
            transform.position += speed * direction * Time.deltaTime;
            if (_distance < hitTreshold)
            {
                targetEnemy.GetComponent<Enemy>().OnDamageTaken(this);
                GameObject.Destroy(this.gameObject);
            }
        }
        else GameObject.Destroy(this.gameObject);
    }
}
