using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public Scene scene;

	public virtual void Start()
    {
		scene = GameObject.FindObjectOfType<Scene>();
    }

    public virtual void Update()
    {
        
    }
}
