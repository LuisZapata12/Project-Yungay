using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapons : MonoBehaviour
{
    private Hand hand;
    public Camera cam;
    public GameObject beggin;

    private float BulletSpeed = 100f;

    [SerializeField]
    private TrailRenderer bulletTrail;

    public Munition munition;

    public bool lockWeapons, ammoPistol;

    private float lastShootTime;

    public GameObject sound;

    public LayerMask enemyMask; 

    public Weapon weapons;

    public Inventory inventory;
    private string detect;

    private float timer;
    bool a;
    private bool hasShoot;
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
                    Disparo(Hand.currentItem);
                    
                }
            }

        }

        if (Hand.currentItem != null)
        {
            EquipmentRange _ = Hand.currentItem as EquipmentRange;
            if (_ != null)
            {
                if (hasShoot && lastShootTime + _.shootDelay > Time.time)
                {
                    timer = timer + Time.deltaTime;
                    if (timer >= 0.2f)
                    {
                        if(Hand.munitionIndex == 0)
                        {
                            a = true;
                            timer = 0;
                        }
                        if (Hand.munitionIndex == 1 && !a)
                        {
                            timer = 0;
                            AudioManager.Instance.PlaySFX("Pistol-case");
                            hasShoot = false;
                        }
                    }
                    else if(timer == 0)
                    {
                        a = false;
                    }
                }
            }
        }

    }

    public void Disparo(ItemObject item)
    {
        EquipmentRange _ = Hand.currentItem as EquipmentRange;
        Vector3 direction = GetDirection(_.bulletSpreadVarianceDis);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (lastShootTime + _.shootDelay < Time.time)
        {
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, enemyMask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                hasShoot = true;
                detect = hit.collider.tag;
                switch (detect)
                {
                    case "Enemy":
                        StartCoroutine(SpawnTrail(trail, hit.point, true, hit, _));
                        break;

                    default:
                        StartCoroutine(SpawnTrail(trail, hit.point, false, hit, _));
                        break;
                }
               
                lastShootTime = Time.time;
                if (Hand.munitionIndex == 1)
                {
                    int index;
                    index = (int) Random.Range(0, 5); 
                    switch (index)
                    {
                        case 1:
                              AudioManager.Instance.PlaySFX(_.fail);
                            break;
                        default:
                            AudioManager.Instance.PlaySFX(_.shoot);
                            break;
                    }
                }
                else if (Hand.munitionIndex == 0)
                {
                    int index;
                    index = (int)Random.Range(0,5);
                    switch (index)
                    {
                        case 1:
                            AudioManager.Instance.PlaySFX(_.fail);
                            break;
                        default:
                            if (Hand.currentItem == hand.weaponSlots[0].weapon)
                            {
                                AudioManager.Instance.PlaySFX("Pistol-nails");
                            }
                            else
                            {
                                AudioManager.Instance.PlaySFX(_.shoot);
                            }
                            break;
                    }
                }
            }
            else
            {
                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, beggin.transform.forward, false, hit, _));
                lastShootTime = Time.time;
                if (Hand.munitionIndex == 1)
                {
                    int index;
                    index = (int)Random.Range(0, 5);
                    switch (index)
                    {
                        case 1:
                            AudioManager.Instance.PlaySFX(_.fail);
                            break;
                        default:
                            AudioManager.Instance.PlaySFX(_.shoot);
                            break;
                    }
                }
                else if (Hand.munitionIndex == 0)
                {
                    int index;
                    index = (int)Random.Range(0, 5);
                    switch (index)
                    {
                        case 1:
                            AudioManager.Instance.PlaySFX(_.fail);
                            break;
                        default:
                            if (Hand.currentItem == hand.weaponSlots[0].weapon)
                            {
                                AudioManager.Instance.PlaySFX("Pistol-nails");
                            }
                            else
                            {
                                AudioManager.Instance.PlaySFX(_.shoot);
                            }
                            break;
                    }
                }
            }

            if (!ConsoleCheats.unlimitedAmmo)
            {
                munition.RestMunition(hand.currentMunition);
            }
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
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, bool Impact,RaycastHit hit,EquipmentRange _)
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
        if(Impact)
        {
            hit.collider.gameObject.GetComponent<EnemyHealth>().lifeE(_.damage);
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
