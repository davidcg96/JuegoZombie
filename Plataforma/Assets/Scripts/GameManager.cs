using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //varaible qur obtiene referencia al gamemanager
    public static GameManager instance = null;
    [SerializeField] private GameObject player; //este el esl objeto jugador
    
    //variable privada, que indica si hemos perdido o no
    private bool gameOver=false; 
    
    //como la referencia al player es privada, usamos esto para poder obtenerlo
    public GameObject Player { get { return player; } }
    //como la referencia es privada al game over, usamos esto para obtenerlo
    public bool GameOver { get { return gameOver; } }

    void Awake()
    {
        //si no hay ningun gamemanager asignado, le asigno este objeto que contiene el script
        if(instance == null) { instance = this; }
        else if (instance != null) { Destroy(gameObject); } //si ya hay un gamemanager lo elimino
        DontDestroyOnLoad(gameObject); //no me destruyas en los cambios de escenas el manager
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // funcion que comprueba si hemos perdido o no, cuando se nos acaba la vida
    public void PlayerHit(int currentHP)
    {
        if (currentHP>0)
        {
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }
    }
}
