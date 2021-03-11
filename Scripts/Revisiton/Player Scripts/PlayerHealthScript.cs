using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    #region Varibles

    [SerializeField]
    private float health = 100;

    [SerializeField]
    private float bloodLossRate = 0.01f;

    [SerializeField]
    private bool isBleeding = false;

    private int bleedingDegree = 5;

    #endregion
    void Update()
    {
        if (isBleeding)
        {
            BleedingEffect();
        }
        //Debug.Log(health);
    }

    #region Methods
    public float getHealthPoints()
    {
        return health;
    }

    public void setHealthPoints(float newHealth)
    {
        health = newHealth;
    }

    public void BleedingEffect()
    {
        health -= bloodLossRate * bleedingDegree;
    }

    public void setBleedingEffect(bool bleeding)
    {
        isBleeding = bleeding;
    }

    public bool getBleedingEffect()
    {
        return isBleeding;
    }

    public void setBleedingDegree(int degree)
    {
        bleedingDegree = degree;
    }

    public int getBleedingDegree()
    {
        return bleedingDegree;
    }
    #endregion
}
