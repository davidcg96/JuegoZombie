using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int startingHealth = 100; //variable con la vida inicial
    [SerializeField] float timeSinceLastHit = 2.0f; //cariable con tiempo desde el ultimo hit, para que no me quiten vida hasta que pase este tiempo
    [SerializeField] int currentHealth;
    [SerializeField] private float timer = 0f;  //timer que cuenta para saber si me pueden quitar vida
    private Animator anim; //animator para animar
    private CharacterMovement characterMovement; //variable para obtener el script characterMovement y desactivarlo cuando muera
    [SerializeField] private Slider healthSlider;             //slider de la vida

    //sonidos 
    private new AudioSource audio; //fuente de adio
    public AudioClip hurtAudio; //audio para cuando me dañan al jugador
    public AudioClip deadAudio; //audio cuando muere el jugador

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth; //igualo la vida ctual con la inicial
        characterMovement = GetComponent<CharacterMovement>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
    }

    //cuando algo golpea al jugador o atraviesa
    void OnTriggerEnter(Collider other)
    {
        //si ha pasado el suficiente tiempo y no estoy eliminado
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            //y ademas me golpea un objeto con la etiqueta weapon
            if (other.tag == "Weapon")
            {
                //recibe golpe y resetea contador
                TakeHit();
                timer= 0f;
            }
        }
    }

    //funcion de recibir golpe
    void TakeHit()
    {
        //si aun estoy con vida
        if (currentHealth>0)
        {
            //llamo a la funcion golpear player
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Player_hurt"); //activo animacion de golpeado
            currentHealth -= 10; //me quito 10 puntos de vida
            healthSlider.value = currentHealth; //pongo la barra de la ui para que se mueva con la vida actual
            audio.PlayOneShot(hurtAudio);
        }
        //si me muero
        if (currentHealth <= 0)
        {
            //activo animacion de morir y desactivo el script charactermovement
            GameManager.instance.PlayerHit(currentHealth);
            anim.SetTrigger("isDead");
            characterMovement.enabled= false;
            audio.PlayOneShot(deadAudio);
        }
    }
}
