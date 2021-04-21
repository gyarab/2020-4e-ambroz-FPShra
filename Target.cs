using UnityEngine;
using static GunSystem;
using TMPro;
//Zdravi priser a hrace
public class Target : MonoBehaviour
{
   public float zivoty = 50;
   public TextMeshProUGUI ukazatelZivotu;
   //Poskozeni
   public void TakeDamage(float amount){
		  zivoty -= amount;
		  if(zivoty <= 0f){
				Die();
		  }
   }
	// Ukazatel zivotu v UI
   public void Update(){
		  if(ukazatelZivotu != null){
				ukazatelZivotu.SetText(zivoty + "");
		  }
   }
   // Zniceni objektu potom co nema zivoty
   void Die(){
	Destroy(gameObject);
   }


}
