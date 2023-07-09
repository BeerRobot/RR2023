using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Loader : MonoBehaviour
{
    public static Loader inst;
    public CanvasGroup transition;

    private void Start()
    {
        if (!inst)
            inst = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public static void Load(string name)
    { 
        inst.LoadScene(name);
    }

    public void LoadScene(string name)
    {
        StartCoroutine(DoLoadScene(name));
    }

    IEnumerator DoLoadScene(string name)
    {
        float timer = 0;
        while (timer < 1)
        {
            transition.alpha = timer;
            timer += 1 * Time.deltaTime;
            yield return null;
        }

        AsyncOperation loading = SceneManager.LoadSceneAsync(name);
        loading.allowSceneActivation = false;
        while (loading.progress < 0.9f)
            yield return null;
        loading.allowSceneActivation = true;


        while (timer > 0)
        {
            transition.alpha = timer;
            timer -= 3 * Time.deltaTime;
            yield return null;
        }



    }

}
