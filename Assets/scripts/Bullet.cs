using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Player_Mechanics PM;


    private void Awake()
    {
        PM = GameObject.Find("Player").GetComponent<Player_Mechanics>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            PM.currentHealth -= 1;
            Destroy(gameObject);
        }
    }


}
