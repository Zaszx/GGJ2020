using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scene : MonoBehaviour
{
	public Transform towersParent;
	private List<Tower> towers = new List<Tower>();

    void Start()
    {
		InitTowers();
    }

	void InitTowers()
	{
		towers = towersParent.GetComponentsInChildren<Tower>().ToList();
	}
	
    void Update()
    {
        
    }
}
