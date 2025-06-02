using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerTrackingReader : MonoBehaviour
{
    [Header("Input Actions (from Input Action Asset)")]
    public ControllerSide controllerSide;
    public InputActionReference positionAction;
    public InputActionReference rotationAction;

    public InputActionReference triggerAction;
    public InputActionReference gripAction;

    [Header("References")]
    public Transform player, camera;
    public Rigidbody playerRB;
    public LineRenderer lr;
    public LayerMask whatIsGrappleable;
    public Transform reticle;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    private Vector3 lastPosition = Vector3.zero;
    private Vector3 position, velocity, globalPosition, forward;

    private bool isSwinging = false;
    private bool isGrappled = false;

    public enum ControllerSide
    {
        Left = -1,
        Right = 1
    }

    void Update()
    {
        position = positionAction.action.ReadValue<Vector3>();
        Quaternion rotation = rotationAction.action.ReadValue<Quaternion>();
        velocity = position - lastPosition;
        lastPosition = position;
        globalPosition = position + player.position;
        forward = rotation * Vector3.forward;

        float trigger = triggerAction.action.ReadValue<float>();
        float grip = gripAction.action.ReadValue<float>();


        if (trigger > 0.8f && !isSwinging) StartSwing();
        if (trigger < 0.8f && isSwinging) StopSwing();

        RaycastHit hit;
        if (Physics.Raycast(globalPosition, forward, out hit, maxSwingDistance))
        {
            reticle.position = hit.point;
            reticle.gameObject.SetActive(true);
            reticle.localScale = Vector3.one / 50 * (reticle.position - globalPosition).magnitude;
        }
        else
        {
            reticle.position = globalPosition + forward * maxSwingDistance;
            reticle.gameObject.SetActive(true);
            reticle.localScale = Vector3.one / 150 * (reticle.position-globalPosition).magnitude;
        }

        if (velocity.magnitude > 0.1 && isGrappled)
        {
            playerRB.AddForce(-velocity * 2000);
            StopSwing();
        }

        if (grip > 0)
        {
            Vector3 force = (int)controllerSide * (Quaternion.Euler(0, camera.rotation.eulerAngles.y, 0) * Vector3.left) * grip * 4;
            playerRB.AddForce(force);
        }

        //Debug.Log(velocity.magnitude);
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartSwing()
    {
        isSwinging = true;

        RaycastHit hit;
        if (Physics.Raycast(globalPosition, forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(globalPosition, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 6f;
            joint.damper = 4f;
            joint.massScale = 5f;

            lr.positionCount = 2;

            isGrappled = true;
        }
    }

    void StopSwing()
    {
        isSwinging = false;
        isGrappled = false;

        if (joint != null)
        {
            lr.positionCount = 0;
            Destroy(joint);
            joint = null;
        }
    }

    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, globalPosition);
        lr.SetPosition(1, swingPoint);
    }
}
