using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

    [SerializeField] GameObject on;
    [SerializeField] GameObject[] off;
    // On Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player Model"))
        {
            foreach (GameObject go in off) { go.SetActive(false); }
            on.SetActive(true);
            PlayerDeathController.PausePlayer();
        }
    }
}
