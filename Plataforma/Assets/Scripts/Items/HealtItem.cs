using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtItem : MonoBehaviour
{

    private GameObject player; //referencia al jugador
    private PlayerHealth playerHealt; //referencia al scripo de salud


    // Start is called before the first frame update
    void Start()
    {
        player= GameManager.instance.Player;
        playerHealt= player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //si el item choca con el jugador
        if (other.gameObject == player)
        {
            //llamo a la funcion de subir vida y destruyo el objeto
            playerHealt.PowerUpHealth();
            Destroy(gameObject);
        }
    }
}
