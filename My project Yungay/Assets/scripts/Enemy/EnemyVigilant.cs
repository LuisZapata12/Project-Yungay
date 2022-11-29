using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyVigilant : MonoBehaviour
{
    public PlayerHealth Life;
    public GameObject pointShoot;
    public Transform Target;
    public  NavMeshAgent Agent;
    public float speed;
    public EnemyWeapon Weapon;
    public float Vision;
    public Animator anim;
    public bool Near;
    public bool DetectPlayer;
    public float Timer;
    public  EnemyHealth dead;
    private FieldOfView fov;
    private Vector3 lastPosition;
    public float distance;
    public bool followLast;
    public float waitTime;
    private float timer;
    private Vector3 originalPos;
    private Vector3 directionToTarget;

    public GameObject shootStart;

    [SerializeField]
    private TrailRenderer bulletTrail;

    private float BulletSpeed = 100f;
    //public float rangeWeapon;

    void Start()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        originalPos = transform.position;
        Life = GameObject.Find("Player").GetComponent<PlayerHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer == true)
        {
            dead.CanvaHealth.SetActive(true);
        }
        else
        {
            dead.CanvaHealth.SetActive(false);
        }
        Vision = fov.viewRadius;
        if (fov.visibleTargets.Count > 0)
        {
            Target = fov.visibleTargets[0];
            lastPosition = Target.position;
        }
        else if (fov.AlertTargets.Count > 0)
        {
            Target = fov.AlertTargets[0];
            lastPosition = Target.position;
        }
        else if(fov.AlertTargets.Count == 0 && fov.visibleTargets.Count == 0)
        {
            if (Target != null)
            {
                lastPosition = Target.position;
                Target = null;
            }
        }

        if (!dead.dead)
        {

            if (Weapon != null)
            {
                ToPlayerWithWeapon();
                if (DetectPlayer)
                {
                    Shoot();
                }
            }
            else
            {

                ToPlayWithouthWeapon();

            }
        }

        if (Vector3.Distance(transform.position, originalPos) < 0.25f)
        {
            anim.SetBool("Run", false);
        }
    }
    public void ToPlayerWithWeapon()
    {
        if (Target != null)
        {
            if (Vector3.Distance(transform.position, Target.position) < fov.viewRadius)
            {
                var lookpos = Target.position - transform.position;
                lookpos.y = 0;
                var rotation = Quaternion.LookRotation(lookpos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
                Agent.enabled = true;
                Agent.SetDestination(Target.transform.position);
                Agent.speed = speed;
                DetectPlayer = true;
                anim.SetBool("RunP", true);

                if ((Vector3.Distance(transform.position, Target.transform.position) < Weapon.Range/*6f*/))
                {
                    Near = true;
                    Agent.enabled = false;
                    anim.SetBool("Iddlep", true);
                    anim.SetBool("RunP", false);
                }
                else
                {
                    Near = false;
                    Agent.enabled = true;
                    anim.SetBool("Iddlep", false);
                    anim.SetBool("RunP", true);

                }
            }
            else
            {
                DetectPlayer = false;
                anim.SetBool("RunP", false);
                Agent.enabled = false;

            }
        }
       
    }
    public void ToPlayWithouthWeapon()
    {
        if (Target != null)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) < fov.viewRadius)
            {
                var lookpos = Target.transform.position - transform.position;
                lookpos.y = 0;
                var rotation = Quaternion.LookRotation(lookpos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
                Agent.enabled = true;
                Agent.SetDestination(Target.transform.position);
                Agent.speed = speed;
                DetectPlayer = true;
                anim.SetBool("Run", true);



                if ((Vector3.Distance(transform.position, Target.transform.position) < 1.2f))
                {
                    Near = true;
                    Agent.enabled = false;

                    anim.SetBool("Run", false);
                    anim.SetBool("Atack", true);
                }
                else
                {
                    Near = false;
                    Agent.enabled = true;
                    anim.SetBool("Atack", false);
                }
            }

            else if (DetectPlayer)
            {
                distance = Vector3.Distance(transform.position, lastPosition);
                if (distance > 0.26f)
                {
                    Agent.SetDestination(lastPosition);
                    anim.SetBool("Run", true);
                    followLast = true;
                }
                else
                {
                    anim.SetBool("Run", false);
                    timer += Time.deltaTime;
                    if (timer >= waitTime)
                    {
                        lastPosition = Vector3.zero;
                        anim.SetBool("Run", true);
                        DetectPlayer = false;
                        Agent.SetDestination(originalPos);
                        timer = 0f;
                    }
                }

            }

        }
        else if (DetectPlayer && Target == null)
        {
            distance = Vector3.Distance(transform.position, lastPosition);
            if (distance > 0.26f)
            {
                Agent.SetDestination(lastPosition);
                anim.SetBool("Run", true);
                followLast = true;
            }
            else
            {
                anim.SetBool("Run", false);
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    lastPosition = Vector3.zero;
                    anim.SetBool("Run", true);
                    DetectPlayer = false;
                    Agent.SetDestination(originalPos);
                    timer = 0f;
                }
            }

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
                        AudioManager.Instance.PlaySFX("Dmr_Shoot");
                        TrailRenderer trail = Instantiate(bulletTrail, shootStart.transform.position, Quaternion.identity);
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
