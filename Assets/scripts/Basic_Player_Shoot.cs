using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Player_Shoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject camera;
    public GameObject bullet;
    public GameObject spawnPoint;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(camera.transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bulletObj, 5f);
    }
    
}
