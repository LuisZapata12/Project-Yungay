using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public float distance;
    public float nearDistance, midDistance, farDistance;
    public Transform target;
    [SerializeField]
    private TrailRenderer bulletTrail;
    private float BulletSpeed = 100f;
    public PlayerHealth Life;
    public GameObject pointShoot;
    public Transform Target;
    public NavMeshAgent Agent;
    public float speed;
    public EnemyWeapon Weapon;
    public Animator anim;
    public bool Near;
    public bool DetectPlayer;
    public float Timer;
    public EnemyHealth dead;



    void Start()
    {

    }


    void Update()
    {
        distance = CalculateDistance();
        if (distance < nearDistance)
        {
            NearAttack();
        }
        else if (distance < midDistance && distance > nearDistance)
        {

        }
        else
        {

        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    private void NearAttack()
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
