using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    public GameObject camera;
    public Vector3 direction;
    public bool ability4Active = false;

    private void Update()
    {
        // button 1: shrink ray
        // button 2: growth ray
        // button 3: hitbox remover

        direction = camera.transform.position;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                hit.transform.localScale = new Vector3(hit.transform.localScale.x * 0.9f , hit.transform.localScale.y * 0.9f, hit.transform.localScale.z * 0.9f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                hit.transform.localScale = new Vector3(hit.transform.localScale.x * 1.1f, hit.transform.localScale.y * 1.1f, hit.transform.localScale.z * 1.1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                hit.collider.enabled = false;
            }
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            ability4Active = true;
        }
    }
}
