using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Scene scene;
	public bool isPlayer1;

	public float repairCooldown = 0.3f;
	public float moveSpeed = 2f;
	private float currentRepairCooldown = 0;

	private static Vector3 up = new Vector3(1, 0, 1).normalized;
	private static Vector3 down = up * -1;
	private static Vector3 left = new Vector3(-1, 0, 1).normalized;
	private static Vector3 right = left * -1;

	private Vector3 MoveDir;


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
			List<Tower> towersInRange = scene.GetTowersInRange(transform.position, 4f);
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
				transform.position += moveSpeed * up * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				transform.position += moveSpeed * down * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				transform.position += moveSpeed * left * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.position += moveSpeed * right * Time.deltaTime;
			}
		}
		else
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				transform.position += moveSpeed * up * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				transform.position += moveSpeed * down * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position += moveSpeed * left * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.position += moveSpeed * right * Time.deltaTime;
			}
		}
	}
}
