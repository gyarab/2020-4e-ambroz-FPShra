using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Inicializace promennych pro kulku, napriklad gravitace, exploze, kolikrat muze dopadnout na zem
    public Rigidbody rb;
    public GameObject exploze;
    public LayerMask whatIsEnemies;

    [Range(0f,1f)]
    public float odrazy;
    public bool useGravity;


    public float vzdalenostExploze;
    public float explozeDamage;

    public int maxOdrazu;
    public float maxLifetime;
    public bool explozeDotyk = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start(){
        Setup();
    }
    //Aktualizace a hlidani pocet kolizi
    private void Update(){
        if(collisions > maxOdrazu){
            Explode();
        }
    //Pokud bude dele nazivu, znici se prekrocenim sveho casu na zivot
        maxLifetime -= Time.deltaTime;
        if(maxLifetime <=0){
            Explode();
        }
    }
    //Exploze kulky a zaznamenani poskozeni, vizualni exploze o chvilku zpomalena
    private void Explode(){
        if(exploze != null){
            Instantiate(exploze, transform.position, Quaternion.identity);
        }
        Collider[] enemies = Physics.OverlapSphere(transform.position, vzdalenostExploze, whatIsEnemies);
        for(int i = 0; i < enemies.Length; i++){
            enemies[i].GetComponent<Target>().TakeDamage(explozeDamage);
        }
        Invoke("Delay", 0.05f);
    }
    //Zniceni kulky po dopadu
    private void Delay(){
        Destroy(gameObject);
    }
    //Pokud trefi hrace ihned bouchne
    private void OnCollisionEnter(Collision collision){
        collisions ++;
        if(collision.collider.CompareTag("Player") && explozeDotyk){
            Explode();
        }
    }
   
    //Nastaveni kulky
    private void Setup(){
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = odrazy;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }
    //Ukazatel jak velky je vybuch
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, vzdalenostExploze);
    }

    }
