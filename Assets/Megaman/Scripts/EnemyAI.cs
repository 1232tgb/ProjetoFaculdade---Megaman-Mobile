using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    Sprite[] spritesAnimados;
    public string path;
    public float velocidade;
    public Transform playerTransform;
    SpriteRenderer enemyRenderer;
    float animaIndex;
    public int FrameChangeSpeed;
    bool walking;
    public bool inFireRange;
    public int healthvalue;

    void Awake()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        spritesAnimados = SpriteManager.CarregaSprite(path);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Start()
    {
        animaIndex = 0;
   
    }
    void Update()
    {
        detectaInimigo();

        if (healthvalue < 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    void detectaInimigo()
    {

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Vector3 direcao = (transform.position - playerTransform.position).normalized;


        if (distance < 10 && distance > 4)
        {
            if (direcao.x < 0)
            {
                walking = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mover(true);
            }
            else if (direcao.x > 0)
            {
                walking = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mover(false);
            }
        }
        else if (distance < 5 || distance > 10)
        {
            walking = false;
        }


        if (distance < 16 && distance > 3)
        {
            inFireRange = true;
        }
        else
        {
            inFireRange = false;
        }

        animaInimigo();
        //print(distance);
    }


    void mover(bool invertido)
    {
        Vector3 move = new Vector3(velocidade, 0, 0);
        if (!invertido)
        {
            transform.position -= move;
        }
        else
        {
            transform.position += move;
        }
    }

    void animaInimigo()
    {
        if (walking)
        {
            float AddValue = Time.deltaTime * FrameChangeSpeed;
            animaIndex += AddValue;
            animaIndex = animaIndex > spritesAnimados.Length ? 0 : animaIndex;
            enemyRenderer.sprite = spritesAnimados[(int)animaIndex];
        }
        else
        {
            enemyRenderer.sprite = spritesAnimados[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<BulletBaseClass>())
        {
            healthvalue -= col.gameObject.GetComponent<BulletBaseClass>().dano;
        }
    }

}
