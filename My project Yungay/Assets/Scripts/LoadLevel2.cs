using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel2 : MonoBehaviour
{
    public Animator fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            StartCoroutine(Fading());

        }
    }
    IEnumerator Fading()
    {
        fade.SetBool("isFade", true);
        yield return new WaitForSeconds(3f);
        fade.SetBool("isFade", false);
        
        SceneManager.LoadScene("Level3");

    }
}
