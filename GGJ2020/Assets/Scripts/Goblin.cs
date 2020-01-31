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
			transform.position += direction * Time.deltaTime;
			int runAnimationIndex = DirectionToIndex(direction, 8);
			animator.Play(runDirections[runAnimationIndex]);
		}
	}

	//this function converts a Vector2 direction to an index to a slice around a circle
	//this goes in a counter-clockwise direction.
	public static int DirectionToIndex(Vector3 dir, int sliceCount)
	{
		//get the normalized direction
		Vector2 normDir = dir.normalized;
		//calculate how many degrees one slice is
		float step = 360f / sliceCount;
		//calculate how many degress half a slice is.
		//we need this to offset the pie, so that the North (UP) slice is aligned in the center
		float halfstep = step / 2;
		//get the angle from -180 to 180 of the direction vector relative to the Up vector.
		//this will return the angle between dir and North.
		float angle = Vector2.SignedAngle(Vector2.up, normDir);
		//add the halfslice offset
		angle += halfstep;
		//if angle is negative, then let's make it positive by adding 360 to wrap it around.
		if (angle < 0)
		{
			angle += 360;
		}
		//calculate the amount of steps required to reach this angle
		float stepCount = angle / step;
		//round it, and we have the answer!
		return Mathf.FloorToInt(stepCount);
	}
}
