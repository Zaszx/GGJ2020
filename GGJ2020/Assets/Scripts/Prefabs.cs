using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs
{
	public static GameObject GoblinPrefab;
	public static GameObject DragonPrefab;

    static Prefabs()
	{
		GoblinPrefab = Resources.Load<GameObject>("Prefabs/Goblin");
		DragonPrefab = Resources.Load<GameObject>("Prefabs/Dragon");
	}
}
