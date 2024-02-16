using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    public GameObject camera;
    public int activeAbility = 0;

    private bool timerRunning = false;

    public float dashSpeed = 200f;
    private Rigidbody rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        
        RaycastHit hit;

        //Ability 1:
        //Button: F
        //Description: Omnidirectional dash (dashes where you are facing)

        if (Input.GetKeyDown(KeyCode.F))
        {
            activeAbility = 1;
            animator.SetInteger("Active_ability", 1);
            Vector3 dashDirection = camera.transform.forward;
            rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
            StartTimer();
        }

        //Ability 2:
        //Button : C
        //Temporarily makes cube at the targeted location, can only be used on certain surfaces

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeAbility = 2;
        }

        //Ability 3:
        //Button : V
        //Temporarily makes a hole in the targeted object, can only be used on certain surfaces
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeAbility = 3;
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activeAbility = 4;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && activeAbility == 2)
        {
            //Instantiate()
        }


        //Old abilities
        // button 1: shrink ray
        // button 2: growth ray
        // button 3: hitbox remover
        // button 4: nothing yet, used for changing colors
        /*
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && activeAbility == 1 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit.transform.localScale = new Vector3(hit.transform.localScale.x * 0.9f, hit.transform.localScale.y * 0.9f, hit.transform.localScale.z * 0.9f);
        }
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && activeAbility == 2 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit.transform.localScale = new Vector3(hit.transform.localScale.x * 1.1f, hit.transform.localScale.y * 1.1f, hit.transform.localScale.z * 1.1f);
        }
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) && activeAbility == 3 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit.collider.enabled = false;
        }
        */
    }
    void StartTimer()
    {
        if (!timerRunning)
        {
            StartCoroutine(TimerCoroutine());
        }
    }

    IEnumerator TimerCoroutine()
    {
        timerRunning = true;

        yield return new WaitForSeconds(0.75f);

        animator.SetInteger("Active_ability", 0);
        activeAbility = 0;


        timerRunning = false;
    }
}
