using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cinematicas : MonoBehaviour
{
    public GameObject cam;
    public GameObject camCine;
    public int currentPosition = 0;
    public float timer = 0;
    bool camMove = true;
    public bool changeCam;
    public float transitionSpeed;
    public List<PositionSingle> position = new List<PositionSingle>();
    private string tagP = "PositionCine";

    private void Start()
    {
        transform.position = position[currentPosition].position.transform.transform.position;
       /* Transform Targets = transform.Find("Position");

        foreach(Transform PositionSingle in Targets)
        {
            Debug.Log(PositionSingle);
        }*/
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (changeCam == false)
            {
                changeCam = true;
            }
            else
            { changeCam = false; }
        }
    }
    private void LateUpdate()
    {
        ChangeCam();
    }
    public void ChangeCam()
    {
        if (changeCam)
        {
            cam.GetComponent<AudioListener>().enabled = false;
            camCine.GetComponent<AudioListener>().enabled = true;
            camCine.GetComponent<Camera>().depth = 1;
            NextPosition();
            MoveCam();
        }
        else
        {
            cam.GetComponent<AudioListener>().enabled = true;
            camCine.GetComponent<AudioListener>().enabled = false;
            camCine.GetComponent<Camera>().depth = -1;
        }
    }
    public void MoveCam()
    {if (camMove)
        {
            transform.position = Vector3.Lerp(transform.position, position[currentPosition].position.transform.position, Time.deltaTime * transitionSpeed);
            Vector3 currentAngle = new Vector3(Mathf.Lerp(transform.rotation.eulerAngles.x, position[currentPosition].position.transform.rotation.x, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(transform.rotation.eulerAngles.y, position[currentPosition].position.transform.rotation.y, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(transform.rotation.eulerAngles.z, position[currentPosition].position.transform.rotation.z, Time.deltaTime * transitionSpeed));
        }
    }
    public void NextPosition()
    {
        float distance = Vector3.Distance(position[currentPosition].position.transform.position,transform.position);
        if(distance <= 0.1)
        {
            timer += Time.deltaTime;
            if(timer >= position[currentPosition].waitTime)
            {
                currentPosition++;
                timer = 0;
                if(currentPosition == position.Count)
                {
                    currentPosition = position.Count - 1;
                    camMove = false;
                }
            }
        }
    }
    public void CreatePosition()
    {
        GameObject targets = new("Position");
        targets.AddComponent<Draw>();
        position.Add(new PositionSingle(targets,0));
        targets.tag = tagP;
    }
    [System.Serializable]
    public class PositionSingle
    {
        public GameObject position;
        public float waitTime;

        public PositionSingle(GameObject position, float time)
        {
            this.position = position;
            this.waitTime = time;
        }
    }
}