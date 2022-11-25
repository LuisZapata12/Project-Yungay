using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Hand hand;
    public static bool isReload;
    public EquipmentRange currentWeapon;

    private void Update()
    {
        currentWeapon = Hand.currentItem as EquipmentRange;
        Debug.Log(isReload); 
    }
    // Start is called before the first frame update
    public void Relo()
   {
        hand.Reloa();
        isReload = false;
    }

    public void Sound()
    {
        AudioManager.Instance.PlaySFX(currentWeapon.reload);
    }

    public void ExitAni()
    {
       hand.animatorPlayer.SetBool("isReload", false);
        isReload = true;
    }
}
