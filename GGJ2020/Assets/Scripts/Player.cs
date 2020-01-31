using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool isPlayer1;

    void Start()
    {
        
    }
	

    void Update()
    {
		HandleMovement();
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
