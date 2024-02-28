using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    [Header("References")]
    public GunData gunData;
    [SerializeField] private Transform cam;

    public Transform spawnpoint;
    public GameObject Projectileprefab;
    public float projectilevelocity;
    private Text AmmoUICount;
    private Text AmmoUIMax;

    float timeSinceLastShot;

    private void Start() {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        AmmoUICount = GameObject.Find("Current_Ammo").GetComponent<Text>();
        AmmoUIMax = GameObject.Find("Max_Ammo").GetComponent <Text>();
        
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f) && FindObjectOfType<PlayerAbility>().activeAbility == 0;

    private void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                var projectile = Instantiate(Projectileprefab, spawnpoint.position, spawnpoint.rotation);
                projectile.GetComponent<Rigidbody>().velocity = spawnpoint.forward * projectilevelocity;
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            
            }
        
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);

        AmmoUICount.text = gunData.currentAmmo.ToString();
        AmmoUIMax.text = gunData.magSize.ToString();
                
    }

    private void OnGunShot() {  }
}
