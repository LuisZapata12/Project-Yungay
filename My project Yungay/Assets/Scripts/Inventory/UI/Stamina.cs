using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public PlayerModel model;
    public Image imageStamina;
    public CanvasGroup staminaGroup;
    


    // Start is called before the first frame update
    void Start()
    {
        model.staActual = model.staMax;
    }

    // Update is called once per frame
    void Update()
    {
        model.staActual = Mathf.Clamp(model.staActual, -1f, model.staMax);

        if (!model.isRunning)
        {
            if (model.staActual <= model.staMax)
            {
                StaminaRegen();
                CheckStamina(1);
            }

            if (model.staActual >= model.staMax)
            {
                CheckStamina(0);
            }
        }

        IsRunning();
    }

    public void IsRunning()
    {
        if (model.actualSpeed >= model.speedRun && PlayerGroundCheck.grounded)
        {
            model.staActual -= model.staminaDrain * Time.deltaTime;
            CheckStamina(1);
        }
    }

    public void StaminaRegen()
    {
        if(!Input.GetKey(KeyCode.LeftShift) && !model.isRunning && model.actualSpeed <= 5f)
        {
            model.staActual += model.staRegen * Time.deltaTime;
            CheckStamina(1);
        }
        
    }

    void CheckStamina(int value)
    {
        imageStamina.fillAmount = model.staActual / model.staMax;

        if (value == 0)
        {
            staminaGroup.alpha = 0;
        }
        else
        {
            staminaGroup.alpha = 1;
        }
    }
}
