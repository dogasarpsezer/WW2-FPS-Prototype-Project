using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private PlayerSprintCrouchProne playerSprintState;
    [SerializeField]
    private PlayerAttack playerSteadyAimState;

    [Header("Stamina Values")]
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float staminaDropRate;
    [SerializeField]
    private float staminaGainRate;
    [SerializeField]
    private float staminaRegainTime;

    [Header("Stamina UI")]
    [SerializeField]
    private Slider staminaBar;
    [SerializeField]
    private Image fillColor;

    private float timer = 0f;

    private void Awake()
    {
        playerSprintState = GetComponent<PlayerSprintCrouchProne>();
        playerSteadyAimState = GetComponent<PlayerAttack>(); 

        staminaBar.maxValue = 100f;
        staminaBar.minValue = 0f;
    }

    private void Update()
    {
        StaminaConsume();
        staminaBar.value = stamina;

        if(stamina < 60f && stamina > 15f)
        {
            fillColor.color = new Color(225f/255f, 231f/255f, 25f/255f, 255f/255f);
        }
        else if (stamina <= 20f)
        {
            fillColor.color = new Color(166f/255f, 28f/255f, 0f/255f, 255f/255f);
        }
        else
        {
            fillColor.color = new Color(3f/255f,108f/255f, 0f/255f, 255f/255f);
        }
    }

    public void UseStaminaWithMultiplier(float multiplier)
    {
        stamina -= staminaDropRate * multiplier * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, 100f);
    }

    public float getStamina()
    {
        return stamina;
    }

    public void GainStamina()
    {
        stamina += staminaGainRate * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, 100f);
    }

    public void StaminaConsume()
    {
        if (playerSteadyAimState.SteadyAimState())
        {
            UseStaminaWithMultiplier(0.75f);
            timer = 0;
        }
        else if (playerSprintState.SprintingState())
        {
            UseStaminaWithMultiplier(1.25f);
            timer = 0;
        }
        else if (!playerSteadyAimState.SteadyAimState() && !playerSprintState.SprintingState())
        {
            timer += 1 * Time.deltaTime;
            
            if(timer >= staminaRegainTime)
            {
                GainStamina();
            }
            
        }
    }
}
