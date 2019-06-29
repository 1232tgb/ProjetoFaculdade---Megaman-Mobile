using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpriteManager : MonoBehaviour
{

    //[SerializeField]
    //private Sprite[] SonicIdle, SonicWalk, SonicRun, SonicJump;
    //[SerializeField]
    //private Sprite[] TailsIdle, TailsWalk, TailsRun, TailsJump, TailsFly;


    //SpriteControl ControleSonic;
    //TailsControl ControleTails;

    void Awake()
    {
        //SetSpriteSonic();
        //SetSpriteTails();
        //ControleSonic = GameObject.FindGameObjectWithTag("Sonic").GetComponent<SpriteControl>();
        //ControleTails = GameObject.FindGameObjectWithTag("Tails").GetComponent<TailsControl>();
    }

    // Use this for initialization
    void Start()
    {

        //ControleSonic.SetSprites(SonicIdle, SonicWalk, SonicRun, SonicJump);


        //ControleTails.SetSprites(TailsIdle, TailsWalk, TailsRun, TailsJump, TailsFly);

    }

    #region Inutilizados
    /*
    void SetSpriteSonic()
    {
        SonicIdle = Resources.LoadAll<Sprite>("Sonic/Sonic Idle");
        SonicWalk = Resources.LoadAll<Sprite>("Sonic/Sonic Walk");
        SonicRun = Resources.LoadAll<Sprite>("Sonic/Sonic Run");
        SonicJump = Resources.LoadAll<Sprite>("Sonic/Sonic Jump");
    }

    void SetSpriteTails()
    {
        TailsIdle = Resources.LoadAll<Sprite>("Tails/Tails Idle");
        TailsWalk = Resources.LoadAll<Sprite>("Tails/Tails Walk");
        TailsRun = Resources.LoadAll<Sprite>("Tails/Tails Run");
        TailsJump = Resources.LoadAll<Sprite>("Tails/Tails Jump");
        TailsFly = Resources.LoadAll<Sprite>("Tails/Tails Fly");
    }
    */

    #endregion



    public static Sprite[] CarregaSprite(string str)
    {
        Sprite[]arraySprite = Resources.LoadAll<Sprite>(str);
        return arraySprite;
    }
}
