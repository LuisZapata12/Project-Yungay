using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapons : MonoBehaviour
{
    private Hand hand;
    public Camera cam;
    public GameObject beggin;

    private float BulletSpeed = 300f;

    [SerializeField]
    private TrailRenderer bulletTrail;

    public Munition munition;

    public bool lockWeapons, ammoPistol;

    private float lastShootTime;

    private Vector2 BulletSpreadVariance;

    public GameObject sound;

    public LayerMask enemyMask; 

    public Weapon weapons;

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<Hand>();
        munition = GetComponent<Munition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hand.canAim)
        {
            if (Input.GetMouseButton(0) && !GameManager.inPause)
            {
                if (munition.CheckAmmo(hand.currentMunition))
                {
                    //Shoot(Hand.currentItem);
                    Disparo(Hand.currentItem);
                }
            }
        }
    }

    public void Disparo(ItemObject item)
    {
        EquipmentRange _ = (EquipmentRange)Hand.currentItem;
        Vector3 direction = GetDirection(_.bulletSpreadVarianceDis);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (lastShootTime + _.shootDelay < Time.time)
        {
            if (Physics.Raycast(Camera.main.transform.position,direction, out hit, /*_.range,*/ enemyMask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point));
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().lifeE(_.damage);
                }

                if (hit.collider.CompareTag("Box"))
                {
                    hit.collider.gameObject.GetComponent<Box>().DestroyByOthers();
                }

                lastShootTime = Time.time;
                AudioManager.Instance.PlaySFX("Pistol");
            }
            else
            {
                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, cam.transform.forward * _.range));
                lastShootTime = Time.time;
                AudioManager.Instance.PlaySFX("Pistol");
            }
            munition.RestMunition(hand.currentMunition);
            inventory.RemoveSlot();
        }
    }

    private Vector3 GetDirection(float variance)
    {
        Vector2 dir = new Vector2(variance, variance);
        Vector3 direction = cam.transform.forward;
        float z = 0;
        direction += new Vector3(
            Random.Range(-dir.x, dir.x),
            Random.Range(-dir.y, dir.y),
            z
        );

        direction.Normalize();

        return direction;
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = HitPoint;

        Destroy(Trail.gameObject, Trail.time);
    }
}
