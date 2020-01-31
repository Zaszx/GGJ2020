using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    public override void Start()
    {
		base.Start();
        ProjectilePrefab = scene.arrowPrefab;
        SetTarget();
        InvokeRepeating("Attack", 0, 1.0f);
    }

	public override void Update()
    {
		base.Update();
        SetTarget();
    }

    public override void SetTarget()
    {
        base.SetTarget();
    }

    public override void Attack()
    {
        base.Attack();
    }
}
