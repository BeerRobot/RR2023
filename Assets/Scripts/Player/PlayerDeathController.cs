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
    [SerializeField] private GameObject youDied;

    private void Awake()
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
            int roll = Random.Range(0, 4);
            switch (roll)
            {
                case 0:
                    rb.AddForce(Vector3.up * Random.Range(10,15), ForceMode.Impulse); 
                    break;
                case 1:
                    rb.AddForce(Vector3.right * Random.Range(10, 15), ForceMode.Impulse);
                    break;
                case 2:
                    rb.AddForce(-Vector3.right * Random.Range(10, 15), ForceMode.Impulse);
                    break;
                case 3:
                    rb.AddForce(Vector3.forward * Random.Range(10, 15), ForceMode.Impulse);
                    break;

            }
        }
        instance.youDied.SetActive(true);
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
        youDied.SetActive(false);
    }


    public static void SetSafeArea(Vector3 safeArea)
    {
        instance.safeZone = safeArea;
    }


    // Get Death State
    public static bool GetDeathState()
    {
        return instance.playerController.dead;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Die();
    }

    public static float GetForce()
    {
        return instance.playerController.currentMagnitude;
    }

    public static void PausePlayer()
    {
        instance.playerController.Pause = true;
    }

    public static void UnpausePlayer()
    {
        instance.playerController.Pause = false;
    }

}
