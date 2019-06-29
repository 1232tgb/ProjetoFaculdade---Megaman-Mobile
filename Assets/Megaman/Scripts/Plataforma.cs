using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{

    Rigidbody2D _rigidBody;
    float contador;
    bool colidiu;
    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
        _rigidBody.mass = 200;
        contador = 0;
        colidiu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (colidiu)
            StartCounting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ClasseBase>())
        {
            colidiu = true;
        }
    }

    private void StartCounting()
    {

        if (contador < 1f)
        {
            contador+=Time.deltaTime;
        }
        else
        {
            //print(contador);
            contador = 0;
            _rigidBody.gravityScale = 5;
        }



    }
}
