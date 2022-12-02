using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerModel model;
    public PlayerGroundCheck playerGroundCheck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && model.canJump && PlayerGroundCheck.grounded && GameManager.inPause == false && model.state != PlayerModel.State.death)/*playerGroundCheck.grounded*/
        {
            Jump();
            Invoke(nameof(ResetJump), model.jumpCooldown);
        }
    }

    private void Jump()
    {

        model.rb.velocity = new Vector3(model.rb.velocity.x, 0f, model.rb.velocity.z);

        model.rb.AddForce(transform.up * model.jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        model.canJump = true;
    }
}
