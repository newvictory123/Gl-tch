using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Player_Mechanics PM;
    public float damage;


    private void Awake()
    {
        PM = GameObject.Find("Player").GetComponent<Player_Mechanics>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.tag == "Enemy_Bullet") 
        {
            PM.currentHealth -= damage;
            Destroy(gameObject);
        }
    }


}
