﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        setRotation();
    }

    private void setRotation()
    {
        float angle = Vector3.Angle(direction,Vector3.up);
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
