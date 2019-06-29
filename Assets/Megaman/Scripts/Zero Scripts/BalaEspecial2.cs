using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEspecial2 : BulletBaseClass
{
    [SerializeField]
    Sprite[] SpritesAnimadas;


    void Awake()
    {
        animaIndex = 0;
        SpritesAnimadas = SpriteManager.CarregaSprite("Zero/Projeteis/tiro especial 2");
        dano = 7;
        setComponents();
        colidiu = false;
    }

    void Update()
    {
        Navega();
        RodaContador();

        AnimaSprite(velocidadeTrocaSprite);
        if (colidiu || contador >= contadorMax)
        {
            RetiraDaTela();
        }
    }

    public override void Navega()
    {
        //_myRigidBody.AddForce(transform.right * velocidade);

        transform.position += transform.right * velocidade / 60;

    }

    public override void RetiraDaTela()
    {
        Destroy(gameObject);
    }

    protected override void setComponents()
    {
        base.setComponents();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.isTrigger && !col.gameObject.CompareTag("Player"))
        {
            colidiu = true;
        }
    }


    public override void RodaContador()
    {
        if (contador < contadorMax)
        {
            contador += Time.deltaTime;
        }
    }



    protected override void AnimaSprite(int SpeedChangeSprite)
    {
        float addValue = Time.deltaTime * SpeedChangeSprite;
        animaIndex += addValue;
        animaIndex = animaIndex > SpritesAnimadas.Length ? 1 : animaIndex;

        _mySpriteRenderer.sprite = SpritesAnimadas[(int)animaIndex];
    }
}
