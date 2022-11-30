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
    public Material oldColor;
    private bool once;
    private GameObject item;
    public LayerMask layer;
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
       // Gizmos.DrawRay(cam.transform.position, cam.transform.forward * rayDistance);

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

        if (Physics.Raycast(ray, out hit, rayDistance,layer) && hit.collider.gameObject.GetComponent<Loot>())
        {
            var loot = hit.collider.gameObject.GetComponent<Loot>();

            if (CheckLoot(loot))
            {
                if (!hit.collider.CompareTag("Enemy"))
                {
                    if (!once)
                    {
                        oldColor = hit.collider.GetComponent<Renderer>().material; 
                        once = true;
                    }
                    item = hit.collider.gameObject;
                    var _ = new Material(oldColor);
                    _.color = changeColor;
                    hit.collider.GetComponent<Renderer>().material = _;
                }

                lootText.GetComponent<Animator>().SetBool("Show", true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    for (int i = 0; i < loot.loot.Count; i++)
                    {
                        var _ = loot.loot[i];
                        inventory.AddItem(_, _.item, _.amount, _.durability);
                    }
                    loot.DeleteObject();
                    lootText.GetComponent<Animator>().SetBool("Show", false);
                }
            }
        }
        else
        {
            lootText.GetComponent<Animator>().SetBool("Show", false);

            if (once && item != null && oldColor != null)
            {
                item.GetComponent<Renderer>().material = oldColor;
                once = false;
            }
        }
    }
}
