using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
	Vector3 targetPosition;
	Tower targetTower;

	float attackTime = 1.0f;
	float currentAttackTime = 0;

	public override void OnSpawn()
	{
		base.OnSpawn();
		UpdateTarget();
		transform.position += Vector3.up * 8;
	}

	public void UpdateTarget()
	{
		targetTower = scene.GetRandomTower();
		targetPosition = Random.onUnitSphere * 5.0f + targetTower.transform.position;
	}

	public override void Update()
    {
		Vector3 targetWithoutY = targetPosition;
		targetWithoutY.y = 0;

		Vector3 posWithoutY = transform.position;
		posWithoutY.y = 0;

		float distanceToTarget = Vector3.Distance(targetWithoutY, posWithoutY);
		if(distanceToTarget < 0.3f)
		{
			currentAttackTime += Time.deltaTime;
			if(currentAttackTime >= attackTime)
			{
				targetTower.OnDamageTaken(this);
				currentAttackTime = 0;
				UpdateTarget();
			}
		}
		else
		{
			Vector3 direction = (targetWithoutY - posWithoutY).normalized;
			direction.y = 0;
			transform.position += direction * Time.deltaTime * movespeed;
		}
		base.Update();
	}
}
