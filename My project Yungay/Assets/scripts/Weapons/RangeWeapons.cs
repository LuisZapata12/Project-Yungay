using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapons : MonoBehaviour
{
    public Camera cam;
    public GameObject beggin;

    private float BulletSpeed = 100f;

    [SerializeField]
    private TrailRenderer bulletTrail;

    public Munition munition;

    public bool lockWeapons, ammoPistol;

    private float lastShootTime;

    private Vector2 BulletSpreadVariance;

    public GameObject sound;

    public LayerMask enemyMask; 

    public Weapon weapons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && GameManager.inPause == false)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.T) && GameManager.inPause == false && weapons.name == "Pistol")
        {
            StateAmmo();
        }
        if (Input.GetKeyDown(KeyCode.R) && GameManager.inPause == false)
        {
            //munition.RechargeAmmo();
        }
    }
    public void StateAmmo()
    {
        ammoPistol = !ammoPistol;
    }
    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        switch(weapons.name)
        {
            case "Pistol":
                if (munition.thereNails || munition.thereBullets)
                {
                    if (lastShootTime + weapons.shootDelay < Time.time)
                    {
                        if (munition.thereNails && ammoPistol == false)
                        {
                            munition.chargerNails -= 1;
                            sound.GetComponent<AudioSource>().PlayOneShot(weapons.shoot);
                            if (Physics.Raycast(ray, out hit, weapons.range, enemyMask))
                            {
                                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                                StartCoroutine(SpawnTrail(trail, hit.point));
                                if (hit.collider.CompareTag("Enemy"))
                                {
                                    hit.collider.gameObject.GetComponent<EnemyHealth>().lifeE(weapons.damage);
                                }
                                lastShootTime = Time.time;
                            }
                            else
                            {
                                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                                StartCoroutine(SpawnTrail(trail, cam.transform.forward * weapons.range));
                                lastShootTime = Time.time;
                            }
                        }
                        if (munition.thereBullets == true && ammoPistol == true)
                        {
                            munition.chargerBullets -= 1;
                            sound.GetComponent<AudioSource>().PlayOneShot(weapons.shoot);
                            if (Physics.Raycast(ray, out hit, weapons.range, enemyMask))
                            {
                                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                                StartCoroutine(SpawnTrail(trail, hit.point));
                                if (hit.collider.CompareTag("Enemy"))
                                {
                                    hit.collider.gameObject.GetComponent<EnemyHealth>().lifeE(weapons.damage);
                                }
                                lastShootTime = Time.time;
                            }
                            else
                            {
                                TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                                StartCoroutine(SpawnTrail(trail, cam.transform.forward * weapons.range));
                                lastShootTime = Time.time;
                            }
                        }
                    }
                }
                break;
            case "SubmachineGun":
                if (munition.thereBullets)
                {
                    if (lastShootTime + weapons.shootDelay < Time.time)
                    {
                        Vector3 direction = GetDirection();
                        munition.chargerBullets -= 1;
                        sound.GetComponent<AudioSource>().PlayOneShot(weapons.shoot);
                        if (Physics.Raycast(beggin.transform.position, direction, out hit, weapons.range, enemyMask))
                        {
                            TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                            StartCoroutine(SpawnTrail(trail, hit.point));
                            if (hit.collider.CompareTag("Enemy"))
                            {
                                hit.collider.gameObject.GetComponent<EnemyHealth>().lifeE(weapons.damage);
                            }
                            lastShootTime = Time.time;

                        }
                        else
                        {
                            TrailRenderer trail = Instantiate(bulletTrail, beggin.transform.position, Quaternion.identity);
                            StartCoroutine(SpawnTrail(trail, cam.transform.forward * weapons.range));
                            lastShootTime = Time.time;
                        }
                    }
                }
                break;
        }
        
    }
    private Vector3 GetDirection()
    {
        Vector3 direction = cam.transform.forward;
        float z = 0;
        direction += new Vector3(
            Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
            Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
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
