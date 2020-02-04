using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
	public Vector3 targetPosition;
	public Tower targetTower;
	public float DecayTimer = 3;

	float attackTime = 0.4f;
	float currentAttackTime = 0;
	private float _movespeed_original;
	private int _attackCounter;
	private bool attacking,dead,deadanim;
	private float _decayTimer;
	private Vector3 _deathPos;

	public override void OnSpawn()
	{
		base.OnSpawn();
		UpdateTarget();
		_movespeed_original = movespeed;
		_attackCounter = 0;
		transform.position += Vector3.up * 8;
		attacking = false;
		dead = false;
		deadanim = false;
		_decayTimer = DecayTimer;
	}

	public void UpdateTarget()
	{
		targetTower = scene.GetRandomTower();
		targetPosition = Random.onUnitSphere * 5.0f + targetTower.transform.position;
	}

	public override void Update()
    {
		var animator = GetComponent<Animator>();
		if (!dead)
		{
			if (targetTower != null)
			{
				Vector3 targetWithoutY = targetPosition;
				targetWithoutY.y = 0;

				Vector3 posWithoutY = transform.position;
				posWithoutY.y = 0;

				float distanceToTarget = Vector3.Distance(targetWithoutY, posWithoutY);
				if (distanceToTarget < 0.3f)
				{
					if (!attacking)
					{
						animator.SetTrigger("Attack");
						attacking = true;
					}

					movespeed = 0;
					currentAttackTime += Time.deltaTime;
					if (currentAttackTime >= attackTime)
					{
						targetTower.OnDamageTaken(this);
						currentAttackTime = 0;
						_attackCounter++;
						if (_attackCounter > 6)
						{
							_attackCounter = 0;
							UpdateTarget();
						}

					}
				}
				else
				{
					if (attacking)
					{
						animator.SetTrigger("Fly");
						attacking = false;
					}
					movespeed = _movespeed_original;
					direction = (targetWithoutY - posWithoutY).normalized;
					direction.y = 0;
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 15 * Time.deltaTime);
					transform.position += direction * Time.deltaTime * movespeed;
				}
			}
			else
			{
				UpdateTarget();
			}

		}
		else if (DecayTimer > 0)
		{
			if (!deadanim)
			{
			animator.SetTrigger("Death");
				deadanim = true;
			}
			float t = DecayTimer / _decayTimer;
			DecayTimer -= 2*Time.deltaTime;
			var color = GetComponent<Material>().color;
			float a = Mathf.Lerp(255, 0, t);
			transform.position = Vector3.Lerp((_deathPos - Vector3.up * 8), _deathPos, t);
			GetComponent<Material>().color = new Color(color.r, color.g, color.b, a);

		}
		else GameObject.Destroy(this);
		base.Update();
	}

	public override void OnDamageTaken(Projectile hittingProjectile)
	{
		if (health > 0)
			health = health - hittingProjectile.damage;
		else
		{
			dead = true;
			_deathPos = transform.position;
			scene.enemies.Remove(this);
		}
	}
}
