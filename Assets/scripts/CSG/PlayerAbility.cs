using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Csgreal;

public class PlayerAbility : MonoBehaviour
{
    //General
    public int activeAbility = 0;

    //For dash ability (1)
    public GameObject camera;
    private bool timerRunning = false;
    public float dashSpeed = 200f;
    private Rigidbody rb;
    public Animator animator;

    //Booleans for deactivating
    private bool ability1active;
    private bool ability2active;
    private bool ability3active;

    //For block placement ability (2)
    public GameObject tempBlock;
    private float extrudeDistance = 1.11f;
    private float destructionDelay = 5;
    public int blockCount = 0;
    public float moveDuration = 0.03f;

    //For hole placement ability (3)
    UnityEngine.ProBuilder.Csgreal.Model HoledObject;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        //Ability 1:
        //Button: F
        //Description: Omnidirectional dash (dashes where you are facing)

        if (Input.GetKeyDown(KeyCode.F))
        {
            activeAbility = 1;
            animator.SetInteger("Active_ability", 1);
            Vector3 dashDirection = camera.transform.forward;
            rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
            DashTimer(1f);
        }

        //Ability 2:
        //Button : C
        //Temporarily makes cube at the targeted location, can only be used on certain surfaces

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (ability2active == false)
            {
                activeAbility = 2;
                ability2active = true;
            }
            else
            {
                activeAbility = 0;
                ability2active = false;
            }
        }

        //Ability 3:
        //Button : V
        //Temporarily makes a hole in the targeted object, can only be used on certain surfaces
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (ability2active == false)
            {
                activeAbility = 3;
                ability2active = true;
            }
            else
            {
                activeAbility = 0;
                ability2active = false;
            }
        }


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
                    StartCoroutine(blockTimer());
                    StartCoroutine(TimerCoroutine(1f, 2));
                }
            }
        }


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

                    // Get the GameObject that the raycast hit
                    GameObject hitObject = hitInfo.collider.gameObject;

                    Vector3 cubePosition = hitInfo.point + hitInfo.normal * -extrudeDistance;
                    GameObject placedObject = Instantiate(tempBlock, cubePosition, Quaternion.identity);
                    placedObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                    //StartCoroutine(MoveObject(placedObject, hitInfo.point + hitInfo.normal * extrudeDistance));
                    StartCoroutine(blockTimer());
                    StartCoroutine(TimerCoroutine(1f, 3));

                    //Instantiate(tempBlock, cubePosition, Quaternion.identity);

                    //CSGreal.BooleanOp booleanOp = CSGreal.BooleanOp.Subtraction;
                    //HoledObject = CSGreal.Perform(booleanOp, hitObject, placedObject);

                   
                    
                    //Instantiate(HoledObject, cubePosition, Quaternion.identity);
                    //CSGreal.Subtract(hitObject, placedObject);
                    //Debug.Log(CSGreal.Subtract(hitObject, placedObject));

                }
            }
        }
        // Check if the ray hits any collider in the scene

            /*
            CreateHole(obj, hitPoint, hitNormal);
        }
    }

}
void CreateHole(Vector3 position, Vector3 normal)
{
    // Instantiate the hole prefab
    GameObject holeObject = Instantiate(holePrefab, position, Quaternion.identity);

    // Scale the hole object based on the hole size
    holeObject.transform.localScale = new Vector3(holeSize, holeSize, holeSize);

    // Ensure that the hole object's normal aligns with the surface normal
    holeObject.transform.up = normal;

    // Get the target object to subtract the hole from
    GameObject targetObject = null;
    Collider[] colliders = Physics.OverlapBox(position, new Vector3(holeSize * 0.5f, holeSize * 0.5f, holeSize * 0.5f));
    foreach (Collider collider in colliders)
    {
        if (collider.gameObject != holeObject)
        {
            targetObject = collider.gameObject;
            break;
        }
    }

    // Perform boolean subtraction to create the hole
    if (targetObject != null)
    {
        ProBuilderMesh targetMesh = targetObject.GetComponent<ProBuilderMesh>();
        if (targetMesh != null)
        {
            // Perform boolean subtraction to create the hole
            PbMeshOperations.Subtract(targetMesh, holeObject.GetComponent<ProBuilderMesh>());

        }
        else
        {
            Debug.LogWarning("Target object doesn't have a ProBuilderMesh component.");
        }
    }
    else
    {
        Debug.LogWarning("No target object found to create hole in.");
    }

    // Destroy the hole object
    Destroy(holeObject);
    */
    }

    
    void DashTimer(float timeToWait)
    {
        if (!timerRunning)
        {
            StartCoroutine(TimerCoroutine(timeToWait, 0));
        }
    }

    IEnumerator TimerCoroutine(float timeToWait, int abilityDefault)
    {
        timerRunning = true;

        yield return new WaitForSeconds(timeToWait);

        animator.SetInteger("Active_ability", 0);
        activeAbility = abilityDefault;


        timerRunning = false;
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
    IEnumerator blockTimer()
    {
        blockCount++;

        yield return new WaitForSeconds(5.1f);

        blockCount--;
    }


}
