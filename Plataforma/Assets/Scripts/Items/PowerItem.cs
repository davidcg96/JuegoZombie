using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour
{

    private GameObject player;      //obtengo referencia al jugador
    private new AudioSource audio;      //referencia a la fuente de audio
    private new ParticleSystem particleSystem; //referencia al efecto de particulas
    private PlayerHealth playerHealt;     //referencia al script 

    private MeshRenderer meshRenderer;
    private ParticleSystem brainParticle;

    public bool moduleEnable;

    public GameObject pickUpEffect; //efecto de explosion
    private ItemExplode itemExplode; //ferencia al script
    private SphereCollider spherecollider;


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;               //jugador
        playerHealt= player.GetComponent<PlayerHealth>(); //script
        playerHealt.enabled = true;                       //lo ponemos habilitado

        particleSystem= player.GetComponent<ParticleSystem>(); //particula del player
       

        meshRenderer= GetComponentInChildren<MeshRenderer>();
        brainParticle= GetComponent<ParticleSystem>();

        itemExplode= GetComponent<ItemExplode>();
        spherecollider= GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StartCoroutine(InvencibleRoutine()); //cuando choque con el item, llamo a la funcion invencible
            meshRenderer.enabled = false; //aunque el objeto se destruye cuando pasan 10 segundos de ser invencible, le quito aqui la malla para que no se vea
        }
    }

    //funcion que hace de duracion del efecto invencible
    public IEnumerator InvencibleRoutine()
    {
        itemExplode.PickUp(); //EFECTO DE EXPLOSION
        spherecollider.enabled = false;
        //activo la emision del jugador
        var emmision=particleSystem.emission;
        emmision.enabled = !moduleEnable;
        //desactivo el script de vida para que no me quite
        playerHealt.enabled = false;
        //desactivo la emision del item
        var brainEmision = brainParticle.emission;
        brainEmision.enabled = moduleEnable;

        playerHealt.Timer=0; ; //resetea tiemr para el bug de que no haga daño el primer gople 

        yield return new WaitForSeconds(10f); //espera 10 segundos
        //corta emision jugaor
        emmision.enabled = moduleEnable;
        //vuelve script ed vida
        playerHealt.enabled = true;
        //destruye item
        Destroy(gameObject);
    }


    void PickUp()
    {
        Instantiate(pickUpEffect, transform.position,transform.rotation);
    }
}
