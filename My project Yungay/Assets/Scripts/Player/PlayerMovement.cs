using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerModel model;
    //public PlayerGroundCheck playerGroundCheck;
    public Transform orientation;

    private float hor;
    private float ver;

    public float airMultiplier;

    public Animator anim;

    public Vector3 move;
    Vector3 checkMove = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        model.rb.freezeRotation = true;
        model.states = new Dictionary<PlayerModel.State,PlayerModel.OnState >()
        {
            { PlayerModel.State.idle, Iddle },
            { PlayerModel.State.move, Movement },
            { PlayerModel.State.walk, Walk },
            { PlayerModel.State.run, Run }
        };
    }

    private void Update()
    {
        model.states[model.state]?.Invoke();


        ControlSpeed();
            
        model.actualSpeed = model.rb.velocity.magnitude;
       
        Iddle();

        if (GameManager.inPause)
        {
            model.sourceSound.SetActive(false);
        }
    }
    void FixedUpdate()
    {
    }

    private void Iddle()
    {
        if(GameManager.inPause == false && model.isDeath == false)
        {
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");
            Movement(); 
        }
        else
        {
            move = checkMove;
        }

    }

    private void Movement()
    {
        move = orientation.forward * ver + orientation.right * hor;
        if (move == checkMove)
        {
            model.state = PlayerModel.State.idle;
            if (hor == 0 || ver == 0)
            {
                model.sourceSound.SetActive(false);
            }
        }
        else if (move != checkMove)
        {
            if (PlayerGroundCheck.grounded/*playerGroundCheck.grounded*/)
            {

                if (Input.GetKey(KeyCode.LeftShift) && model.staActual >= 0 && !model.isCrouching)
                {
                    //Run();
                    model.state = PlayerModel.State.run;
                }

                else
                {
                    // Walk();
                    model.state = PlayerModel.State.walk;
                }
            }

            else if (!PlayerGroundCheck.grounded/*playerGroundCheck.grounded*/)
            {
                model.rb.AddForce(move.normalized * model.speedWalk * 10f * airMultiplier, ForceMode.Force);

            }

            if (hor != 0 || ver != 0)
            {
                model.sourceSound.SetActive(true);
            }
        }
    }

    private void Walk()
    {
        model.state = PlayerModel.State.walk;
        model.rb.AddForce(move.normalized * model.speedWalk * 10f, ForceMode.Force);
        model.isRunning = false;
        if(model.actualSpeed < model.speedWalk)
        {
            model.state = PlayerModel.State.idle;
        }
    }
    private void Run()
    {
        model.state = PlayerModel.State.run;
        model.rb.AddForce(move.normalized * model.speedRun * 10f, ForceMode.Force);
        if (model.actualSpeed >=4.5)
        {
            model.isRunning = true;
        }
        else
        {
            model.isRunning = false;
            model.state = PlayerModel.State.idle;
        }
        
        
            
    }
    private void ControlSpeed()
    {
        Vector3 flatVel = new Vector3(model.rb.velocity.x, 0f, model.rb.velocity.z);

        if(model.isRunning)
        {
            if (flatVel.magnitude > model.speedRun)
            {
                Vector3 limitedVel = flatVel.normalized * model.speedRun;
                model.rb.velocity = new Vector3(limitedVel.x, model.rb.velocity.y, limitedVel.z);
            }
        }
        else
        {
            if (flatVel.magnitude > model.speedWalk)
            {
                Vector3 limitedVel = flatVel.normalized * model.speedWalk;
                model.rb.velocity = new Vector3(limitedVel.x, model.rb.velocity.y, limitedVel.z);
            }
        }
        
    }
}
