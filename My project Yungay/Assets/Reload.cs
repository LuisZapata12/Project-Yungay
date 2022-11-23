using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Hand hand;
    public EquipmentRange currentWeapon;

    private void Update()
    {
        currentWeapon = Hand.currentItem as EquipmentRange;
    }
    // Start is called before the first frame update
    public void Relo()
   {
        hand.Reload();
   }

    public void Sound()
    {
        AudioManager.Instance.PlaySFX(currentWeapon.reload);
    }

    public void ExitAni()
    {
       hand.animatorPlayer.SetBool("isReload", false);
    }
}
