using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Mechanics : MonoBehaviour
{

    public Slider healthBar;
    public float maxHealth;
    public float currentHealth;
    public GameObject deathScreen;
    public GameObject winScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            deathScreen.SetActive(true);
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "WIN_Zone")
        {
            winScreen.SetActive(true);
            Debug.Log("Hello");
        }
    }


}
