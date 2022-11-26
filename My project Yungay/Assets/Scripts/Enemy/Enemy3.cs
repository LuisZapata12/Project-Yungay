using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    public PlayerHealth Life;
    //public LayerMask Player;
    public GameObject pointShoot;
    public GameObject Target;
    //public NavMeshAgent Agent;
    public float speed;
    public EnemyWeapon Weapon;
    public float Vision;
    public Animator anim;
    public bool Near;
    public bool DetectPlayer;
    public float Timer;
    public EnemyHealth dead;
    private Vector3 directionToTarget;

    public float angle;


    [SerializeField]
    private TrailRenderer bulletTrail;

    private float BulletSpeed = 100f;
    void Start()
    {
        //Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player");
        Life = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer == true)
        {
            dead.healthBar.enabled = true;
        }
        else
        {
            dead.healthBar.enabled = false;
        }


        if (!dead.dead)
        {
            if (DetectPlayer)
            {
                Shoot();
            }
            if (Weapon != null)
            {
                ToPlayerWithWeapon();
                
            }
           
        }






    }
    public void ToPlayerWithWeapon()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < Vision)
        {
            var lookpos = Target.transform.position - transform.position;
            lookpos.y = 0;
            var rotation = Quaternion.LookRotation(lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
            //Agent.enabled = true;
            //Agent.SetDestination(Target.transform.position);
            //Agent.speed = speed;
            DetectPlayer = true;
            anim.SetBool("RunP", true);
            anim.SetBool("Iddlep", true);
            
        }
        else
        {
            DetectPlayer = false;
            anim.SetBool("RunP", false);
            //Agent.enabled = false;

        }
    }
   
    public void Shoot()
    {
        directionToTarget = (Target.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(pointShoot.transform.position, directionToTarget, out hit, Weapon.Range))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                if (Weapon.Munition > 0)
                {
                    Timer += Time.deltaTime;
                    if (Timer > Weapon.timeToShoot)
                    {
                        anim.SetBool("Shoot", true);
                        TrailRenderer trail = Instantiate(bulletTrail, pointShoot.transform.position, Quaternion.identity);
                        StartCoroutine(SpawnTrail(trail, hit.point));
                        Weapon.Munition--;
                        Timer = 0;
                        Life.Damage(Weapon.damage);

                    }
                    else
                    {
                        anim.SetBool("Shoot", false);
                    }


                }


            }

        }
        if (Weapon.Munition == 0)
        {
            anim.SetBool("Reload", true);
            Timer += Time.deltaTime;
            if (Timer >= Weapon.timetoRecharge)
            {
                Weapon.Munition += Weapon.charger;
                Timer = 0;
                anim.SetBool("Reload", false);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (Weapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pointShoot.transform.position, directionToTarget * Weapon.Range);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Vision);
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

