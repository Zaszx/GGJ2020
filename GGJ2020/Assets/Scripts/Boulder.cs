using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Projectile
{
    [HideInInspector]
    public Enemy parent;
    // Start is called before the first frame update
    public override void Start()
    {
        pastPoint = false;
        destination = targetEnemy.position;
        direction = (destination - transform.position).normalized;
    }

    // Update is called once per frame
    public override void Update()
    {
        transform.position += speed * direction * Time.deltaTime;
        if (_distanceToTarget < hitTreshold)
        {
            targetEnemy.GetComponent<Tower>().OnDamageTaken(parent);
            GameObject.Destroy(this.gameObject);
        }
    }
}
