using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementRange = 7;
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float attackChance = 50;
    [SerializeField] private float lookAtSpeed = 10;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int hp = 5;
    [SerializeField] private GameObject impact;
    [SerializeField] private CapsuleCollider collision;
    bool attacking = false;
    bool hit = false;
    bool dead = false;


    void Start()
    {
        player = PlayerDeathController.instance.transform;
    }

    void Update()
    {
        if (dead)
            return;
        if (hit)
            return;
        if (Mathf.Abs(transform.position.magnitude - player.position.magnitude) > movementRange)
            UpdateIdle();
        if (Mathf.Abs(transform.position.magnitude - player.position.magnitude) < movementRange)
            UpdateMove();
        if (Mathf.Abs(transform.position.magnitude - player.position.magnitude) < attackRange)
            UpdateAttack();
    }

    private void UpdateIdle()
    {
        if (attacking)
            return;
        animator.Play("Idle");
    }

    private void UpdateMove()
    {
        if (attacking)
            return;
        animator.Play("Run");

        UpdateRoation();

        Vector3 movePosition = player.position;
        movePosition.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, movePosition, moveSpeed * Time.deltaTime);

    }

    void UpdateRoation()
    {
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);
    }

    private void UpdateAttack()
    {
        if (attacking)
            return;


        UpdateRoation();

        int roll = Random.Range(0, 100);
        if (roll > attackChance)
        {
            animator.Play("Attack");
            attacking = true;
            Invoke("ResetAttack", 1.5f);
        }
        else
        {
            UpdateMove();
        }
            
    }

    void ResetAttack()
    {
        attacking = false;
    }

    void ResetHit()
    {
        hit = false;
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (!attacking)
            return;
        if (dead)
            return;
        if (hit)
            return;

        if (collider.name.Contains("Sword"))
        {
            GameObject clone = Instantiate(impact);
            clone.transform.position = collider.ClosestPoint(transform.position);
            Destroy(clone,2f);
            hp--;
            if (hp == 0)
            {
                dead = true;
                collision.height = 0.25f;
                animator.Play("Dead");
                return;
            }

            hit = true;
            animator.Play("Hit");
            Invoke("ResetHit", 0.5f);

        }
    }

}
