using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField]
    Transform pontoDoTiro;
    [SerializeField]
    GameObject tiro;
    public string path;



    EnemyAI enemyAI;
    public float contadorPraAtirar, limiteDoContador;

    void Start()
    {
        Transform[] obj = GetComponentsInChildren<Transform>();

        foreach (Transform childrenTransform in obj)
        {
            if (childrenTransform.name == "PontoDoTiro")
            {
                pontoDoTiro = childrenTransform;
            }
        }


        tiro = carregaGameObject(path);
        enemyAI = GetComponent<EnemyAI>();
    }



    void Update()
    {
        if (enemyAI.inFireRange)
        {
            if (pontoDoTiro)
            {
                Vector3 direcaoTiro = (enemyAI.playerTransform.position - pontoDoTiro.position);

                if (direcaoTiro.magnitude > 1)
                    direcaoTiro.Normalize();

                AtiraBalaComum(direcaoTiro);
            }
        }


    }

    void AtiraBalaComum(Vector3 direcao)
    {

        if (contadorPraAtirar < limiteDoContador)
        {
            contadorPraAtirar += Time.deltaTime;
        }
        else
        {
            contadorPraAtirar = 0;
            GameObject obj = Instantiate(tiro, pontoDoTiro.position, pontoDoTiro.rotation);
            obj.GetComponent<TiroInimigo>().SetDirecao(direcao);

        }

    }



    GameObject carregaGameObject(string Path)
    {
        return Resources.Load<GameObject>(Path);
    }

}
