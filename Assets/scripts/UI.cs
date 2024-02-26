using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        CurrentAM = GameObject.Find("Current_Ammo").GetComponent<Text>();
        HB = GameObject.Find("Greenbar").GetComponent<Image>();
        a_3 = GameObject.Find("Toggle_dash").GetComponent<Toggle>();
        a_3.isOn = false;
        UnityEngine.Debug.Log(CurrentAM);
    }

    // Update is called once per frame
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
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
        yield return new WaitForSeconds(3);
        a_3.isOn = false;
    }

    private void Takedamage ()
    {
        playerhealth -= damage;
        HB.fillAmount = playerhealth / 100f;
    }
}   
