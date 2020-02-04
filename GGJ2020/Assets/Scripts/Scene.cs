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
	public float DragonSpawnRate = 30f;
	public float CatapultSpawnRate = 15f;
	public float GoblinSpawnRate = 7f;
	public int DragonMax = 1;
	public int CatapultMax = 3;
	public int GoblinMax = 5;

	public List<Enemy> enemies = new List<Enemy>();

	private float _currentSpawnRate;
	private int Dcount, Ccount, Gcount;
	private float DspawnCounter, CspawnCounter, GspawnCounter;

	void Start()
    {
		Dcount = Ccount = Gcount = 0;
		DspawnCounter = CspawnCounter = GspawnCounter = 0;
		InitTowers();
    }

	void InitTowers()
	{
		towers = towersParent.GetComponentsInChildren<Tower>().ToList();
		baseTower = towersParent.GetComponentInChildren<Base>();
	}
	
    void Update()
    {
		if (Gcount <= GoblinMax && enemies.Count == 0 || GspawnCounter > GoblinSpawnRate + Random.value)
		{
			SpawnEnemy(0);
			GspawnCounter = 0;
		}
		if (Ccount <= CatapultMax && CspawnCounter > CatapultSpawnRate + Random.value)
		{
			SpawnEnemy(1);
			CspawnCounter = 0;
		}
		if (Dcount <= DragonMax && DspawnCounter > DragonSpawnRate + Random.value)
		{
			SpawnEnemy(2);
			DspawnCounter = 0;
		}

		DspawnCounter += Time.deltaTime;
		CspawnCounter += Time.deltaTime;
		GspawnCounter += Time.deltaTime;

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
