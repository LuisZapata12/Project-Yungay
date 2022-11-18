using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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

    }
    public void ToPlayerWithWeapon()
    {
        if (Vector3.Distance(transform.position, Target.position) < Vision)
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

            if ((Vector3.Distance(transform.position, Target.transform.position) < Weapon.Range))
            {
                Near = true;
                Agent.enabled = false;
                anim.SetBool("Iddlep",true);
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
    public void Shoot()
    {

        RaycastHit hit;
        if  (Physics.Raycast (pointShoot.transform.position,pointShoot.transform.forward,out hit ,Weapon.Range))
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
                        Life.Damage(Weapon.damage);

                    }
                }
            }

        }
        if (Weapon.Munition == 0)
        {
            Timer += Time.deltaTime;
            if (Timer>=Weapon.timetoRecharge)
            {
                Weapon.Munition +=Weapon.charger;
                Timer = 0;
            }
        }
    }
}
