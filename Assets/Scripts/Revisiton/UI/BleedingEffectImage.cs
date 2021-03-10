using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BleedingEffectImage : MonoBehaviour
{
    [SerializeField]
    private Image bleedingImage;

    [SerializeField]
    private PlayerHealthScript playerHealth;

    private float transperancy = 0f;

    private void Awake()
    {
        bleedingImage = GetComponent<Image>();
    }

    void Update()
    {
        if (playerHealth.getHealthPoints() == 100f)
        {
            HealingEffect();
        }

        if (playerHealth.getBleedingEffect() && playerHealth.getHealthPoints() > 0f)
        {

            BleedingEffect();
        }
    }

    public void BleedingEffect()
    {
        if(transperancy < 215f)
        {
            bleedingImage.color = new Color(bleedingImage.color.r, bleedingImage.color.g, bleedingImage.color.b, transperancy / 255);
        }
        transperancy += Time.deltaTime * 2 * playerHealth.getBleedingDegree();
    }

    public void HealingEffect()
    {
        transperancy = Mathf.Clamp(transperancy, 0f, 255f);
        if (transperancy != 0f)
        {
            bleedingImage.color = new Color(bleedingImage.color.r, bleedingImage.color.g, bleedingImage.color.b, transperancy / 255);
        }

        transperancy -= Time.deltaTime * 100;

        
    }
}
