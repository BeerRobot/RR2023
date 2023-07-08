using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class PlayerDeathController : MonoBehaviour
{
    public static PlayerDeathController instance;

    private Vector3 safeZone;

    [SerializeField] private Rigidbody[] ragDoll;
    [SerializeField] private FullBodyBipedIK iK;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;


    private void Start()
    {
        if (!instance)
            instance = this;

        safeZone = transform.position;

    }

    public static void Die()
    {
        instance.playerController.dead = true;
        instance.iK.enabled = false;
        instance.animator.enabled = false;
        foreach (Rigidbody rb in instance.ragDoll)
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        instance.Respawn();

    }

    public void Respawn()
    {
        Invoke("DelayRespawn", 5f);
    }

    void DelayRespawn()
    {
        foreach (Rigidbody rb in instance.ragDoll)
        {
            rb.isKinematic = true;
        }
        playerController.dead = false;
        iK.enabled = true;
        animator.enabled = true;
        safeZone.y += 1;
        transform.position = safeZone;
        playerController.transform.position = safeZone;
    }


    public static void SetSafeArea(Vector3 safeArea)
    {
        instance.safeZone = safeArea;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Die();
    }


}
