using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 mouseWorldPosition;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxVelocity = 4;
    [SerializeField] private float radius = 3;

    [Header("Player Model")]
    [SerializeField] private Transform playerModel;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private float lookAtSpeed;
    [SerializeField] private float followSpeed;

    [Header("Player Model")]
    [SerializeField] private Transform ikSolver;
    [SerializeField] private Transform solverParent;

    private void Update()
    {
        UpdateInput();
        UpdateIK();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdatePlayerModel();
    }

    private void UpdateMovement()
    {
        if (DistanceToMouse() < radius)
        {
            UpdateStop();
            return;
        }
        Vector3 worldForward = Vector3.forward;
        Vector3 transformForward = rigidBody.transform.forward;

        Vector3 torque = Vector3.Cross(worldForward, transformForward);
        rigidBody.AddTorque(torque, ForceMode.Acceleration);
        rigidBody.AddForce((mouseWorldPosition - rigidBody.position) * speed * Time.fixedDeltaTime);

        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);

    }

    private void UpdateStop()
    { 
        if(rigidBody.velocity.magnitude > 0.1f)
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, 0.75f * Time.fixedDeltaTime);
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
        playerModel.position = movePosition;// Vector3.Lerp(playerModel.position, movePosition, followSpeed * Time.deltaTime);

        playerAnim.SetFloat("Move", rigidBody.velocity.magnitude);
    }

    private void UpdateIK()
    {
        Vector3 lookDirection = mouseWorldPosition - solverParent.position;
        lookDirection.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        solverParent.rotation = targetRotation;// Quaternion.Lerp(solverParent.rotation, targetRotation, 100 * Time.deltaTime);

        float ikLocalZ = Mathf.Lerp(0.3f, 0.9f, DistanceToMouse());
        Vector3 ikLocalPosition = ikSolver.localPosition;
        ikLocalPosition.z = ikLocalZ;
        ikSolver.localPosition = Vector3.Lerp(ikSolver.localPosition, ikLocalPosition, 10 * Time.deltaTime);
    }

    private float DistanceToMouse()
    {
        Vector3 difference = (mouseWorldPosition) - (transform.position);
        return Mathf.Abs(difference.magnitude);
    }

}
