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

	public void SpawnEnemy()
	{
		Goblin newGoblin = Instantiate(Prefabs.GoblinPrefab).GetComponent<Goblin>();
		newGoblin.scene = this;

		newGoblin.transform.position = Random.insideUnitCircle.normalized * 6.0f;
		newGoblin.OnSpawn();

		enemies.Add(newGoblin);
		newGoblin.transform.SetParent(enemiesParent);
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
