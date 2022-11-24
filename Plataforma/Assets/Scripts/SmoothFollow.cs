using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    //La distancia que dejo de margen entre la cámara y el target para empezar a moverse
    public float xMargin = 1.0f;
    public float yMargin = 1.0f;

    //la cantidad que avanza la camara
    public float xSmooth = 10.0f;
    public float ySmooth = 10.0f;

    //estos vectores me indican el limite maximo y minimo que recorre la cámara en X y en Y, es edcir si el limite es 30m y avanzo 30m la camara se detiene ahi
    public Vector2 maxXandY;
    public Vector2 minXandY;

    public Transform cameraTarget;

    private void Awake()
    {
        //obtengo el target de la camara, que esta un poco adelantado al personaje
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").transform;
    }

    private void FixedUpdate()
    {
        //llamo a la funcion seguir jugador
        TrackPlayer();
    }

    //compruebo que no he excedido el margen entre la camara y su target
    bool CheckXmargin()
    {
        return Mathf.Abs(transform.position.x-cameraTarget.position.x)>xMargin;
    }
    bool CheckYmargin()
    {
        return Mathf.Abs(transform.position.y - cameraTarget.position.y) > yMargin;
    }

    //Funcion que sigue al jugador, si se para el jugador , se para la cámara pero avanza un poco para estar por denlante del jugador.
    //Un posible error, cuando crezca el escenario, son los limites maximos y minimos, no se sabe cuanto va a medir el escenario
    void TrackPlayer()
    {
        //cojo dos variables que contienen la posicion de la camara
        float targetX=transform.position.x;
        float targetY=transform.position.y;
        //compruebo si me he pasado del margen, es decir avanzo el personaje con el teclado, el player, lleva dentro el target de la camara, este avanza tmb
        //y si me alejo en la distancia con el target pues la cámara sige al personaje
        if (CheckXmargin())
        {
            //voy a avanzar la camara de la pos inicial a inicial+xSmooth^deltaTime, pero lo hago con transicion sueve (lerp)
            targetX = Mathf.Lerp(transform.position.x, cameraTarget.position.x, xSmooth * Time.deltaTime);
        }
        if (CheckYmargin())
        {
            //voy a avanzar la camara de la pos inicial a inicial+xSmooth^deltaTime, pero lo hago con transicion sueve (lerp)
            targetY = Mathf.Lerp(transform.position.y, cameraTarget.position.y, ySmooth * Time.deltaTime);
        }
        //actualizo la posicion para que esté dentro del max y min
        targetX = Mathf.Clamp(targetX,minXandY.x,maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);
        //le doy la posicion
        transform.position= new Vector3(targetX,targetY,transform.position.z);
    }
}
