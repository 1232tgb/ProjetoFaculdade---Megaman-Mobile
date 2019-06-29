using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string levelName1, levelName2, levelName3;
    public Image imageFade;

    private void Start()
    {
        if (imageFade)
        {
            imageFade.enabled = true;
            imageFade.CrossFadeAlpha(0.01f, 2, false);
        }
    }

    public void NavegarTela1()
    {
        imageFade.CrossFadeAlpha(1, 2, false);
        Invoke("trocaTela1", 1);
    }

    void trocaTela1()
    {
        SceneManager.LoadScene(levelName1);
    }


    public void NavegarTela2()
    {
        imageFade.CrossFadeAlpha(1, 2, false);
        Invoke("trocaTela2", 1);
    }

    void trocaTela2()
    {
        SceneManager.LoadScene(levelName2);
    }

    public void NavegarTela3()
    {
        imageFade.CrossFadeAlpha(1, 2, false);
        Invoke("trocaTela3", 1);
    }

    void trocaTela3()
    {
        SceneManager.LoadScene(levelName3);
    }

}
