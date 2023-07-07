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

    [Header("Player Model")]
    [SerializeField] private Transform playerModel;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private float lookAtSpeed;
    [SerializeField] private float followSpeed;

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdatePlayerModel();
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

    private void UpdatePlayerModel()
    {
        Vector3 lookDirection = transform.position - playerModel.position;
        lookDirection.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        playerModel.rotation = Quaternion.Lerp(playerModel.rotation, targetRotation, lookAtSpeed * Time.deltaTime);

        Vector3 movePosition = transform.position;
        movePosition.y = playerModel.position.y;
        playerModel.position = Vector3.Lerp(playerModel.position, movePosition, followSpeed * Time.deltaTime);

        playerAnim.SetFloat("Move", rigidBody.velocity.magnitude);

    }

}
