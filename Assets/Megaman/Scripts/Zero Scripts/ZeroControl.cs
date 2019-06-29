using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


enum EstadoDeLocomocao { idle, andando, pulando, caindo, atirando, batendo }
enum TipoDeTiro { tiro_Comum, tiro_Especial_1, tiro_Especial_2 }
public class ZeroControl : ClasseBase
{
    //continuar as animções do personagens quando esta  em estado de charged
    EstadoDeLocomocao locomocao;
    TipoDeTiro tiro;
    public Sprite[] FallingArray, DashArray, AimingStandingArray, AimingWalkingArray, AimingJumpingArray, AimingFallingArray, SwordStandingArray, SwordJumpingArray;
    Sprite[] ChargingStanding, ChargingWalking, ChargingJumping, ChargingFalling;
    public AudioSource aud;
    public int SpeedChangeIdle, speedChangeWalk, speedChangeJump, speedChangeAimIdle, speedChangeAimWalking, speedChangeOnAir;
    public bool onGround;
    public float valorDirecao;
    ZeroFire zeroFire;
    [HideInInspector]
    public int HealthValue;
   
    Transform posicaoInicial;
    Transform posicaoQueda;

    int lifeCount, itemCount;


    float contadorTiroAnimacao;
    public float contadorTiroAnimacaoLimite;

    void Awake()
    {

        SetSprites();
        SRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
        zeroFire = GetComponent<ZeroFire>();
        posicaoInicial = GameObject.Find("PosicaoInicial").GetComponent<Transform>();
        //audSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        locomocao = EstadoDeLocomocao.idle;
        tiro = TipoDeTiro.tiro_Comum;
        IdleIndex = 0;
        MoveIndex = 0;
        JumpIndex = 0;
        AimIndex = 0;
        SwordIndex = 0;
        jumpForce = 0;
        speedChangeAimIdle = SpeedChangeIdle;
        speedChangeAimWalking = speedChangeWalk;
        speedChangeOnAir = speedChangeJump;
        lifeCount = 3;
        posicaoQueda = GameObject.Find("posicao de queda").transform;
        transform.position = posicaoInicial.position;
        HealthValue = 4;
        contadorTiroAnimacao = contadorTiroAnimacaoLimite;
    }

    int limiteMaximoVelocidadeQueda = -12;
    void FixedUpdate()
    {

        if (jump)
        {
            _rigidBody.AddForce(new Vector3(0, jumpForce, 0));
            jumpForce = Mathf.Lerp(jumpForce, 0, 0.5f);
        }

        if (!jump && !onGround)
        {
            if (_rigidBody.velocity.y < limiteMaximoVelocidadeQueda)
            {
                Vector3 novaVelocidade = _rigidBody.velocity;
                novaVelocidade.y = limiteMaximoVelocidadeQueda;
                _rigidBody.velocity = novaVelocidade;
            }
        }

    }

    void Update()
    {
        onGround = CheckGround();

        Move();
        flipSprite();

        if (onGround)
        {
            jump = false;

            if (contadorTiroAnimacao >= contadorTiroAnimacaoLimite)
            {
                if (!swordAttacking)
                {
                    if (Horizontal == 0)
                    {
                        IdleAnimation(SpeedChangeIdle);
                    }
                    else
                    {
                        WalkingAnimation(speedChangeWalk);
                    }
                }
                else
                {
                    StartCoroutine(SwordAttack());

                }

            }
            else
            {

                if (Horizontal == 0)
                {
                    NewAimAnimation(speedChangeAimIdle, AimingStandingArray);
                }
                else
                {
                    NewAimAnimation(speedChangeAimWalking, AimingWalkingArray);
                }



            }


            _rigidBody.gravityScale = 1;

        }
        else
        {
            if (jump)
            {
                if (contadorTiroAnimacao >= contadorTiroAnimacaoLimite)
                {

                    if (_rigidBody.velocity.y > 0)
                    {
                        JumpAnimation(speedChangeJump, JumpArray);
                    }
                    else if (_rigidBody.velocity.y < 0)
                    {
                        JumpAnimation(speedChangeJump, FallingArray);
                    }
                }
                else
                {
                    if (_rigidBody.velocity.y > 0)
                    {
                        JumpAnimation(speedChangeJump, AimingJumpingArray);
                    }
                    else if (_rigidBody.velocity.y < 0)
                    {
                        JumpAnimation(speedChangeJump, AimingFallingArray);
                    }
                }

                _rigidBody.gravityScale = 5;
            }
            else
            {
                if (contadorTiroAnimacao >= contadorTiroAnimacaoLimite)
                {
                    JumpAnimation(speedChangeJump, FallingArray);
                }
                else
                {
                    JumpAnimation(speedChangeJump, AimingFallingArray);
                }

                _rigidBody.gravityScale = 5;
            }
        }

        MainGame.instance.SetLives(lifeCount);
        MainGame.instance.SetItems(itemCount);

        if (HealthValue <= 0)
        {
            lifeCount--;
        }

        retornaAoInicio();

        ContadorDoTiro();

    }

    void ContadorDoTiro()
    {

        if (attacking)
        {
            contadorTiroAnimacao = 0;
        }


        if (contadorTiroAnimacao >= contadorTiroAnimacaoLimite)
        {
            contadorTiroAnimacao = contadorTiroAnimacaoLimite;
        }
        else
        {
            contadorTiroAnimacao += Time.deltaTime * 1.5f;
        }
    }

    void Move()
    {
        Vector3 Locomotion = new Vector3(Horizontal, 0, 0) / quebraVelocidade;
        transform.position += Locomotion;

    }
    #region Animacoes concluidas

    void IdleAnimation(int SpriteSpeedChange)
    {
        MoveIndex = 0;
        JumpIndex = 0;
        AimIndex = 0;
        //SwordIndex = 0;
        float AddValue = Time.deltaTime * SpriteSpeedChange;
        IdleIndex += AddValue;

        IdleIndex = IdleIndex > IdleArray.Length ? 0 : IdleIndex;

        SRenderer.sprite = IdleArray[(int)IdleIndex];
    }

    void WalkingAnimation(int SpriteSpeedChange)
    {
        IdleIndex = 0;
        JumpIndex = 0;
        AimIndex = 0;
        //SwordIndex = 0;
        float AddValue = Mathf.Abs(Time.deltaTime * SpriteSpeedChange * Horizontal);
        MoveIndex += AddValue;

        MoveIndex = MoveIndex > WalkArray.Length ? 0 : MoveIndex;

        SRenderer.sprite = WalkArray[(int)MoveIndex];

    }

    void JumpAnimation(int SpriteSpeedChange, Sprite[] array)
    {
        IdleIndex = 0;
        MoveIndex = 0;
        AimIndex = 0;
        //SwordIndex = 0;
        float AddValue = Time.deltaTime * SpriteSpeedChange;
        JumpIndex += AddValue;

        JumpIndex = JumpIndex > array.Length ? 0 : JumpIndex;

        SRenderer.sprite = array[(int)JumpIndex];
    }

    void AimAnimation(int SpriteSpeedChange, Sprite[] notChargedArray, Sprite[] chargedArray)
    {
        IdleIndex = 0;
        MoveIndex = 0;
        JumpIndex = 0;
        //SwordIndex = 0;
        float AddValue = Time.deltaTime * SpriteSpeedChange;
        //aimIndex += AddValue;


        if (!zeroFire.charged)
        {
            AimIndex += AddValue;

            AimIndex = AimIndex > notChargedArray.Length ? 0 : AimIndex;

            SRenderer.sprite = notChargedArray[(int)AimIndex];
        }
        else
        {
            AimIndex += AddValue * 2;

            AimIndex = AimIndex > chargedArray.Length ? 0 : AimIndex;

            SRenderer.sprite = chargedArray[(int)AimIndex];

        }

    }

    void NewAimAnimation(int SpriteSpeedChange, Sprite[] notChargedArray)
    {
        IdleIndex = 0;
        MoveIndex = 0;
        JumpIndex = 0;
        //SwordIndex = 0;

        float AddValue = Time.deltaTime * SpriteSpeedChange;
        AimIndex += AddValue;

        AimIndex = AimIndex > notChargedArray.Length ? 0 : AimIndex;

        SRenderer.sprite = notChargedArray[(int)AimIndex];

    }

    IEnumerator SwordAttack()
    {
        JumpIndex = 0;
        MoveIndex = 0;
        AimIndex = 0;
        IdleIndex = 0;
        SwordIndex = 0;
        float AddValue = Time.deltaTime * 15;

        while ((int)SwordIndex < SwordStandingArray.Length)
        {
            SwordIndex += AddValue;
            //print(SwordIndex);
            if ((int)SwordIndex < SwordStandingArray.Length)
            {
                SRenderer.sprite = SwordStandingArray[(int)SwordIndex];
            }

            yield return null;
        }
        print("teste");
    }

    #endregion

    void flipSprite()
    {

        if (Horizontal > 0)
        {
            Flip(0f);
        }
        else if (Horizontal < 0)
        {
            Flip(180f);
        }

    }

    void Flip(float degrees)
    {
        transform.rotation = Quaternion.Euler(0, degrees, 0);
    }

    void SetSprites()
    {
        IdleArray = SpriteManager.CarregaSprite("Zero/Idle");
        WalkArray = SpriteManager.CarregaSprite("Zero/Walking");
        JumpArray = SpriteManager.CarregaSprite("Zero/Jumping");
        FallingArray = SpriteManager.CarregaSprite("Zero/Falling");
        DashArray = SpriteManager.CarregaSprite("Zero/Dash");
        AimingStandingArray = SpriteManager.CarregaSprite("Zero/Shooting Standing");
        AimingWalkingArray = SpriteManager.CarregaSprite("Zero/Shooting Walking");
        AimingJumpingArray = SpriteManager.CarregaSprite("Zero/Aiming Jumping");
        AimingFallingArray = SpriteManager.CarregaSprite("Zero/Aiming Falling");
        SwordJumpingArray = SpriteManager.CarregaSprite("Zero/Sword attack jumping");
        SwordStandingArray = SpriteManager.CarregaSprite("Zero/Sword attack standing");
        ChargingStanding = SpriteManager.CarregaSprite("Zero/Charging Standing");
        ChargingWalking = SpriteManager.CarregaSprite("Zero/Charging Walking");
        ChargingJumping = SpriteManager.CarregaSprite("Zero/Charging Jumping");
        ChargingFalling = SpriteManager.CarregaSprite("Zero/Charging Falling"); ;
    }

    float Horizontal
    {
        get { return inputHorizontal; }
    }

    public override void SetJump()
    {
        if (!jump && onGround)
        {
            jumpForce = jumpForceMax;
            jump = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Spike"))
        {
            //lifeCount--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ItemPickUp"))
        {
            itemCount++;
            Destroy(col.gameObject);
            SetAudioPickUp();
        }
    }

    bool CheckGround()
    {
        Ray2D ray2d = new Ray2D(transform.position, -transform.up);

        Debug.DrawRay(ray2d.origin, ray2d.direction * valorDirecao, Color.red);

        RaycastHit2D raycastHit2d = Physics2D.Raycast(ray2d.origin, ray2d.direction, valorDirecao);

        if (raycastHit2d)
        {
            if (raycastHit2d.collider.gameObject.CompareTag("Chao"))
            {
                return true;
            }
        }
        return false;

    }

    void retornaAoInicio()
    {

        if (lifeCount <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (transform.position.y <= posicaoQueda.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetAudioTiroComum()
    {
        //aud.PlayOneShot(atirandoBalaComum);
    }

    public void SetAudioEspecial()
    {
        //aud.PlayOneShot(atirandoBalaEspecial);
    }

    public void SetAudioCharging()
    {
        //aud.PlayOneShot(caregandoTiro);
    }

    public void SetAudioPickUp()
    {
        //aud.PlayOneShot(pegandoItem);
    }

    public override void SetMove(float move)
    {
        inputHorizontal = move;
    }

    public override void SetFire(bool attack)
    {
        attacking = attack;
    }
}
