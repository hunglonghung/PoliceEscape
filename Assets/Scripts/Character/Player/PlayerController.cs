using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Car movement")]
    [SerializeField] public float DriftFactor = 0.95f;
    [SerializeField] public float AccelerationFactor = 30f;
    [SerializeField] public float turnFactor = 3.5f;
    [SerializeField] public float AccelerationInput = 0;
    [SerializeField] public float SteeringInput = 0;
    [SerializeField] public float RotationAngle = 0;
    [SerializeField] public float MaxSpeed = 20f;
    [SerializeField] public float VelocityVsUp = 0;
    [SerializeField] Rigidbody2D carRigidbody2D;
    [Header("Weapon")]
    [SerializeField] public float RecoilDistance = 0.05f;
    [SerializeField] public GameObject Gun;
    [SerializeField] public bool IsPushedBack;
    [SerializeField] public float time;
    [SerializeField] public float recoilTime = 0.25f;
    [SerializeField] public Transform FireBox;
    void Start()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        // Fire();
    }
//Movement
    private void PlayerMovement()
    {
       ApplyEngineForce();
       KillSideVelocity();
       ApplySteering();
    }
    public void ApplyEngineForce()
    {
        VelocityVsUp = Vector2. Dot(transform.up, carRigidbody2D.velocity);
        if (VelocityVsUp > MaxSpeed && AccelerationInput > 0) return;
        if (VelocityVsUp < -MaxSpeed * 0.5f && AccelerationInput < 0) return;
        if (carRigidbody2D.velocity.sqrMagnitude > MaxSpeed * MaxSpeed && AccelerationInput > 0) return;
        if (AccelerationInput == 0) carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;
        Vector2 engineForceFactor = transform.up * AccelerationFactor * AccelerationInput;
        carRigidbody2D.AddForce(engineForceFactor,ForceMode2D.Force);
    }
    public void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor =carRigidbody2D.velocity.magnitude / 8;
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        RotationAngle -= SteeringInput * turnFactor;
        carRigidbody2D.MoveRotation(RotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        SteeringInput = inputVector.x;
        AccelerationInput = inputVector.y;
    }
    public void KillSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity,transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity,transform.right);
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * DriftFactor;
    }
    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;
        if (AccelerationInput < 0 && VelocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
    }

//Firing

    // private void Fire()
    // {
    //     if (Input.GetKeyUp(KeyCode.Space) && !isRecoiling)
    //     {
    //         BulletSpawn();
    //         if(IsPushedBack)
    //         {
    //             StartCoroutine(Recoil());
    //         }
    //     }
    // }
    // public void BulletSpawn()
    // {
    //     GameObject bullet = BulletSpawnner.Instance.GetBullet();
    //     Debug.Log(bullet);
    //     if(bullet != null)
    //     {
    //         bullet.transform.position = FireBox.position;
    //         bullet.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0,0,90);
    //         bullet.GetComponent<BulletFiring>().OnInit();
    //     }
    // }

    // private IEnumerator Recoil()
    // {

    //     isRecoiling = true;
    //     Vector3 startPosition = transform.position;
    //     Vector3 endPosition = transform.position - transform.up * RecoilDistance;
    //     float elapsedTime = 0;

    //     while (elapsedTime < recoilTime)
    //     {
    //         transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / recoilTime);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     transform.position = endPosition;
    //     isRecoiling = false;
    // }

    // private void BecomeInvisible()
    // {

    // }
    
}
