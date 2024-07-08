using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    Transform targetTransform;
    Vector3 targetPosition;
        // Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        FollowPlayer();
        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;
        // Send the input to the car controller.
        playerController.SetInputVector(inputVector);
    }
    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = player.transform;
        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;
        float steerAmount = angleToTarget / 45.0f;
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);
        return steerAmount;
    }


}
