using UnityEngine;


// Rotate object
public class Rotate : MonoBehaviour
{
    // Private variables
    [SerializeField] private bool xAngle;
    [SerializeField] private bool yAngle;
    [SerializeField] private bool zAngle;

    [SerializeField] private float speed;


    // Update
    void Update()
    {
        transform.Rotate(xAngle ? 1 : 0 * (speed * Time.deltaTime), 
                         yAngle ? 1 : 0 * (speed * Time.deltaTime), 
                         zAngle ? 1 : 0 * (speed * Time.deltaTime), Space.Self);
    }
}
