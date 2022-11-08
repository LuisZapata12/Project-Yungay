using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    public PlayerModel model;
    public PlayerHeadCheck playerHeadCheck;
    //public PlayerGroundCheck playerGroundCheck;
    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<PlayerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerGroundCheck.grounded/*playerGroundCheck.grounded*/)
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!model.isCrouching)
            {
                model.isCrouching = true;
                model.state = PlayerModel.State.crounching;
                model.canJump = false;
                model.cap.height = model.crouchHeight;
                model.cap.center = new Vector3(model.cap.center.x, model.CrouchY, model.cap.center.z);
            }
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) && model.isCrouching && playerHeadCheck.headCheck == false)
        {

            model.state = PlayerModel.State.idle;
            model.isCrouching = false;
            model.canJump = true;
            model.cap.height = model.standHeight;
            model.cap.center = new Vector3(model.cap.center.x, model.standY, model.cap.center.z);
        }

        else if (!Input.GetKey(KeyCode.LeftControl) && model.isCrouching && playerHeadCheck.headCheck == false)
        {
            model.isCrouching = false;
            model.state = PlayerModel.State.idle;
            model.canJump = true;
            model.cap.height = model.standHeight;
            model.cap.center = new Vector3(model.cap.center.x, model.standY, model.cap.center.z);
        }
        //else if (model.isCrouching && playerHeadCheck.headCheck == false)
        //{
        //    model.isCrouching = false;
        //    model.canJump = true;
        //    model.cap.height = model.standHeight;
        //    model.cap.center = new Vector3(model.cap.center.x, model.standY, model.cap.center.z);
        //}

    }
}
