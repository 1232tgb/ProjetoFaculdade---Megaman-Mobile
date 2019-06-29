using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpecialFire { Especial_1, Especial_2 }
public class ZeroFire : MonoBehaviour
{

    SpecialFire EspecialAtual;
    ZeroControl zeroControl;
    GameObject tiro, tiroEspecial_1, tiroEspecial_2;
    Transform saidaDoProjetilNoChao, saidaDoProjetilNoAr;
    public float contadorTiro, limiarDoContador;
    float valorDoTiro;
    public bool charged, charging;

    void Awake()
    {
        zeroControl = GetComponent<ZeroControl>();
        //tiro = Resources.Load<GameObject>("Zero/Projeteis/TiroPadraoProjetil");
        tiro = carregaGameObject("Zero/Projeteis/TiroPadraoProjetil");
        tiroEspecial_1 = carregaGameObject("Zero/Projeteis/TiroEspecial1");
        tiroEspecial_2 = carregaGameObject("Zero/Projeteis/TiroEspecial2");
        saidaDoProjetilNoChao = GameObject.Find("saida1").GetComponent<Transform>();
        saidaDoProjetilNoAr = GameObject.Find("saida2").GetComponent<Transform>();
        contadorTiro = 0;
        valorDoTiro = 0;
        EspecialAtual = SpecialFire.Especial_1;
    }

    void Update()
    {
        //charging = Input.GetMouseButton(0);



        if (charging)
        {
            AcrescentaContador();
            if (contadorTiro <= limiarDoContador)
            {
                charged = false;
            }
            else
            {
                charged = true;
            }
        }
        if (zeroControl.attacking)
        {
            if (!charged)
            {
                AtiraBalaComum();

            }
            else
            {
                AtiraBalaEspecial();

            }
            charged = false;
            contadorTiro = 0;
        }


        ContagemDotiroEspecial();

    }

    public void setCharging(bool charge)
    {
        charging = charge;
    }

    void AtiraBalaComum()
    {
        if (zeroControl.onGround)
        {
            GameObject obj = Instantiate(tiro, saidaDoProjetilNoChao.position, saidaDoProjetilNoChao.rotation);
        }
        else
        {
            GameObject obj = Instantiate(tiro, saidaDoProjetilNoAr.position, saidaDoProjetilNoAr.rotation);
        }
        zeroControl.SetAudioTiroComum();
    }

    void AtiraBalaEspecial()
    {
        if (EspecialAtual == SpecialFire.Especial_1)
        {
            if (zeroControl.onGround)
            {
                GameObject obj = Instantiate(tiroEspecial_1, saidaDoProjetilNoChao.position, saidaDoProjetilNoChao.rotation);
            }
            else
            {
                GameObject obj = Instantiate(tiroEspecial_1, saidaDoProjetilNoAr.position, saidaDoProjetilNoAr.rotation);
            }
            zeroControl.SetAudioEspecial();
        }
        else if (EspecialAtual == SpecialFire.Especial_2)
        {
            if (zeroControl.onGround)
            {
                GameObject obj = Instantiate(tiroEspecial_2, saidaDoProjetilNoChao.position, saidaDoProjetilNoChao.rotation);
            }
            else
            {
                GameObject obj = Instantiate(tiroEspecial_2, saidaDoProjetilNoAr.position, saidaDoProjetilNoAr.rotation);
            }
            zeroControl.SetAudioEspecial();
        }
    }

    GameObject carregaGameObject(string Path)
    {
        return Resources.Load<GameObject>(Path);
    }

    void AcrescentaContador()
    {

        contadorTiro += Time.deltaTime;
        if (contadorTiro > 1.0f)
        {
            zeroControl.SetAudioCharging();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PowerUp"))
        {
            EspecialAtual = SpecialFire.Especial_2;
            Destroy(col.gameObject);
            zeroControl.SetAudioPickUp();

        }
    }


    void ContagemDotiroEspecial()
    {
        if (EspecialAtual == SpecialFire.Especial_2)
        {
            valorDoTiro += Time.deltaTime;
            //print(valorDoTiro);
            if (valorDoTiro >= 10)
            {
                valorDoTiro = 0;
                EspecialAtual = SpecialFire.Especial_1;
            }
        }
    }

}
