using System.Collections;
using UnityEngine;


// Automatic floor spikes
// ToDo: Trigger floor spikes with pressure pads
public class FloorSpikes : MonoBehaviour
{
    // Private variables
    [SerializeField] private float intervalInSeconds;
    [SerializeField] private float pushSpeed;
    [SerializeField] private float retractSpeed;

    [SerializeField] private GameObject spikeObject;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [SerializeField] private bool isPush = false;
    [SerializeField] private bool isRetract = false;

    private float speed;
    private float startTime;
    private float journeyLength;


    // Start
    private void Start()
    {
        StartCoroutine(PushTrap());
    }


    // Update 
    void Update()
    {
        if (isPush)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            spikeObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        }
        
        if (isRetract)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            spikeObject.transform.localPosition = Vector3.Lerp(endPosition, startPosition, fractionOfJourney);
        }
    }


    // Push trap
    private IEnumerator PushTrap()
    {
        yield return null;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
        speed = pushSpeed;
        isRetract = false;
        isPush = true;
        StartCoroutine(RetractTrap());
    }


    // Retract trap
    private IEnumerator RetractTrap()
    {
        yield return new WaitForSeconds(intervalInSeconds);
        startTime = Time.time;
        journeyLength = Vector3.Distance(endPosition, startPosition);
        speed = retractSpeed;
        isPush = false;
        isRetract = true;
        yield return new WaitForSeconds(intervalInSeconds);
        StartCoroutine(PushTrap());
    }
}
