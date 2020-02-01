using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Enemy
{
	public Tower targetTower;
	public float attackRange = 2.0f;

	public float attackCooldown = 1.0f;
	private float currentAttackCooldown = 0;
	private float _movespeed_original;
	private GameObject _boulder;

	public float aggroRange = 20;

	public override void Start()
	{
		_movespeed_original = movespeed;
		_boulder = Prefabs.BoulderPrefab;
	}

	public override void OnSpawn()
	{
		base.OnSpawn();
		UpdateTarget();
		targetTower = scene.baseTower;
	}

	public void UpdateTarget()
	{
		Tower newTarget = scene.GetClosestTowerToPos(transform.position, targetTower == null ? float.PositiveInfinity : aggroRange);
		if (newTarget != null)
			targetTower = newTarget;
	}

	public override void Update()
	{
		if (scene == null)
			return;
		currentAttackCooldown += Time.deltaTime;
		UpdateTarget();

		Vector3 targetWithoutY = targetTower.transform.position;
		targetWithoutY.y = 0;

		Vector3 posWithoutY = transform.position;
		posWithoutY.y = 0;

		Vector3 distanceToTarget = (targetWithoutY - posWithoutY);

		direction = (targetWithoutY - posWithoutY).normalized;

		if (distanceToTarget.magnitude <= attackRange)
		{
			movespeed = 0;
			if (currentAttackCooldown >= attackCooldown)
			{
				Throw();
				currentAttackCooldown = 0;
			}
		}
		else
		{
			movespeed = _movespeed_original;
			transform.position += movespeed * direction * Time.deltaTime;
		}
		base.Update();
	}

	public void Throw()
	{
		GetComponent<Animator>().SetTrigger("AttackTrigger");
		var boulder = GameObject.Instantiate(_boulder, transform.position + (Vector3.up + transform.forward * -0.5f) , Quaternion.identity).GetComponent<Boulder>();
		boulder.parent = this;
		boulder.targetEnemy = targetTower.transform;
		boulder.damage = damage;
	}
}
