using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacoBoxeo : MonoBehaviour
{
    public int life;


    public void RecibirDa�o(int valor)
    {
        life -= valor;
    }
}
