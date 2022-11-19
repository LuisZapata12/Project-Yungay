using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Look : MonoBehaviour
{
    public Camera camera;
    private EquipmentRange weapon;
    public GameObject init;
    private Vector3 end;
    private Vector3 endPosition;
    public GameObject look;
    public LayerMask collision;
    public LayerMask no;
    public float smooth;
    public bool zoom = false;
    private Coroutine coroutine;
    private Animator anim;
    public Sprite weaponCursor,aimCursor;
    public CinemachineCameraOffset offset;
    public float zoomValue;
    private bool once;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hand.canAim)
        {
            weapon = Hand.currentItem as EquipmentRange;

            UpdateLimit();

            float distance1 = Vector3.Distance(camera.transform.position, endPosition);
            float distance2 = Vector3.Distance(camera.transform.position, init.transform.position);


            if (Input.GetMouseButtonDown(1) && weapon != null)
            {
                IsPoint();
                zoom = true;
                anim.Play("Aim_Pistol");
                anim.SetBool("isAim", true);
                Hand.imageCursor.sprite = aimCursor;
            }

            if (Input.GetMouseButtonUp(1))
            {
                zoom = false;
                anim.SetBool("isAim", false);
                Hand.imageCursor.sprite = weaponCursor;
            }


            if (zoom && !once)
            {
                StartCoroutine(Zoom(zoomValue));
                once = true;
            }

            if (!zoom && once)
            {
                StartCoroutine(Back());
                once = false;
            }
            //if (zoom && offset.m_Offset.z != zoomValue)
            //{

            //    offset.m_Offset.z += Time.deltaTime * smooth;

            //}
            //else if(!zoom && offset.m_Offset.z == zoomValue)
            //{
            //    //camera.transform.position = Vector3.Lerp(camera.transform.position, init.transform.position, Time.deltaTime * smooth);
            //    //if (distance2 < 0.01f)
            //    //{
            //    //    camera.transform.position = init.transform.position;
            //    //}

            //    offset.m_Offset.z -= Time.deltaTime * smooth;
            //}
        }
    }

    public void IsPoint()
    {
        //look.GetComponent<RawImage>().texture = weapon.look.texture;
    }

    private void UpdateLimit()
    {
        weapon = Hand.currentItem as EquipmentRange;
        RaycastHit hit;
        if (weapon?true:false)
        {
            Ray ray = new Ray(init.transform.position, init.transform.forward * weapon.zoom);
            end = ray.origin + ray.direction * weapon.zoom;
            endPosition = end;
            if (Physics.Raycast(init.transform.position, init.transform.forward, out hit, weapon.zoom, collision))
            {
                if (hit.point != null)
                {
                    endPosition = hit.point;
                }
            }
        }
    }

    IEnumerator Zoom(float value)
    {
        while(offset.m_Offset.z < value)
        {
            offset.m_Offset.z += Time.deltaTime * smooth;
            yield return new WaitForEndOfFrame();
        }
        offset.m_Offset = new Vector3(0f, 0f, value);
        yield break;
    }

    IEnumerator Back()
    {
        while (offset.m_Offset.z > 0)
        {
            offset.m_Offset.z -= Time.deltaTime * smooth;
            yield return new WaitForEndOfFrame();
        }
        offset.m_Offset = Vector3.zero;
        yield break;
    }
}