using UnityEngine;


// Move object back and forth
public class Move : MonoBehaviour
{
    // Private variables
    [SerializeField] private Transform startMarker;
    [SerializeField] private Transform endMarker;

    [SerializeField] private float speed = 1.0F;


    // Update
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, time);
    }
}
