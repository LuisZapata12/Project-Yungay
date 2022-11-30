using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public float distance;
    public float nearDistance, midDistance, farDistance;
    [SerializeField]
    private TrailRenderer bulletTrail;
    private float BulletSpeed = 100f;
    public PlayerHealth Life;
    public GameObject pointShoot;
    public Transform Target;
    public NavMeshAgent Agent;
    public float speed;
    public EnemyWeapon pistol, dmr;
    public Animator anim;
    public bool Near;
    public bool DetectPlayer;
    public float Timer;
    public EnemyHealth dead;

    public GameObject wawe;
    public GameObject prefabWawe2;
    public GameObject spawnWawe2;

    public GameObject spawnDebris;
    public GameObject[] prefabDebris;
    public float timerDebris;

    void Start()
    {
        Target = GameObject.Find("Player").transform;
    }


    void Update()
    {
        distance = CalculateDistance();
        LifeEvents();

        if (distance < nearDistance)
        {
            NearAttack();
        }
        else if (distance < midDistance && distance > nearDistance)
        {
            MidAttack();
            Shoot(pistol);
        }
        else if(distance > midDistance && distance < farDistance)
        {
            FarAttack();
            Shoot(dmr);
        }
    }
    private void LifeEvents()
    {
        if (dead.life <= dead.healthMax && dead.life > (dead.healthMax * 75) / 100)
        {
            wawe.SetActive(true);
          
        }
        if (dead.life <= (dead.healthMax * 75) / 100 && dead.life > (dead.healthMax * 50) / 100)
        {
            if (spawnWawe2.transform.childCount <=0)
            {
                var wawe = Instantiate(prefabWawe2, spawnWawe2.transform.position, Quaternion.identity);
                wawe.transform.parent = spawnWawe2.transform;
            }
        }
        if (dead.life <= (dead.healthMax * 50 /100))
        {
            Debris();
        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    private void NearAttack()
    {
        anim.SetBool("RunP", false);
        anim.SetBool("Iddlep", false);
        anim.SetBool("Shoot", false);
        anim.SetBool("Reload", false);
        var lookpos = Target.transform.position - transform.position;
            lookpos.y = 0;
            var rotation = Quaternion.LookRotation(lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
            Agent.enabled = true;
            Agent.SetDestination(Target.transform.position);
            Agent.speed = speed;
            DetectPlayer = true;
            anim.SetBool("Run", true);



            if ((Vector3.Distance(transform.position, Target.transform.position) < 1.5f))
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

    private void MidAttack()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Atack", false);
        var lookpos = Target.position - transform.position;
        lookpos.y = 0;
        var rotation = Quaternion.LookRotation(lookpos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
        Agent.enabled = true;
        Agent.speed = speed;
        DetectPlayer = true;
        anim.SetBool("RunP", true);

        if ((Vector3.Distance(transform.position, Target.transform.position) < midDistance))
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

    private void FarAttack()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Atack", false);
        var lookpos = Target.position - transform.position;
        lookpos.y = 0;
        var rotation = Quaternion.LookRotation(lookpos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
        Agent.enabled = true;
        Agent.speed = speed;
        DetectPlayer = true;
        anim.SetBool("RunP", true);

        if ((Vector3.Distance(transform.position, Target.transform.position) < farDistance))
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

    public void Shoot(EnemyWeapon weapon)
    {
        RaycastHit hit;
        if (Physics.Raycast(pointShoot.transform.position, pointShoot.transform.forward, out hit, weapon.Range))
        {
            Debug.Log("Rayo");
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player");
                if (weapon.Munition > 0)
                {

                    Debug.Log("Municion");
                    Timer += Time.deltaTime;
                    if (Timer > weapon.timeToShoot)
                    {
                        anim.SetBool("Shoot", true);
                        weapon.Munition--;
                        Timer = 0;
                        Life.Damage(weapon.damage);

                    }
                    else

                    {
                        anim.SetBool("Shoot", false);
                    }
                }
            }

        }
        if (weapon.Munition == 0)
        {
            anim.SetBool("Reload", true);
            Timer += Time.deltaTime;
            if (Timer >= weapon.timetoRecharge)
            {
                weapon.Munition += weapon.charger;
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


    private void Debris()
    {
        timerDebris += Time.deltaTime;
        if (timerDebris >= 2f)
        {
            int randomNumber = Random.Range(0, prefabDebris.Length);
            var debris = Instantiate(prefabDebris[randomNumber], spawnDebris.transform.position, Quaternion.identity);
            debris.transform.parent = spawnDebris.transform;
            timerDebris = 0f;
        }
    }
}
