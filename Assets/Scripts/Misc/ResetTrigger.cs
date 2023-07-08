using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    public Transform resetPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player Model"))
        {
            PlayerDeathController.SetSafeArea(resetPoint.position);
        }
    }
}
