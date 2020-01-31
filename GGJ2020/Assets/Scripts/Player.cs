using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Scene scene;
	public bool isPlayer1;

	public float repairCooldown = 0.3f;
	private float currentRepairCooldown = 0;

    void Start()
    {
        
    }
	
    void Update()
    {
		HandleMovement();
		HandleRepair();

	}

	void HandleRepair()
	{
		currentRepairCooldown += Time.deltaTime;
		if(currentRepairCooldown >= repairCooldown)
		{
			List<Tower> towersInRange = scene.GetTowersInRange(transform.position, 0.1f);
			towersInRange.ForEach(t => t.OnRepaired(1));
			if(towersInRange.Count > 0)
			{
				currentRepairCooldown = 0;
			}
		}
	}

	void HandleMovement()
	{
		if (isPlayer1)
		{
			if (Input.GetKey(KeyCode.W))
			{
				transform.position += Vector3.up * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				transform.position += Vector3.down * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				transform.position += Vector3.left * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.position += Vector3.right * Time.deltaTime;
			}
		}
		else
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				transform.position += Vector3.up * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				transform.position += Vector3.down * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position += Vector3.left * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.position += Vector3.right * Time.deltaTime;
			}
		}
	}
}
