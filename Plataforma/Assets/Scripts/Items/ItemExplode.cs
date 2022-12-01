using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplode : MonoBehaviour
{

    public GameObject pickUpEffect;   //efecto de explosion, prefab de examples

    //funcion de capturar el objeto
    public void PickUp()
    {
        //creo una nueva instancia del prefab de efecto de explosion
        GameObject newPickUpEffect = (GameObject)Instantiate(pickUpEffect,transform.position,transform.rotation);
        Destroy(newPickUpEffect,1); //destruyelo al segundo
    }
  
}
