using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs
{
	public static GameObject GoblinPrefab;

    static Prefabs()
	{
		GoblinPrefab = Resources.Load<GameObject>("Prefabs/Goblin");
	}
}
