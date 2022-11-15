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
    private Transform player;
    public Transform lastPosition;
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.visibleTargets.Count > 0)
        {
            Target = fov.visibleTargets[0];
        }
        else if (fov.AlertTargets.Count > 0)
        {
            Target = fov.AlertTargets[0];
        }
        else if(fov.AlertTargets.Count == 0 && fov.visibleTargets.Count == 0 && Target != null)
        {
            lastPosition = Target;
            Target = null;
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
        if (Vector3.Distance(transform.position, Target.transform.position) < Vision && Target != null)
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
        else if(lastPosition != null)
        {
            if ((Vector3.Distance(transform.position, lastPosition.position) > 0.1f))
            {
                Agent.SetDestination(lastPosition.position);
                anim.SetBool("Run", true);
                Debug.Log(Vector3.Distance(transform.position, lastPosition.position));
            }
            else
            {
                lastPosition = null;
            }

        }
        else
        {
            anim.SetBool("Run", false);
            DetectPlayer = false;
            Agent.enabled = false;
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
