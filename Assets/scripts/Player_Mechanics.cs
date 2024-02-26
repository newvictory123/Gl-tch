using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Mechanics : MonoBehaviour
{

    public Slider healthBar;
    public float maxHealth;
    public float currentHealth;
    public GameObject DeathScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        DeathScreen.SetActive(false);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            DeathScreen.SetActive(true);
        }
    }

}
