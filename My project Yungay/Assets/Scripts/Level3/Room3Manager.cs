using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Room3Manager : MonoBehaviour
{
    public GameObject spawn;
    public GameObject obstacles;
    public PlayableDirector Cm;
    public CameraShake cameraShake;
    public bool endcinematic =false;
    int i = 0;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.transform.childCount <= 0 && endcinematic == false)
        {
            if (i == 0)
            {
                StartCoroutine(cameraShake.Shake());
                i++;
            }
            obstacles.SetActive(false);
            Debug.Log("Inicia cinematica");
            if (timer >= 1.6f)
            {
                Cm.Play();
            }else
            {
                timer += Time.deltaTime;
            }

        }
        else if (endcinematic == true)
        {
            Cm.Stop();
        }
        
    }


    public void EndCinematic()
    {
        endcinematic = true;
    }
}
