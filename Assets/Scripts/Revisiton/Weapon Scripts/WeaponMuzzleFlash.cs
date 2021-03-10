using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMuzzleFlash : MonoBehaviour
{
    [SerializeField]
    private Transform weaponEffect;
    [SerializeField]
    private Vector3 EffectNormal;
    [SerializeField]
    private Vector3 EffectOff;

    private void Awake()
    {
        weaponEffect = gameObject.transform.Find("WeaponFireEffect").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            weaponEffect.transform.localPosition = new Vector3(EffectNormal.x + EffectOff.x, EffectNormal.y + EffectOff.y, EffectNormal.z + EffectOff.z);
        }
        else
        { 
            weaponEffect.transform.localPosition = new Vector3(EffectNormal.x, EffectNormal.y, EffectNormal.z);
        }
    }
}
