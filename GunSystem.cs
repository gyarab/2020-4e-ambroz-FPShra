using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
   public float damage;
   public float casMeziStrelou, spread, range, reloadTime;
   public int zasobnik;
   public bool allowButtonHold;
   int bulletsLeft, buletsShot;

   bool shooting, readyToShoot, reloading;

   public Camera fpsCam;

   public GameObject impactEffect;

   public TextMeshProUGUI ammunitionDisplay;

   //Zbran ihned pripravena a s plnym zasobnikem
   private void Awake(){
		  bulletsLeft = zasobnik;
		  readyToShoot = true;
   }
   
   
   private void Update(){
		  MyInput();
		  //Ukazatel naboju v UI
		  if(ammunitionDisplay != null){
				ammunitionDisplay.SetText(bulletsLeft + "/" + zasobnik);
		  }
   }
	
   //Pridani klaves pro strileni
   private void MyInput(){
		if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
		else shooting = Input.GetKeyDown(KeyCode.Mouse0); 
	
		

		if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < zasobnik && !reloading) Reload();

		if(readyToShoot && shooting && !reloading && bulletsLeft > 0){
			Shoot();
		}
		}
		//Strelba
		private void Shoot(){
			// Moznost udelat zbran ve stylu brokovnice, rozptyl kulek
			float x = Random.Range(-spread, spread);
			float y = Random.Range(-spread, spread);
			//Vektor, kam bude dopadat výstřel
			Vector3 direction = fpsCam.transform.forward + new Vector3(x,y,0);
			
			//Vystrel
			RaycastHit hit;
			//Zaznamenani vystrelu
			if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
			{
				Target target = hit.transform.GetComponent<Target>();
				if(target != null){
					target.TakeDamage(damage);
				}
			}
			//Dopad kulky
			GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impact, 1f);

			readyToShoot = false;
			bulletsLeft --;
			Invoke("ResetShot", casMeziStrelou);
		}

		//Restartovani vystrelu
		private void ResetShot(){
			readyToShoot =true;
		}

		//Nabijeni zbrane a pauza aby se zbran nabila
		private void Reload(){
		reloading = true;
		Invoke("ReloadFinished", reloadTime);
		}

		//Nabiti zbrane a nasledovne zrestartovani zasobniku na plny, ukonceni nabijeni
		private void ReloadFinished(){
			bulletsLeft = zasobnik;
			reloading = false;
		}


}
