using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
	public GameObject pickupEffect;
	public int zivotynavic = 40;

	void OnTriggerEnter (Collider other){
		if(other.CompareTag("Player")){
			Pickup(other);
		}
	}
	void Pickup(Collider postava){

		
		Instantiate(pickupEffect, transform.position, transform.rotation);
		Target stats = postava.GetComponent<Target>();
		stats.zivoty += zivotynavic;

		Destroy(gameObject);
	}
}
