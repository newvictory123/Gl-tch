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

     GameObject UI;
     PlayerMovement moveScript;
     PlayerCam cameraScript;
     GameObject Gun;
    

    private void Start()
    {
        HB = GameObject.Find("Greenbar").GetComponent<Image>();
        currentHealth = maxHealth;
        deathScreen.SetActive(false);
        moveScript = GetComponent<PlayerMovement>();
        cameraScript = GameObject.Find("Player Camera").GetComponent<PlayerCam>();
        Gun = GameObject.Find("Arm holder");
        UI = GameObject.Find("UI");
    }

    private void Update()
    {
        HB.fillAmount = currentHealth / 100f;

        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            deathScreen.SetActive(true);
            UI.SetActive(false);
            moveScript.enabled = false;
            cameraScript.enabled = false;
            Gun.SetActive(false);
            Cursor.visible = true;
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "WIN_Zone")
        {
            winScreen.SetActive(true);
            UI.SetActive(false);
            moveScript.enabled = false;
            cameraScript.enabled = false;
            Gun.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState=CursorLockMode.Confined;
        }
    }


}
