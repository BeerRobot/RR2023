using System.Collections;
using UnityEngine;


// Pressure Pad wall trap
public class PressurePad : MonoBehaviour
{
    // Private variables
    [SerializeField] private float intervalInSeconds;
    [SerializeField] private float pushSpeed;
    [SerializeField] private float retractSpeed;

    [SerializeField] private GameObject wall;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    private bool isPush = false;
    private bool isRetract = false;

    private float speed;
    private float startTime;
    private float journeyLength;


    // Update
    private void Update()
    {
        if (isPush)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            wall.transform.localPosition = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        }

        if (isRetract)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            wall.transform.localPosition = Vector3.Lerp(endPosition, startPosition, fractionOfJourney);
        }
    }


    // On Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player Model"))
        {
            if (!PlayerDeathController.GetDeathState())
            {
                Invoke("TriggerWall", 2f);
            }
        }
    }


    // Trigger Wall
    private void TriggerWall()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
        speed = pushSpeed;
        isRetract = false;
        isPush = true;
        StartCoroutine(RetractWall());
    }


    // Retract wall
    private IEnumerator RetractWall()
    {
        yield return new WaitForSeconds(intervalInSeconds);
        startTime = Time.time;
        journeyLength = Vector3.Distance(endPosition, startPosition);
        speed = retractSpeed;
        isPush = false;
        isRetract = true;
    }
}
