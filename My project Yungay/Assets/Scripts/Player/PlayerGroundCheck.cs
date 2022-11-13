using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public PlayerModel model;

    [Header("GroundCheck")]
    public float height;
    public float prueba;
    public LayerMask Ground;
    public static bool grounded;
    public static bool jump;
    public float groundDrag;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, Ground);

        jump = Physics.Raycast(transform.position, Vector3.down, 1f, Ground);

        if (grounded)
        {
            model.rb.drag = groundDrag;
        }
        else
        {
            model.rb.drag = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, Vector3.down * prueba);
    }
}
