using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    //General
    public int activeAbility = 0;
    int previousAbility;
    public Animator animator;
    public GameObject camera;

    //For dash ability (1)
    private bool timerRunning = false;
    public float dashSpeed = 200f;
    private Rigidbody rb;
    bool onCoolDown = false;

    //For block placement ability (2)
    public GameObject tempBlock;
    private float extrudeDistance = 1.11f;
    private float destructionDelay = 15;
    public float moveDuration = 0.25f;
    public int blockCount = 0;


    //For UI
    private Toggle a_1;
    private Toggle a_2;
    private Text CubeCounter;



    void Start()
    {
        a_1 = GameObject.Find("Toggle_destroy").GetComponent<Toggle>();
        a_2 = GameObject.Find("Toggle_build").GetComponent<Toggle>();
        CubeCounter = GameObject.Find("Current_Cube").GetComponent<Text>();
        a_1.isOn = false;
        a_2.isOn = false;
        CubeCounter.text = 0.ToString();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CubeCounter.text = blockCount.ToString();
        //Ability 1:
        //Button: F
        //Description: Omnidirectional dash (dashes where you are facing)

        if (Input.GetKeyDown(KeyCode.F) && !onCoolDown)
        {
            previousAbility = activeAbility;
            activeAbility = 1;
            animator.SetInteger("Active_ability", 1);
            Vector3 dashDirection = camera.transform.forward;
            rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
            DashTimer(1f, previousAbility);
            StartCoroutine(CoolDownTimer(2.5f));
        }

        //Ability 2:
        //Button : C
        //Temporarily makes cube at the targeted location, can only be used on certain surfaces

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (activeAbility == 0)
            {
                activeAbility = 2;
                a_2.isOn = true;
                a_1.isOn = false;
            }
            else if (activeAbility == 2)
            {
                activeAbility = 0;
                a_2.isOn = false;
                
            }
        }

        //Ability 3:
        //Button : V
        //Temporarily makes a hole in the targeted object, can only be used on certain surfaces
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (activeAbility != 3)
            {
                activeAbility = 3;
                a_1.isOn = true;
                a_2.isOn = false;
            }
            else if (activeAbility == 3)
            {
                activeAbility = 0;
                a_1.isOn = false;
                
            }
        }

        //Logic for ability 2
        if (Input.GetKeyDown(KeyCode.Mouse0) && activeAbility == 2 && blockCount < 3)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            

            if (Physics.Raycast(ray, out hitInfo))
            {
                Material hitMaterial = hitInfo.collider.gameObject.GetComponent<Renderer>().material;
                if (hitMaterial.name == "Ability material (Instance)")
                {
                    animator.SetInteger("Active_ability", 2);
                    Vector3 cubePosition = hitInfo.point + hitInfo.normal * -extrudeDistance;
                    GameObject placedObject = Instantiate(tempBlock, cubePosition, Quaternion.identity);
                    placedObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                    Destroy(placedObject, destructionDelay);
                    StartCoroutine(MoveObject(placedObject, hitInfo.point + hitInfo.normal * extrudeDistance));
                    StartCoroutine(BlockTimer());
                    StartCoroutine(AnimationTimer(1f, 2));
                }
            }
        }

        //Logic for ability 3
        if (Input.GetKeyDown(KeyCode.Mouse0) && activeAbility == 3 && blockCount < 3)
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Material hitMaterial = hitInfo.collider.gameObject.GetComponent<Renderer>().material;
                if (hitMaterial.name == "Ability material (Instance)")
                {
                    animator.SetInteger("Active_ability", 3);
                    StartCoroutine(AnimationTimer(1f, 3));
                }
            }
        }

    }

    
    void DashTimer(float timeToWait, int abilityDefault)
    {
        if (!timerRunning)
        {
            StartCoroutine(AnimationTimer(timeToWait, abilityDefault));
        }
    }

    IEnumerator AnimationTimer(float timeToWait, int abilityDefault)
    {
        timerRunning = true;

        yield return new WaitForSeconds(timeToWait);

        animator.SetInteger("Active_ability", 0);
        activeAbility = abilityDefault;

        timerRunning = false;
    }

    IEnumerator CoolDownTimer(float timeToWait)
    {
        onCoolDown = true;

        yield return new WaitForSeconds(timeToWait);

        onCoolDown = false;
    }

    IEnumerator MoveObject(GameObject obj, Vector3 targetPosition)
    {

        yield return new WaitForSeconds(0.4f);
        Vector3 startPosition = obj.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
    }
    IEnumerator BlockTimer()
    {
        blockCount++;

        yield return new WaitForSeconds(15f);

        blockCount--;
    }
}
