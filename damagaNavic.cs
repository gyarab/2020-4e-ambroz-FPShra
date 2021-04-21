using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagaNavic : MonoBehaviour
{
	public GameObject pickupEffect;
	public int damageNavic = 10;

	void OnTriggerEnter (Collider other){
		if(other.CompareTag("Player")){
			Pickup(other);
		}
	}
	void Pickup(Collider gun){

		
		Instantiate(pickupEffect, transform.position, transform.rotation);
		GunSystem stats = gun.GetComponent<GunSystem>();
		stats.damage += damageNavic;

		Destroy(gameObject);
	}
}