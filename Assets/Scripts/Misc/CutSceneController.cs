using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{

    public GameObject[] OnAfterCutScene;

    public void Start()
    {
        StartCoroutine("WaitForCutscene");
    }

    IEnumerator WaitForCutscene()
    {
        yield return new WaitForSeconds(61);
        foreach (GameObject obj in OnAfterCutScene)
        {
            obj.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(3);
        PlayerDeathController.UnpausePlayer();
        Destroy(gameObject, 3f);
    }

}
