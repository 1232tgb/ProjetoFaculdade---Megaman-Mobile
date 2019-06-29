using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUp : MonoBehaviour
{
    public float velocidade;
    public bool inverte;
    public Transform posicaoMaxima, posicaoMinima;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, velocidade, 0);

        if (!inverte)
        {
            transform.position += move * Time.deltaTime;

            if (transform.position.y >= posicaoMaxima.position.y)
            {
                transform.position = posicaoMinima.position;
            }
        }
        else
        {
            transform.position -= move * Time.deltaTime;

            if (transform.position.y <= posicaoMinima.position.y)
            {
                transform.position = posicaoMaxima.position;
            }
        }



    }
}
