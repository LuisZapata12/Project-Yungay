using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public PlayerHealth life;
    public GameObject pointShoot;
    public GameObject Target;
    public float speed;
    public float runToSpeed;
    public EnemyWeapon Weapon;
    public float vision;
    public Animator anim;
    public NavMeshAgent Agent;
    public float Timer;
    public EnemyHealth Dead;
    


    public Transform[] wayPoints;
     public int waypointIndex;
    Vector3 newpoint;



    public bool DetectPlayer;



    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        UpdateDestination();
        Target = GameObject.Find("Player");

        DetectPlayer = false;
    }

    // Update is called once per frame
    void Update()

    {
        if (!Dead.dead)
        {
            if (!DetectPlayer)
            {
                if (Vector3.Distance(transform.position, newpoint) < 0.1f)
                {
                    UpdateDestination();
                    IterateWayPointIndex();

                }
            }
            else
            {
                Shoot();
            }
        }
            
        
        
       


        ToPlayer();




    }
  
    void UpdateDestination()
    {
        anim.SetBool("Walk", true);
        newpoint = wayPoints[waypointIndex].position;
        Agent.SetDestination(newpoint);
        Agent.speed = speed;
        
    }
    void IterateWayPointIndex()
    {
        waypointIndex++;
        if (waypointIndex == wayPoints.Length)
        {
            waypointIndex = 0;
        }
    }

    public void ToPlayer()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < vision)
        {
            var lookpos = Target.transform.position - transform.position;
            lookpos.y = 0;
            var rotation = Quaternion.LookRotation(lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
            Agent.SetDestination(Target.transform.position);
            Agent.speed = runToSpeed;
            DetectPlayer = true;
            anim.SetBool("RunP", true);
            anim.SetBool("Walk", false);
            if (Vector3.Distance(transform.position, Target.transform.position) < 4f)
            {
                Agent.speed = 0;
                anim.SetBool("Iddlep", true);
                anim.SetBool("RunP", false);
            }
            else
            {
                Agent.speed = runToSpeed;
                anim.SetBool("Iddlep", false);
                anim.SetBool("RunP", true);
            }
        }
        else
        {
            DetectPlayer = false;
            anim.SetBool("RunP", false);
            anim.SetBool("Walk", true);
            Agent.speed = speed;
            Agent.SetDestination(newpoint);

        }
    }
    public void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(pointShoot.transform.position, pointShoot.transform.forward, out hit, Weapon.Range))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                if (Weapon.Munition > 0)
                {
                    Timer += Time.deltaTime;
                    if (Timer > Weapon.timeToShoot)
                    {
                        Weapon.Munition--;
                        Timer = 0;
                        //Life.Damage(Weapon.damage);

                    }


                }


            }

        }
        if (Weapon.Munition == 0)
        {
            Timer += Time.deltaTime;
            if (Timer >= Weapon.timetoRecharge)
            {
                Weapon.Munition += Weapon.charger;
                Timer = 0;
            }
        }
    }
}
