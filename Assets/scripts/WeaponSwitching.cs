using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private Toggle Fastfire;
    private Toggle Singleshot;

    private void Start() {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
        Fastfire = GameObject.Find("Toggle_SingleShot").GetComponent<Toggle>();
        Singleshot = GameObject.Find("Toggle_FastFire").GetComponent<Toggle>();

    }

    private void SetWeapons() {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i);

        if (keys == null) keys = new KeyCode[weapons.Length];

        selectedWeapon = 0;
    }

    private void Update() {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++)
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
                selectedWeapon = i;

        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);
            timeSinceLastSwitch += Time.deltaTime;

        if (selectedWeapon == 0)
        {
            Singleshot.isOn = true;
            Fastfire.isOn = false;
        }

        else if (selectedWeapon == 1)
        {
            Fastfire.isOn = true;
            Singleshot.isOn = false;
        }


    }

    private void Select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected() {  }
}
