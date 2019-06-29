using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControl : ClasseBase
{


    [SerializeField]
    AudioSource aud;

    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    int ringCount = 0;

    bool audioonce = false;
    bool cooldown;
    public GameObject ringPref;


    void Start()
    {
        SRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        IdleIndex = 0;
        MoveIndex = 0;
        JumpIndex = 0;
        velocidade = 10;
        cooldown = false;

        IdleArray = SpriteManager.CarregaSprite("Sonic/Sonic Idle");
        WalkArray = SpriteManager.CarregaSprite("Sonic/Sonic Walk");
        RunArray = SpriteManager.CarregaSprite("Sonic/Sonic Run");
        JumpArray = SpriteManager.CarregaSprite("Sonic/Sonic Jump");
        aud = GetComponent<AudioSource>();
        MainGame.instance.SetItems(ringCount);
    }

    void FixedUpdate()
    {

        //rigidBody.AddForce(new Vector2(inputHorizontal, 0) * velocidade);
        _rigidBody.AddRelativeForce(new Vector2(velocidade, 0) * inputHorizontal);

        if (jump)
        {
            _rigidBody.AddForce(new Vector2(0, jumpForce));//adiciona força
            jumpForce = Mathf.Lerp(jumpForce, 0, 0.5f);



        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up);
        transform.up = hit.normal;


    }

    void Update()
    {
        //inputHorizontal = Input.GetAxis("Horizontal");



        Flip();

        #region Movimentacao
        if (!jump)
        {
            _rigidBody.gravityScale = 1;
            if (MovePhysics > 0)
            {
                MoveAnim();
                _rigidBody.drag = 0;

                float dif = _rigidBody.velocity.normalized.x - velocidade * transform.up.y;
                if (Mathf.Abs(dif) > 0)
                {
                    //break animation
                }
            }
            else
            {
                IdleAnim();
                _rigidBody.drag = 3;
            }
        }
        else
        {
            if (!audioonce)
            {
                audioonce = true;
                aud.Play();
            }

            JumpAnim();
            _rigidBody.gravityScale = 5;
        }
        #endregion

    }

    private void JumpAnim()
    {


        JumpIndex += Time.deltaTime * 20;
        if (JumpIndex >= JumpArray.Length)
        {
            JumpIndex = 0;

        }

        SRenderer.sprite = JumpArray[(int)JumpIndex];
        MoveIndex = 0;
        IdleIndex = 0;



    }


    void Flip()
    {
        //SRenderer.flipX = Velocity > 0 ? true : SRenderer.flipX;
        //SRenderer.flipX = Velocity < 0 ? false : SRenderer.flipX;
        if (inputHorizontal > 0) SRenderer.flipX = false;
        if (inputHorizontal < 0) SRenderer.flipX = true;
    }



    void MoveAnim()
    {

        if (MovePhysics < .5f && MovePhysics >= 0.01f)
        {
            MoveArray(WalkArray);
        }
        else if (MovePhysics >= .5f)
        {
            MoveArray(RunArray);
        }

        #region Comentado

        //MoveIndex += velocidadeFisica;//atual
        //MoveIndex += Mathf.Abs(rigidBody.velocity.x * 0.05f);
        //if (MoveIndex >= WalkArray.Length)
        //{
        //    MoveIndex = 0;
        //}
        //SRenderer.sprite = WalkArray[(int)MoveIndex];
        //boringIndex = 0;
        #endregion

    }

    void MoveArray(Sprite[] moveArray)
    {
        //MoveIndex += Mathf.Abs(rigidBody.velocity.x * 0.05f);
        MoveIndex += MovePhysics;
        if (MoveIndex >= moveArray.Length)
        {
            MoveIndex = 0;
        }

        SRenderer.sprite = moveArray[(int)MoveIndex];
        IdleIndex = 0;
    }

    void IdleAnim()
    {
        MoveIndex = 0;

        IdleIndex += Time.deltaTime;

        if (IdleIndex > IdleArray.Length)
        {
            IdleIndex = 0;
        }

        SRenderer.sprite = IdleArray[(int)IdleIndex];
    }

    #region comentado
    //public void SetVelocity(float vel)
    //{
    //    inputHorizontal = vel;
    //}
    #endregion

    public override void SetJump()
    {
        if (!jump)
        {
            jumpForce = 500;
            jump = true;
        }

    }

    public void InverteSentido()
    {
        velocidade *= -1;
    }


    float MovePhysics
    {
        get { return Mathf.Abs(_rigidBody.velocity.x * 0.05f); }
        //get { return Mathf.Abs(rigidBody.velocity.x * (inputHorizontal / quebraVelocidade)); }

    }

    //float JumpPhysics 
    //{
    //    get { return Mathf.Abs(rigidBody.velocity.y); }
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Chao"))
            //if (!col.gameObject.GetComponent<Collider2D>().isTrigger)
        {

            jump = false;
        }
        if (col.gameObject.CompareTag("SpringVertical"))
        {
            // SetJump();
            JumpAnim();
        }

        if (col.gameObject.CompareTag("Spike")) 
        {
            print("caiu nos spikes");
            cooldown = true;
            Invoke("coolDown", 1);
            _rigidBody.AddForce(col.relativeVelocity.normalized * -300);
            aud.PlayOneShot(clips[2]);
            for (int i = 0; i < ringCount; i++) 
            {
                GameObject ringInstance = (GameObject)Instantiate(ringPref, transform.position, transform.rotation);
                Rigidbody2D rgB2d = ringInstance.GetComponent<Rigidbody2D>();
                rgB2d.AddForce(Vector3.up * 500);
            }

            ringCount = 0;
            MainGame.instance.SetItems(ringCount);
        }


        if (audioonce)
        {
            audioonce = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Ring")) 
        {
            aud.PlayOneShot(clips[0]);//faz o som
            Destroy(col.gameObject);
            ringCount++;
            MainGame.instance.SetItems(ringCount);
        }
    
    }

    void coolDown() 
    {
        cooldown = false;
    }


}
