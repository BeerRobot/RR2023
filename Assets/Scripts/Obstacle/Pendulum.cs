using UnityEngine;


// Swing object
public class Pendulum : MonoBehaviour
{
    // Private variables
    [SerializeField] private float speed = 1;


    // Update
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -90), Quaternion.Euler(0, 0, 90), time);
    }
}
