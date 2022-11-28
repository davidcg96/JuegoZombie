using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy01Move : MonoBehaviour
{

    [SerializeField] private Transform player; //posicion del player para perseguirlo o no
    private NavMeshAgent nav; //variable el nav mesh del enemigo para controlarlo
    private Animator anim;  //variable el animator para animarlo
    private Enemy01Health enemy01health; //accedo al scrip de vida para descativar el ia y no de errores
    // Start is called before the first frame update
    void Start()
    {
        //obtenemos animator y nav mesh
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemy01health = GetComponent<Enemy01Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //si la distancia entre el player y el enemigo es menor que 12
        if (Vector3.Distance(player.position,this.transform.position)<12)
        {
            //si no he perdido y ademas el enemigo sigue vivo
            if(!GameManager.instance.GameOver && enemy01health.IsAlive)
            {
                nav.SetDestination(player.position); //persigo al jugador
                anim.SetBool("isWalk", true);        //pongo animacion de andar
                anim.SetBool("isIddle", false);   //quito la animacion de iddle
            }
           
        } //si he perdido o el enemigo esta muerto
        else if (GameManager.instance.GameOver || !enemy01health.IsAlive)
        {
            //pongo la animacion de iddle y desactivo la IA
            anim.SetBool("isWalk", false);
            anim.SetBool("isIddle", true);
            nav.enabled = false;
        }
    }
}
