using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public abstract class BulletBaseClass : MonoBehaviour
{
    protected Rigidbody2D _myRigidBody;
    protected BoxCollider2D _myBoxCollider;
    protected SpriteRenderer _mySpriteRenderer;
    protected float animaIndex;
    public int dano, velocidade, velocidadeTrocaSprite;
    public bool colidiu;
    public float contador, contadorMax;

    protected virtual void setComponents()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myBoxCollider = GetComponent<BoxCollider2D>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();

    }
    protected virtual void AnimaSprite(int SpeedChangeSprite) { }
    public abstract void Navega();
    public abstract void RetiraDaTela();
    public abstract void RodaContador();
}
