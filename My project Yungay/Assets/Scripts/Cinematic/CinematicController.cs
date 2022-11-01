using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour
{

    private PlayableDirector currentDirector;
    private bool cinematicSkipped = true;
    private float timeToSkipTo;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !cinematicSkipped)
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

    public void GetDirector(PlayableDirector director)
    {
        cinematicSkipped = false;
        currentDirector = director;
    }
    public void GetSkipTime(float skipTime)
    {
        timeToSkipTo = skipTime;

    }
}
