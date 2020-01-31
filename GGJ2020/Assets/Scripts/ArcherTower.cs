using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    public override void Start()
    {
		base.Start();
        Health = 100;
        Damage = 8;
        Range = 5;
        SetTarget();
        InvokeRepeating("Attack", 1.0f, 1.0f);
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
