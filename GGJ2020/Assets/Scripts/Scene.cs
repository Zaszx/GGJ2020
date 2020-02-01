using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scene : MonoBehaviour
{
	public Transform towersParent;
	public Transform enemiesParent;
	private List<Tower> towers = new List<Tower>();
	public Base baseTower;
	public GameObject arrowPrefab;

	public List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
		InitTowers();
    }

	void InitTowers()
	{
		towers = towersParent.GetComponentsInChildren<Tower>().ToList();
		baseTower = towersParent.GetComponentInChildren<Base>();
	}
	
    void Update()
    {
        if(enemies.Count == 0 || Random.value < 0.001f)
		{
			SpawnEnemy();
		}
    }

	public void OnTowerDestroyed(Tower tower)
	{
		towers.Remove(tower);
		GameObject.Destroy(tower.gameObject);
	}

	public void OnEnemyKilled(Enemy enemy)
	{
		enemies.Remove(enemy);
		GameObject.Destroy(enemy.gameObject);
	}

	public Tower GetRandomTower()
	{
		return towers[Random.Range(0, towers.Count)];
	}

	public void SpawnEnemy()
	{
		GameObject enemyPrefab = Random.value < 0.5f ? Prefabs.DragonPrefab : Prefabs.GoblinPrefab;
		Enemy newEnemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
		newEnemy.scene = this;
		
		Vector2 random2dpos = Random.insideUnitCircle.normalized * 50.0f;
		newEnemy.transform.position = new Vector3(random2dpos.x, 0.5f, random2dpos.y);
		newEnemy.OnSpawn();

		enemies.Add(newEnemy);
		newEnemy.transform.SetParent(enemiesParent);
	}

	public Tower GetClosestTowerToPos(Vector3 position, float aggroRange)
	{
		Tower result = towers.OrderBy(tower => Vector3.Distance(tower.transform.position, position)).First();

		if (Vector3.Distance(result.transform.position, position) > aggroRange)
			return null;

		return result;
	}

	public List<Tower> GetTowersInRange(Vector3 position, float range)
	{
		return towers.Where(tower => Vector3.Distance(position, tower.transform.position) <= range).ToList();
	}
}
