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
	public float SpawnRate = 3.5f;

	public List<Enemy> enemies = new List<Enemy>();

	private float _currentSpawnRate;
	private float _spawnCounter = 0;

    void Start()
    {
		_spawnCounter = 0;
		_currentSpawnRate = SpawnRate + Random.value;
		InitTowers();
    }

	void InitTowers()
	{
		towers = towersParent.GetComponentsInChildren<Tower>().ToList();
		baseTower = towersParent.GetComponentInChildren<Base>();
	}
	
    void Update()
    {
		int id;
		if (enemies.Count == 0 || _spawnCounter > _currentSpawnRate)
		{
			float randValue = Random.value;
			if (randValue < 0.1f)
				id = 0; //goblin
			else if (randValue < 0.9f)
				id = 1; //catapult
			else
				id = 2; //dragon
			SpawnEnemy(id);
		}
		else
		{
			_spawnCounter += Time.deltaTime;
			if (Input.GetKeyDown(KeyCode.X))
				SpawnEnemy(0);
			if (Input.GetKeyDown(KeyCode.C))
				SpawnEnemy(1);
			if (Input.GetKeyDown(KeyCode.V))
				SpawnEnemy(2);
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

	public void SpawnEnemy(int enemyId)
	{
		GameObject enemyPrefab;
		switch (enemyId)
		{
			case 0:
				enemyPrefab = Prefabs.GoblinPrefab;
				break;
			case 1:
				enemyPrefab = Prefabs.CatapultPrefab;
				break;
			case 2:
				enemyPrefab = Prefabs.DragonPrefab;
				break;
			default:
				enemyPrefab = Prefabs.GoblinPrefab;
				break;
		}

		Enemy newEnemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
		newEnemy.scene = this;
		
		Vector2 random2dpos = Random.insideUnitCircle.normalized * 40.0f;
		newEnemy.transform.position = new Vector3(random2dpos.x, 0.5f, random2dpos.y);
		newEnemy.OnSpawn();

		enemies.Add(newEnemy);
		newEnemy.transform.SetParent(enemiesParent);

		_currentSpawnRate = SpawnRate + Random.value;
		_spawnCounter = 0;
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
