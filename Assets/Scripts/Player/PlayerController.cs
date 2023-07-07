using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 mouseWorldPosition;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxVelocity = 4;

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 worldForward = Vector3.forward;
        Vector3 transformForward = rigidBody.transform.forward;

        Vector3 torque = Vector3.Cross(worldForward, transformForward);
        rigidBody.AddTorque(torque, ForceMode.Acceleration);
        rigidBody.AddForce((mouseWorldPosition - rigidBody.position) * speed * Time.fixedDeltaTime);

        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);

    }

    private void UpdateInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mouseWorldPosition = hit.point;
            mouseWorldPosition.y = 0.5f;
        }

    }

}
