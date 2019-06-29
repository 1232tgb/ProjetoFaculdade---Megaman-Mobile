using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionFase : MonoBehaviour
{
    public string path;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("trigger");

            Invoke("TrocaCena", 2);
        }
    }


    void TrocaCena()
    {
        SceneManager.LoadScene(path);
    }

}
