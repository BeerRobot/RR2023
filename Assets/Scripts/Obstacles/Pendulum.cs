using UnityEngine;


// Swing object
public class Pendulum : MonoBehaviour
{
    // Private variables
    [SerializeField] private float speed = 1;

    [SerializeField] private bool reverse = false;


    // Update
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);

        if (!reverse)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -90), Quaternion.Euler(0, 0, 90), time);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0, -90), time);
        }
    }
}
