using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class ClasseBase : MonoBehaviour
{
    public SpriteRenderer SRenderer;
    protected Rigidbody2D _rigidBody;



    [SerializeField]
    protected Sprite[] IdleArray, WalkArray, RunArray, JumpArray;
    protected float IdleIndex, MoveIndex, JumpIndex, AimIndex, SwordIndex;
    public float inputHorizontal;//input
    public int velocidade;
    public float jumpForce, jumpForceMax;
    public bool jump, swordAttacking, attacking;
    [SerializeField]
    protected int quebraVelocidade = 20;

    public virtual void SetJump()
    {

    }

    public virtual void SetMove(float move)
    {

    }

    public virtual void SetFire(bool charge)
    {

    }
}
