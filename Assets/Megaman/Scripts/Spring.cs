using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    //fazer o sonic pular com animacao

    [SerializeField]
    Sprite[] SpringSprites;
    [SerializeField]
    SpriteRenderer sRenderer;
    public int JumpForce;

   

    // Use this for initialization
    void Awake()
    {
        SpringSprites = Resources.LoadAll<Sprite>("Spring");
        sRenderer = GetComponent<SpriteRenderer>();
       
       
    }


    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() != null) 
        {
            
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*JumpForce);
           // col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
            sRenderer.sprite = SpringSprites[1];
            Invoke("ResetSpring", 0.5f);
        
        }
    }

    void ResetSpring() 
    {
        sRenderer.sprite = SpringSprites[0];
    }

	
    //// Update is called once per frame
    //void Update () {
		
    //}
}
