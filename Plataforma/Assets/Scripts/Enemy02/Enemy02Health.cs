using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy02Health : MonoBehaviour
{

    [SerializeField] private int startingHealth = 20; //vida inicial
    [SerializeField] private float timeSinceLastHit = 0.5f; //tiempo desde el ultimo golpe
    [SerializeField] private float disapearSpeed = 2f;  //tiempo que tarda en desaparecer
    [SerializeField] private int curretHealth; //vida actual

    private float timer = 0f;   //contador para recibir daño o desaparecer
    private Animator anim;  // animaciones
    private bool isAlive;   //para saber si estoy vivo o no
    private new Rigidbody rigidbody;  // para desplazar el cuerpo hacia abajo
    private CapsuleCollider capsuleCollider; //para desactivarlo
    private bool disapearEnemy = false; //para saber si ha desaparecido
    private new AudioSource audio; //audio
    public AudioClip hurtClip;
    public AudioClip dieClip;

    // Start is called before the first frame update
    void Start()
    {
        //obtengo todos los elementos
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        isAlive = true;
        curretHealth = startingHealth;
        audio=GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //actualiza el timer
        if (disapearEnemy)
        {
            transform.Translate(-Vector3.up*disapearSpeed*Time.deltaTime);
        }
    }

    //cuando el enemigo 02 choca con algo
    void OnTriggerEnter( Collider other)
    {
        //si no estoy en gameover y ha transcurrido el tiempo necesario desde el ultimo golpe
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //y ademas me choco contra el arma del jugador
            if(other.tag == "PlayerWaeapon")
            {
                //quita daño y resetea
                TakeHit();
                timer = 0f;
            }
        }
    }

    //quitar daño y animaciones
    void TakeHit()
    {
        //si tengo vida suficiente
        if (curretHealth > 0)
        {
            //pongo animacion de herido me quito vida y reproduzco sonido
            anim.Play("Enemy02_Hurt");
            curretHealth -= 10;
            audio.PlayOneShot(hurtClip);
        }
        //si me mata
        if(curretHealth <= 0)
        {
            //variable de vivo a falso y matar enemigo
            isAlive = false;
            KillEnemy();
        }
    }

    //funcion de matar enemigo
    void KillEnemy()
    {
        //desactivo los colliders, la cinematica, animacion de muerto y sonido
        capsuleCollider.enabled = false;
        anim.SetTrigger("Enemy_Die");
        rigidbody.isKinematic = true;
        audio.PlayOneShot(dieClip);

        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        disapearEnemy=true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
