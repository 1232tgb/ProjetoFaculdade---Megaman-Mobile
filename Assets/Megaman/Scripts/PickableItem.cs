using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableItem : MonoBehaviour
{
    [SerializeField]
    Sprite[] StandardState;
    SpriteRenderer myRenderer;
    float AnimIndex;
    public string path;
    private void Awake()
    {
        AnimIndex = 0;
        StandardState = SpriteManager.CarregaSprite(path);
        myRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        animacao();
    }

    void animacao()
    {
      
        AnimIndex += Time.deltaTime * 2;
        AnimIndex = AnimIndex > StandardState.Length ? 0 : AnimIndex;
        myRenderer.sprite = StandardState[(int)AnimIndex];
    }
}
