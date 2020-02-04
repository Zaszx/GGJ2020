using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Scene scene;
	public bool isPlayer1;

	public float repairCooldown = 0.2f;
	public float moveSpeed = 5f;
	private float currentRepairCooldown = 0;

	private static Vector3 up = new Vector3(1, 0, 1).normalized;
	private static Vector3 down = up * -1;
	private static Vector3 left = new Vector3(-1, 0, 1).normalized;
	private static Vector3 right = left * -1;

	private Vector3 MoveDir;


	public void Start()
    {
    }
	
    public void Update()
    {
		HandleRepair();
	}

	public void FixedUpdate()
	{
		HandleMovement();
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
		var rigid = GetComponent<Rigidbody>();
		Quaternion rot = transform.rotation;
		if (isPlayer1)
		{
			if (Input.GetKey(KeyCode.W))
			{
				rigid.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
			}
			if (Input.GetKey(KeyCode.S))
			{
				rigid.MovePosition(transform.position - transform.forward * moveSpeed * Time.fixedDeltaTime);
			}
			if (Input.GetKey(KeyCode.A))
			{
				rigid.MoveRotation(Quaternion.LookRotation(transform.forward - transform.right*3*Time.fixedDeltaTime,Vector3.up));
			}
			if (Input.GetKey(KeyCode.D))
			{
				rigid.MoveRotation(Quaternion.LookRotation(transform.forward + transform.right * 3 * Time.fixedDeltaTime, Vector3.up));
			}
		}
		else
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				rigid.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				rigid.MovePosition(transform.position - transform.forward * moveSpeed * Time.fixedDeltaTime);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				rigid.MoveRotation(Quaternion.LookRotation(transform.forward - transform.right * 3 * Time.fixedDeltaTime, Vector3.up));
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				rigid.MoveRotation(Quaternion.LookRotation(transform.forward + transform.right * 3 * Time.fixedDeltaTime, Vector3.up));
			}
		}
	}
}
