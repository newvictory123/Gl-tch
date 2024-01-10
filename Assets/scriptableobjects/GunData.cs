using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    public new string name;
    public float damage;
    public float range;

    public int currentAmmoCount;
    public int magSize;
    public int firerate;
    public int timeToReload;
    [HideInInspector]
    public bool isReloading;
}

