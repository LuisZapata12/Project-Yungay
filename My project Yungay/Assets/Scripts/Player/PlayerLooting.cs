using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooting : MonoBehaviour
{
    private Camera cam;
    private Inventory inventory;
    public GameObject lootText;
    public float rayDistance;
    public Color changeColor;
    private Color oldColor;
    private bool once;
    private GameObject item;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = GetComponent<Inventory>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Loot();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * rayDistance);

    }

    private bool CheckLoot(Loot loot)
    {
        bool canloot = false;

        for (int i = 0; i < loot.loot.Count; i++)
        {
            if (loot.loot[i].amount > 0)
            {
                canloot = true;
                break;
            }
        }
        return canloot;
    }

    private void Loot()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.gameObject.GetComponent<Loot>())
        {
            var loot = hit.collider.gameObject.GetComponent<Loot>();

            if (CheckLoot(loot))
            {
                if (!once)
                {
                    oldColor = hit.collider.GetComponent<Renderer>().material.color;
                    once = true;
                }

                item = hit.collider.gameObject;
                hit.collider.GetComponent<Renderer>().material.color = changeColor;

                lootText.GetComponent<Animator>().SetBool("Show", true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    for (int i = 0; i < loot.loot.Count; i++)
                    {
                        var _ = loot.loot[i];
                        inventory.AddItem(_, _.item, _.amount);
                    }
                    loot.DeleteObject();
                    lootText.GetComponent<Animator>().SetBool("Show", false);
                }
            }
        }
        else
        {
            lootText.GetComponent<Animator>().SetBool("Show", false);

            if (once && item != null)
            {
                item.GetComponent<Renderer>().material.color = oldColor;
                once = false;
            }
        }
    }
}
