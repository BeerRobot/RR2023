using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{

    [SerializeField] private GameObject footDust;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private AudioClip[] swordSlashes;
    [SerializeField] private AudioSource swordAudio;
    private float rotYDelta = 0;

    private float lastRotY = 0;

    bool canPlaySwoosh = true;
    private void Update()
    {

        if (!canPlaySwoosh)
            return;
        rotYDelta = transform.eulerAngles.y - lastRotY;

        if (!swordAudio.isPlaying && Mathf.Abs(rotYDelta) > 50)
        {
            swordAudio.clip = swordSlashes[Random.Range(0, swordSlashes.Length)];
            swordAudio.Play();
            canPlaySwoosh = false;
            lastRotY = transform.eulerAngles.y;
            Invoke("ReleaseSwoosh", 0.5f);
        }

        lastRotY = transform.eulerAngles.y;
    }

    void ReleaseSwoosh()
    {
        canPlaySwoosh = true;
    }



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
