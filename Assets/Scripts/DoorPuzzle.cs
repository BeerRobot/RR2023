using System.Collections;
using UnityEngine;


// Door "puzzle"
public class DoorPuzzle : MonoBehaviour
{
    // Private variables
    [SerializeField] private Rigidbody weightRB;

    [SerializeField] private MeshCollider weightCollider;
    [SerializeField] private MeshCollider doorCollider;

    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private int weightHP = 5;
    [SerializeField] private GameObject impactFXPrefab;
    [SerializeField] private Transform impactFXParent;

    [SerializeField] private GameObject chain;
    [SerializeField] private float chainMoveSpeed = 2f;

    private bool isOpen = false;

    public GameObject vCam;
    public AudioSource audio;

    public bool forceOff = false;


    // Update
    private void Update()
    {
        if (isOpen)
        {
            chain.transform.Translate(Vector3.up * chainMoveSpeed * Time.deltaTime);
        }
    }


    // On Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Sword"))
        {
            GameObject clone = Instantiate(impactFXPrefab, impactFXParent);
            Destroy(clone, .5f);
            weightHP--;
            if (weightHP == 0)
            {
                isOpen = true;
                weightRB.isKinematic = false;
                weightCollider.isTrigger = false;
                doorCollider.enabled = false;
                leftDoor.localEulerAngles = new Vector3(0, -74, 0);
                rightDoor.localEulerAngles = new Vector3(0, 74, 0);
                if (forceOff)
                {
                    rightDoor.gameObject.SetActive(false);
                    leftDoor.gameObject.SetActive(false);
                }
                StartCoroutine(DoSlovePuzzle());
                audio.Play();
            }
        }
    }

    IEnumerator DoSlovePuzzle()
    {
        PlayerDeathController.PausePlayer();
        yield return new WaitForSeconds(1.5f);
        vCam.SetActive(true);
        yield return new WaitForSeconds(3);
        vCam.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        PlayerDeathController.UnpausePlayer();

    }

}
