using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs
{
	public static GameObject GoblinPrefab;
	public static GameObject DragonPrefab;
	public static GameObject DragonPrefab_attack;
	public static GameObject DragonPrefab_death;
	public static GameObject ArrowPrefab;
	public static GameObject CatapultPrefab;
	public static GameObject BoulderPrefab;

	static Prefabs()
	{
		GoblinPrefab = Resources.Load<GameObject>("Prefabs/Goblin");
		DragonPrefab = Resources.Load<GameObject>("Prefabs/Dragon");
		DragonPrefab_attack = Resources.Load<GameObject>("Prefabs/Dragon_attack");
		DragonPrefab_attack = Resources.Load<GameObject>("Prefabs/Dragon_death");
		ArrowPrefab = Resources.Load<GameObject>("Prefabs/Arrow");
		CatapultPrefab = Resources.Load<GameObject>("Prefabs/Catapult");
		BoulderPrefab = Resources.Load<GameObject>("Prefabs/Boulder");
	}
}
