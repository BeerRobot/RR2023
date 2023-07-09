using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine("WaitForCutscene");
    }

    IEnumerator WaitForCutscene()
    {
        yield return new WaitForSeconds(30);
        if (Loader.inst)
            Loader.Load("Menu");
    }

}
