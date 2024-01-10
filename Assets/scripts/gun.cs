using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;

public class gun : MonoBehaviour
{
    [SerializeField] GunData gunData;
    [SerializeField] private Transform muzzle;

    float timeSinceLastShot;

    private void Start()
    {
        shooting.shootInput += shootingGun;
        shooting.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.isReloading)
        {
            //reload
        }
    }

    private IEnumerator Reload()
    {
        gunData.isReloading = true;

        yield return new WaitForSeconds(gunData.timeToReload);
    }

    private bool canShoot() => !gunData && timeSinceLastShot > 1f / (gunData.firerate / 60);
    public void shootingGun()
    {
        if (gunData.currentAmmoCount > 0)
        {
            if (canShoot())
            {
                if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.range))
                {
                   Idamagable damageable = hitInfo.transform.GetComponent<Idamagable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmoCount--;
                timeSinceLastShot = 0f;
                onGunShot();
            }
        } 
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }
    private void onGunShot()
    {
        Debug.Log("shot");
    }
}
