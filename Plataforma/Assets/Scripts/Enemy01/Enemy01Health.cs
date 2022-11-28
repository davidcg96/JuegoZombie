using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{
    
    [SerializeField] private int startingHealth = 20;       //vida inicial enemigo
    [SerializeField] private float timeSinceLastHit = 0.5f; //tiempo desde el ultimo golpe
    [SerializeField] private float dissapearSpeed = 2f;     // tiempo que tarda en desaparecer
    [SerializeField] private int currentHealth;             //vida actual enemigo
   

    private float timer = 0f;                       //contador para desaàrecer
    private Animator anim;                          //animacion
    private NavMeshAgent nav;                       //IA
    private bool isAlive;                           //estoy vivo
    private new Rigidbody rigidbody;                    // su cuerpo 
    private CapsuleCollider capsuleCollider;        //collider
    private bool dissapearEnemy=false;              // ha desaparecido el enemigo
    private BoxCollider weaponCollider; //para descativar el collider del enemigo y no me quite daño al morir
    private CapsuleCollider enemyCollider; //lo mismo que el anterior

    //sonidos
    private new AudioSource audio;
    public AudioClip deadClip;
    public AudioClip golpeClip;
    public bool IsAlive { get { return isAlive; } }

    // Start is called before the first frame update
    void Start()
    {
        //obtener los componentes
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //inicializar vida y variable de vivo
        isAlive = true;
        currentHealth = startingHealth;
        //estos son para desactiver los collider del mazo y enemigo y no me quiten daño al morir el enemigo
        weaponCollider=GetComponentInChildren<BoxCollider>();
        enemyCollider = GetComponent<CapsuleCollider>();
        //sonido
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //inicializar timer
        //si matamos al enemigo, desplaza el cuerpo hacia abajo hasta que desaparezca
        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up*dissapearSpeed*Time.deltaTime);
        }
    }

    //funcion de cuando me chocan con el collider del enemigo
    void OnTriggerEnter(Collider other)
    {
        //si ha pasado almenos el tiempo entre golpes y el juego no esta perdido
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //si el que me golpea lleva este tag
            if(other.tag == "PlayerWaeapon")
            {
                //reinicia timer y quita vida
                TakeHit();
                timer = 0f;
            }
        }
    }

    //iuncion para quitar vida
    void TakeHit()
    {
        //si el enemigo tiene vida aun
        if (currentHealth > 0)
        {
            //activa la animacion de herido y quitale vida
            anim.Play("Enemy_hurt");
            currentHealth -= 10;
            audio.PlayOneShot(golpeClip);
        }
        //si la vida es 0 o menos, pongo que no esta vivo y mato al enemigo
        if (currentHealth<=0)
        {
            isAlive=false;
            KillEnemy();
            audio.PlayOneShot(deadClip);
        }
    }

    //funcion para eliminar enemigo
    void KillEnemy()
    {
        //desactivo el collider y la ia, y pongo la animacion de enemigo muerto, con su cuerpo sin que le afecte nada
        capsuleCollider.enabled=false;
        nav.enabled=false;
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        weaponCollider.enabled=false;
        enemyCollider.enabled=false;
        StartCoroutine(RemoveEnemy());
    }

    //funcion que elimine el enemigo de la escena
    IEnumerator RemoveEnemy()
    {
        //a los 2f pon que el enemigo desaparezca y al 1f destruye le objeto
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


}
