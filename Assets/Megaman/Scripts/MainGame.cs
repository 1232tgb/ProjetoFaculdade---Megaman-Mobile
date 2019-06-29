using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;//instancia da propria classe --- singleton

    //public int itemCount;
    public Text ringsText;
    public Text timeText;
    public Text livesText;
    public List<EnemyAI> inimigos;
    public bool noMoreEnemies;

    float timer = 60 * 3;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        //inimigos = new List<EnemyAI>();

        GameObject[] inimigosArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enmey in inimigosArray)
        {
            inimigos.Add(enmey.GetComponent<EnemyAI>());
        }


    }


    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.Floor(timer % 60);
            timeText.text = " " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        for (int i = 0; i < inimigos.Count; i++)
        {
            if (i > inimigos.Count) { continue; }

            if(inimigos[i] == null)
            {
                inimigos.RemoveAt(i);
            }
        }

        if(inimigos.Count<= 0)
        {
            noMoreEnemies = true;
        }
    }


    public void SetItems(int itens)
    {
        ringsText.text = "Itens: " + itens;
    }

    public void SetLives(int lifeCount)
    {
        livesText.text = "Lives: " + lifeCount;
    }



}
