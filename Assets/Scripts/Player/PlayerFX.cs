using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{

    [SerializeField] private GameObject footDust;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;

    public void SpawnFootDust(int foot)
    {
        GameObject clone = Instantiate(footDust);
        if (foot == 0)
            clone.transform.position = leftFoot.position;
        else
            clone.transform.position = rightFoot.position;

        Destroy(clone, 1.5f);
    }

}
