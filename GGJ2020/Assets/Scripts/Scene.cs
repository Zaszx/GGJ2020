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
        if(enemies.Count == 0 || Random.value < 0.01f)
		{
			SpawnEnemy();
		}
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
}
