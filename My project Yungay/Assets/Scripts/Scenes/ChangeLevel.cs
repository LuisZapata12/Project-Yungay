using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ChangeLevel : MonoBehaviour
{

    public string x;

    public void ChangeScene(string level)
    { 
    SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeScene(x);
    }
}
