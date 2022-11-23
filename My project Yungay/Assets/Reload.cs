using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Hand hand;
    // Start is called before the first frame update
   public void Relo()
   {
        hand.Reload();
   }

    public void Sound()
    {
        AudioManager.Instance.PlaySFX("Pistol_Reload");
    }
    public void ExitAni()
    {
       hand.animatorPlayer.SetBool("isReload", false);
    }
}
