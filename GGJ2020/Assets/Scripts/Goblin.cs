using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
	public Tower targetTower;
	public float attackRange = 0.3f;

	public float attackCooldown = 1.0f;
	private float currentAttackCooldown = 0;

	public float aggroRange = 1;

	public static readonly string[] runDirections = { "Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE" };
	public static readonly string[] attackDirections = { "Attack_N", "Attack_NW", "Attack_W", "Attack_SW", "Attack_S", "Attack_SE", "Attack_E", "Attack_NE" };

	public Animator animator;

	public override void Start()
    {
		health = 100;
		damage = 8;

		animator = GetComponent<Animator>();
    }

	public override void OnSpawn()
	{
		base.OnSpawn();

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
		base.Update();
		if (scene == null)
			return;
		currentAttackCooldown += Time.deltaTime;
		UpdateTarget();

		Vector3 distanceToTarget = (targetTower.transform.position - transform.position);
		if(distanceToTarget.magnitude <= attackRange)
		{
			Vector3 direction = (targetTower.transform.position - transform.position).normalized;
			int runAnimationIndex = DirectionToIndex(direction, 8);
			animator.Play(attackDirections[runAnimationIndex]);

			if (currentAttackCooldown >= attackCooldown)
			{
				targetTower.OnDamageTaken(this);
				currentAttackCooldown = 0;
			}
		}
		else
		{
			Vector3 direction = (targetTower.transform.position - transform.position).normalized;
			transform.position += movespeed * direction * Time.deltaTime;
			int runAnimationIndex = DirectionToIndex(direction, 8);
			animator.Play(runDirections[runAnimationIndex]);
		}
	}
}
