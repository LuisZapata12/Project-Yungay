using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour
{

    private PlayableDirector currentDirector;
    private bool cinematicSkipped = true;
    private float timeToSkipTo;
    private GameObject cursor;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        if (!cinematicSkipped)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentDirector ? true : false)
                {
                    currentDirector.time = 30.0f;
                    cinematicSkipped = true;
                }
                else
                {

                }
            }
        }
        else
        {
            cursor.SetActive(true);
        }
    }

    public void GetDirector(PlayableDirector director)
    {
        cursor.SetActive(false);
        cinematicSkipped = false;
        currentDirector = director;
    }
    public void GetSkipTime(float skipTime)
    {
        timeToSkipTo = skipTime;

    }
}
