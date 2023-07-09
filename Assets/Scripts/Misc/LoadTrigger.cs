using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player Model"))
        {
            Loader.inst?.LoadScene(sceneName);
        }
    }

}
