using UnityEngine;


// Enums
public enum RotateAxis
{ 
    None,
    X,
    Y,
    Z
}


// Rotate object
public class Rotate : MonoBehaviour
{
    // Private variables
    [SerializeField] private RotateAxis axis;

    [SerializeField] private float speed;


    // Update
    void Update()
    {
        switch (axis)
        {
            case RotateAxis.X:
                transform.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime);
                break;
            case RotateAxis.Y:
                transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
                break;
            case RotateAxis.Z:
                transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
                break;
        }
    }
}
