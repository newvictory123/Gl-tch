using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Mechanics : MonoBehaviour
{
    public Image HB;
    public float maxHealth;
    public float currentHealth;
    public GameObject DeathScreen;

    private void Start()
    {
        HB = GameObject.Find("Greenbar").GetComponent<Image>();
        currentHealth = maxHealth;
        DeathScreen.SetActive(false);
    }

    private void Update()
    {
        HB.fillAmount = currentHealth / 100f;

        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            DeathScreen.SetActive(true);
        }
    }

}
