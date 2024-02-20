using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static Action shootInput;
    public static Action reloadInput;
    public Transform spawnpoint;
    public GameObject Projectileprefab;
    public float projectilevelocity;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
            var projectile = Instantiate(Projectileprefab, spawnpoint.position, spawnpoint.rotation);
            projectile.GetComponent<Rigidbody>().velocity = spawnpoint.forward * projectilevelocity;

        }

        if (Input.GetKeyDown(reloadKey))
        {
                reloadInput?.Invoke();
        }
            
    }
}
