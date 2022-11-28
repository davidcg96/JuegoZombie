using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDestruction : MonoBehaviour
{

    public float lifeSpan = 2.0f; //duracion del arma en el juego
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //cuando choco on algo destruye el arma
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
