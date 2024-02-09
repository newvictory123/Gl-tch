using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    public GameObject camera;
    public Vector3 direction;
    public int activeAbility = 1;


    private void Update()
    {
        // button 1: shrink ray
        // button 2: growth ray
        // button 3: hitbox remover
        // button 4: nothing yet, used for changing colors

        
        direction = camera.transform.position;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeAbility = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeAbility = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeAbility = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activeAbility = 4;
        }

        //ability 1 2 and 3 checks
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
    }
}
