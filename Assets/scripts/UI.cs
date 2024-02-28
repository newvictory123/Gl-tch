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
    private Toggle a_3;
    private float playerhealth = 100f;
    private readonly float damage = 10f;


    // Start is called before the first frame update
    void Start()
    {
        a_3 = GameObject.Find("Toggle_dash").GetComponent<Toggle>();
        a_3.isOn = false;
    }

    // Update is called once per frame
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            a_3.isOn = true;
            StartCoroutine(Toggleoffdelay());
        }
    }

    private IEnumerator Toggleoffdelay()
    {
        yield return new WaitForSeconds(2.5f);
        a_3.isOn = false;
    }


}   
