using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroInimigo : MonoBehaviour
{
    public int dano;
    Vector3 direcaoDoAlvo;
    public float velocidade;

    private void Update()
    {
        transform.position += direcaoDoAlvo*velocidade;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.isTrigger && collision.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.GetComponent<ZeroControl>())
            {
                collision.gameObject.GetComponent<ZeroControl>().HealthValue -= dano;
            }
            Destroy(gameObject);
        }
    }

    public void SetDirecao(Vector3 direcao)
    {
        if (direcao.magnitude > 1)
        {
            direcao.Normalize();
        }

        direcaoDoAlvo = direcao;
    }
}
