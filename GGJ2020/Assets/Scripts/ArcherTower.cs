﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    public override void Start()
    {
		base.Start();
        ProjectilePrefab = Prefabs.ArrowPrefab;
        SetTarget();
        InvokeRepeating("Attack", 0, attackRate);
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
