using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  public NavMeshAgent agent;
  public Transform player;
  public LayerMask whatIsGround, whatIsPlayer;
  public float zivoty;

  //Chuze
  public Vector3 walkPoint;
  bool walkPointSet;
  public float walkPointRange;

  //Utok
public float vzdalenostViditelnosti;
bool uzUtocil;
public float casMeziUtokem;
public float vzdalenostUtoku;
public bool hracVDosahuViditelnosti, hracVDosahuUtoku;
public GameObject projectile;
//Nastaveni Navmesh(Kde se muze protivnik pohybovat)
private void Awake(){
	player = GameObject.Find("Postava").transform;
	agent = GetComponent<NavMeshAgent>();
}
//Kontrola zda je hrac v dosahu a zda na neho muze zautocit
private void Update(){
	hracVDosahuViditelnosti = Physics.CheckSphere(transform.position, vzdalenostViditelnosti, whatIsPlayer);
	hracVDosahuUtoku = Physics.CheckSphere(transform.position, vzdalenostUtoku, whatIsPlayer);
	//Podminky co delat jestlize hrace vidi nebo nevidi
	if(!hracVDosahuViditelnosti && !hracVDosahuUtoku){
		Patroling();
	}
	if(hracVDosahuViditelnosti && !hracVDosahuUtoku){
		ChasePlayer();
	}
	if(hracVDosahuUtoku && hracVDosahuViditelnosti){
		AttackPlayer();
	}
}
//Volna chuze
private void Patroling(){
	if(!walkPointSet){
	SearchWalkPoint();
	}


	if(walkPointSet){
		agent.SetDestination(walkPoint);
	}

	Vector3 distanceToWalkPoint = transform.position - walkPoint;
	

	if(distanceToWalkPoint.magnitude <1f){
		walkPointSet = false;
	}
	}
//Vybrani nahodneho bodu na zemi, kontrola zda je bod na zemi, protivnik nasledne dojde k tomuto bodu
private void SearchWalkPoint(){
	float randomZ = Random.Range(-walkPointRange, walkPointRange);
	float randomX = Random.Range(-walkPointRange, walkPointRange);

	walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

	if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
		walkPointSet = true;
	}
}
//Pokud protivnik vidi hrace ale neni v jeho dosahu utoku, zacne hrace pronasledovat
public void ChasePlayer(){
	agent.SetDestination(player.position);
}
//Pokud je hrac v dosahu utoku, zacne protivnik utocit svymi bombami
public void AttackPlayer(){
	agent.SetDestination(transform.position);
	transform.LookAt(player);
	//Vhozeni bomby na postavu
	if(!uzUtocil){
		
		Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
		rb.AddForce(transform.up * 4f, ForceMode.Impulse);
		
		uzUtocil = true;
		Invoke(nameof(ResetAttack), casMeziUtokem);
	}

}
//Resetovani utoku aby protivnik mohl znovu zautocit
public void ResetAttack(){
	uzUtocil = false;
}

//Protivnik dostava poskozeni
public void TakeDamage(int damage){
	zivoty -= damage;
	
	if(zivoty <= 0){
		Invoke(nameof(DestroyEnemy), 0.5f);
	}
}
//Zniceni bomby po dopadu
private void DestroyEnemy(){
	Destroy(gameObject);
}
//Vizualizace viditelnosti a vzdalenosti utoku v Unity
private void OnDrawGizmosSelected(){
	Gizmos.color = Color.red;
	Gizmos.DrawWireSphere(transform.position, vzdalenostUtoku);
	Gizmos.color = Color.yellow;
	Gizmos.DrawWireSphere(transform.position, vzdalenostViditelnosti);
}


}
