using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    
    [SerializeField] private float range = 10f; //rango para atacar o perseguir
    [SerializeField] private float timeBetweenAttacks = 1f; //tiempo entre ataque
    private Animator anim; //animaciones
    private GameObject player; //referencia la player 
    private bool playerInRange; //para saber si esta en rango


    public float arrowSpeed = 600f; //velocidad flecha
    public Transform arrowSpawn;   //posicion donde sale la flecha
    public Rigidbody arrowPrefab; //prefab de la flecha

    Rigidbody clone;                //la instancia del nuevo arna

    // Start is called before the first frame update
    void Start()
    {
        arrowSpawn = GameObject.Find("ArrowSpawn").transform; //busco directamente el prefab
        anim = GetComponent<Animator>();
        player = GameManager.instance.Player; //obtengo la referencia del jugador desde gameManager
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position)< range)
        {
            playerInRange= true;
            anim.SetTrigger("isAtacking");
        }
        else
        {
            playerInRange = false;
        }

    }


    public void FireArcherProyectil()
    {
        clone = Instantiate(arrowPrefab,arrowSpawn.position,arrowSpawn.rotation) as Rigidbody;
        clone.AddForce(-arrowSpawn.transform.right*arrowSpeed);
    }
}
