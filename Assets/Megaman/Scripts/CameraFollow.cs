using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float altura;
    Transform player;
    // Use this for initialization
    void Awake()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
            transform.position = new Vector3(player.position.x, player.position.y + altura, transform.position.z);

    }
}
