using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //variables de movimiento
    public float maxSpeed = 6.6f; //velocidad del jugador
    public bool facingRigth= true; //para saber si el jugador va hacia delante o atras
    public float moveDirection;

    //variables de salto
    public float jumpSpeed = 300.0f; //fuerza de salto
    public bool isGrounded = false; //verifica que estoy en el suelo
    public Transform groundCheck; //posicion del suelo
    public float groundRadius = 0.2f; //radio de la esfera para chequear el suelo
    public LayerMask whatIsGrounded; //capa suelo

    //variables de ataque de armas
    public float knifeSpeed = 600.0f; //velocidad de ataque
    public Transform knifeSpawn;  // posicion donde inicia el cuchillo
    public Rigidbody rbKnife;     // Es el prefab del cuchillo 
    Rigidbody clone;                //la instancia del nuevo cuchillo

    private Rigidbody rb;   //rigidbody del jugador para moverse o añadir fuerzas de salto etc

    private Animator anim; //variable de animator para controlar el cambio de estado de iddle a run etc



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //obtengo el rigisbody del objeto que tiene este script
        anim = GetComponent<Animator>(); //obtengo el animator del personaje
        groundCheck = GameObject.Find("GroundCheck").transform; //obtengo el empty del suelo, podria haberlo pasado en unity y no ponerlo aqui
    }

    // Update is called once per frame
    void Update()
    {
        //obtengo las teclas a,d del teclado para moverme, que es un vector tal que así para la d (1,0) y (-1,0) para la a
        moveDirection = Input.GetAxis("Horizontal");
        //cuando pulse el spacio, hago que salte, aplicando una fuerza al rigidbody en el eje y
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) )
        {
            anim.SetTrigger("isJumping");
            rb.AddForce(new Vector2(0, jumpSpeed));
        }
    }

    void FixedUpdate()
    {
        //multiplico la velocidad del jugador hacia la dirección que quiero, derecha o izquierda, y la mantengo en el eje y. Le aplica una fuerza al jugador
        rb.velocity = new Vector2(moveDirection*maxSpeed, rb.velocity.y);
        // crea una esfera, en la posicion de chequear suelo, con radio 0.2, en la capa siguiente
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,whatIsGrounded);

        //aplico la rotacion, si me cambio
        if (moveDirection>0.0f && !facingRigth)
        {
            Flip();
        }
        else if(moveDirection<0.0 && facingRigth)
        {
            Flip();
        }
        anim.SetFloat("speed",Mathf.Abs(moveDirection));//cambia de estado de animación

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    // Funcion para cuando pulsemos a o s para rotar al personaje y que mire hacia esa direccion
    void Flip()
    {
        //cambio de lado
        facingRigth = !facingRigth;
        //rotame al jugador en el eje y, 180 grados, desde posicion del mundo
        transform.Rotate(Vector3.up,180.0f,Space.World);
    }

    //funcion de ataque
    void Attack()
    {
        //pongo la transicion de animacion a ataque
        anim.SetTrigger("Attacking");
       
    }

    public void CallFireProjectil()
    {
        //creo el arma, le paso prefab del cuchillo, la posicion y rotacion, le digo que sea rigidbody y no objeto
        clone = Instantiate(rbKnife, knifeSpawn.position, knifeSpawn.rotation) as Rigidbody;
        //le añado fuerza para que vaya hacia delante con esta velocidad
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
    }
}
