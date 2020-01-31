using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
	public Vector3 targetPosition;

    public override void Start()
    {
        
    }

	public override void OnSpawn()
	{
		base.OnSpawn();

		targetPosition = scene.baseTower.transform.position;
	}

	public override void Update()
    {
		base.Update();

		Vector3 direction = (targetPosition - transform.position).normalized;
		transform.position += direction * Time.deltaTime;
    }
}
