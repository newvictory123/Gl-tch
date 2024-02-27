using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Mechanics : MonoBehaviour
{
    public Image HB;
    public float maxHealth;
    public float currentHealth;
    public GameObject deathScreen;
    public GameObject winScreen;

    private void Start()
    {
        HB = GameObject.Find("Greenbar").GetComponent<Image>();
        currentHealth = maxHealth;
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        HB.fillAmount = currentHealth / 100f;

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
