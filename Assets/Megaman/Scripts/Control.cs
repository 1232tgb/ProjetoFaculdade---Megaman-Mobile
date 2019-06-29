using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    [SerializeField]
    ClasseBase hero;

    ZeroFire zerofire;

    float input;
    bool podePular, fire, charging;

    void Start()
    {
        hero = GetComponent<ClasseBase>();
        zerofire = GetComponent<ZeroFire>();
    }


    void Update()
    {
        if (hero.GetType() == typeof(ZeroControl))
        {

            hero.SetFire(fire);
            zerofire.setCharging(charging);
            //hero.attacking = Input.GetMouseButtonUp(0);
            hero.swordAttacking = Input.GetMouseButtonUp(1);
        }

        hero.SetMove(input);

        if (podePular)
        {
            hero.SetJump();
        }

        //hero.inputHorizontal = Input.GetAxisRaw("Horizontal");
        //if (Input.GetButton("Jump")) { hero.SetJump(); }



        //spriteControl.SetVelocity(Input.GetAxis("Horizontal"));
    }

    public void MoveRight()
    {
        input = 1;
    }

    public void MoveLeft()
    {
        input = -1;
    }

    public void MoveNeutral()
    {
        input = 0;
    }

    public void Jumping()
    {
        podePular = true;
    }

    public void NotJumping()
    {
        podePular = false;
    }

    public void setFireFalse()
    {
        fire = false;
        //charging = true;
    }

    public void Release()
    {
        fire = true;
        Invoke("setFireFalse", 0.01f);
    }

    public void Charge()
    {
        charging = true;
    }

    public void DontCharge()
    {
        charging = false;
    }
}
