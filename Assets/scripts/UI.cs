using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI : MonoBehaviour


{
    
    private Text CurrentAM;
    private Image HB;
    private Toggle a_1;
    private Toggle a_2;
    private Toggle a_3;
    private float playerhealth = 100f;
    private readonly float damage = 10f;
    private string Ammo;


    // Start is called before the first frame update
    void Start()
    {
        Ammo = GameObject.Find("Arm").GetComponent<Gun>().gunData.currentAmmo.ToString();
        CurrentAM = GameObject.Find("Current Ammo").GetComponent<Text>();
        HB = GameObject.Find("Green bar").GetComponent<Image>();
        a_1 = GameObject.Find("toggle_destroy").GetComponent<Toggle>();
        a_2 = GameObject.Find("toggle_build").GetComponent<Toggle>();
        a_3 = GameObject.Find("toggle_dash").GetComponent<Toggle>();
        a_1.isOn = false;
        a_2.isOn = false;
        a_3.isOn = false;
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(Ammo);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (a_2.isOn == true)
            {
                a_2.isOn = false;
            }
            else if (a_2.isOn == false)
            {
                a_2.isOn = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (a_1.isOn == true)
            {
                a_1.isOn = false;
            }
            else if (a_1.isOn == false)
            {
                a_1.isOn = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            a_3.isOn = true;
            StartCoroutine(Toggleoffdelay());
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Takedamage();
        }
    }

    private IEnumerator Toggleoffdelay()
    {
        yield return new WaitForSeconds(1);
        a_3.isOn = false;
    }

    private void Takedamage ()
    {
        playerhealth -= damage;
        HB.fillAmount = playerhealth / 100f;
    }
}   
