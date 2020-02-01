using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	public Transform attachedEntity;
	public Transform healthbarParent;

	public Image healthbarImage;

	private int maxHealth;
	private int currentHealth;

	public void Init(int maxHealth)
	{
		this.maxHealth = maxHealth;
		if (attachedEntity == null)
			attachedEntity = transform.parent;
	}

    public void SetHealth(int health)
	{
		currentHealth = health;
		healthbarImage.fillAmount = (float)currentHealth / maxHealth;
	}

	public void Update()
	{
		if(attachedEntity != null)
		{
			Vector3 wantedPos = Camera.main.WorldToScreenPoint(attachedEntity.position + Vector3.up * 0.5f);
			healthbarParent.position = wantedPos;
		}
	}
}
