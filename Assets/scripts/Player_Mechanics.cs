using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Mechanics : MonoBehaviour
{

    public Slider healthBar;
    public float maxHealth;
    public float currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            //Die
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy_Bullet")
        {
            currentHealth -= 1;
        }
    }

}
