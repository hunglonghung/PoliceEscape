using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    public enum EnemyType
    {
        Car,
        Van,
        Truck,
        Tank,
        Planes
    }
    [SerializeField] public EnemyType enemyType;
    [SerializeField] int hp = 100;
    [SerializeField] public bool IsDead = false;
    Transform targetTransform;
    Vector3 targetPosition;
    [SerializeField] GameObject SmokingEffects;
    [SerializeField] GameObject BurningEffects;
    [SerializeField] GameObject Explosion;
    [SerializeField] SpriteRenderer CarSprite;
    [SerializeField] List<GameObject> otherCars;
    [SerializeField] public float avoidanceDistance = 5f;
    [SerializeField] public Vector3 VectorToTarget;
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        otherCars = EnemySpawnner.Instance.SpawnedEnemies;
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            targetTransform = player.transform;
        }
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        if(!IsDead)
        {
            Vector2 inputVector = Vector2.zero;
            FollowPlayer();
            AvoidOtherCars();
            inputVector.x = TurnTowardTarget();
            inputVector.y = 1.0f;
            // Send the input to the car controller.
            playerController.SetInputVector(inputVector);
        }
        else
        {
            Vector2 inputVector = Vector2.zero;
            playerController.SetInputVector(inputVector);
            StartCoroutine(getEnemy());
        }

    }
    public IEnumerator getEnemy()
    {
        yield return new WaitForSeconds(5f);
        EnemyPooling.Instance.ReturnObject(gameObject,(EnemyPooling.EnemyType)(int)enemyType);
        
    }
    public void AvoidOtherCars()
    {
        foreach (var car in otherCars)
        {
            if (car == null || car == gameObject) continue; 
            Vector3 diff = car.transform.position - transform.position;
            float distance = diff.magnitude;
            if (distance < avoidanceDistance)
            {
                VectorToTarget -= diff.normalized / distance; 
            }
        }
        
        VectorToTarget.Normalize();
    }
    void OnInit()
    {
        hp = 100;
        IsDead = false;
        Explosion.SetActive(false);
        SmokingEffects.SetActive(false);
        BurningEffects.SetActive(false);
        CarSprite.color = new Color(255,255,255,255);
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
        VectorToTarget = targetPosition - transform.position;
        VectorToTarget.Normalize();
        float angleToTarget = Vector2.SignedAngle(transform.up, VectorToTarget);
        angleToTarget *= -1;
        float steerAmount = angleToTarget / 45.0f;
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);
        return steerAmount;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag);
        hp -= 10;
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("-40");
            hp -= 40;
            if(hp <= 0)
            {
                ShowDeathEffects();
                IsDead = true;
            }
        }
        if(hp > 40 && hp <= 70)
        {
            ShowSmokingEffects();
        }

    }
    public void ShowSmokingEffects()
    {
        SmokingEffects.SetActive(true);
    }
    public void ShowDeathEffects()
    {
        Explosion.SetActive(true);
        BurningEffects.SetActive(true);
        SmokingEffects.SetActive(true);
        CarSprite.color = Color.black;

    }


}
