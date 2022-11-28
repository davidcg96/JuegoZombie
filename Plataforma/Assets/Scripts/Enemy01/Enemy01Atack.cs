using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Atack : MonoBehaviour
{
    //Dos variables, que contienen la distancia que llega el mazo al atacar y el tiempo entre ataque del enemigo
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAtack = 1f;

    //necesitamos referencia al animator (animacion de ataque), referencia al player (posicion etc), variable para saber si estoy a suficiente distancia
    //para atacar, y referencia al collider del mazo para los eventos de istrigger
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider weaponCollider;
    private Enemy01Health enemy01health;

    //sonidos
   // private new AudioSource audio;
   // public AudioClip ataqueClip;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player= GameManager.instance.Player; //en lugar de pasar al jugador lo obtenemos de esta manera desde gamemanager
        weaponCollider= GetComponentInChildren<BoxCollider>(); //el collider del mazo esta dentro de muchas piezas del enemigo
        StartCoroutine(Attack());
        enemy01health= GetComponent<Enemy01Health>();
      //  audio= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //si la distancia es menor que el rango y esta vivo el enemigo
        if (Vector3.Distance(transform.position,player.transform.position)<range && enemy01health.IsAlive)
        {
            playerInRange=true;
        }
        else
        {
            playerInRange=false;
        }
    }

    //Como tenemos una cadencia de ataque cada 1f, tenemos que llamar a la funcion atacar cada timeBetweenAtack, para eso necesitamos un ienumerator y corutina
    IEnumerator Attack()
    {
        //si estoy en rango de atacar, y no he perdido el juego
        if(playerInRange && !GameManager.instance.GameOver)
        {
            //activo el estado de atacar animacion
            anim.Play("Enemy_attack");
            //espera este tiempo de ataque
            yield return new WaitForSeconds(timeBetweenAtack);
            
        }
        yield return null;
        //pasado el tiempo de espera, vuelvo a llamar a esta funcion
        StartCoroutine(Attack());
    }
}
