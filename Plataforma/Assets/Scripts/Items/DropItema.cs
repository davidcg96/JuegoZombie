using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItema : MonoBehaviour
{

    public GameObject[] items; //items para arrojar
    int randomInt; //variable con valor aleatorio para arrojar items
   
    public void Drop()
    {
        randomInt=Random.Range(0,items.Length); //tomo un valor al azar de entre el tamaño de items posibles 
        Instantiate(items[randomInt], transform.position, Quaternion.identity); //creo un nuevo objeto 
    }
}
