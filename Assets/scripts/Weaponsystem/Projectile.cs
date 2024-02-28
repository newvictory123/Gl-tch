using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Projectile : MonoBehaviour
{

    private Transform SP;
    private float GD;

    private void Start()
    {
        SP = GameObject.Find("Arm").GetComponent<Gun>().spawnpoint;
        GD = GameObject.Find("Arm").GetComponent<Gun>().gunData.maxDistance;
    }


    public void OnCollisionEnter (Collision col)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
            if (Vector3.Distance(gameObject.transform.position, SP.position) > GD)
            Destroy(gameObject);
    }
}
