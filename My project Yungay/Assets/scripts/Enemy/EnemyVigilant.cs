using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVigilant : MonoBehaviour
{
    public PlayerHealth Life;
    //public LayerMask Player;
    public GameObject pointShoot;
    public GameObject Target;
    public  NavMeshAgent Agent;
    public float speed;
    public EnemyWeapon Weapon;
    public float Vision;
    public Animator anim;
    public bool Near;
    public bool DetectPlayer;
    public float Timer;
    public  EnemyHealth dead;
    
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Vector3.Distance(transform.position, Target.transform.position) < Vision)
        {
            var lookpos = Target.transform.position - transform.position;
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
        if (Vector3.Distance(transform.position, Target.transform.position) < Vision)
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



            if ((Vector3.Distance(transform.position, Target.transform.position) < 2f))
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
                        Debug.Log("Shot");
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
                Debug.Log("Masmunicion");
            }
        }
    }
  

    private void OnDrawGizmos()
    {
        if (Weapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pointShoot.transform.position, pointShoot.transform.forward * Weapon.Range);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Vision);
    }



}
