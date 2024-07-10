using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    [SerializeField] bool isDead = false;
    Transform targetTransform;
    Vector3 targetPosition;
    [SerializeField] GameObject SmokingEffects;
    [SerializeField] GameObject BurningEffects;
    [SerializeField] GameObject Explosion;
    [SerializeField] SpriteRenderer CarSprite;
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            targetTransform = player.transform;
        }
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        if(!isDead)
        {
            Vector2 inputVector = Vector2.zero;
            FollowPlayer();
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
    void OnInit()
    {
        hp = 100;
        isDead = false;
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
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
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
        }
        if(hp > 40 && hp <= 70)
        {
            ShowSmokingEffects();
        }
        if(hp <= 40)
        {
            hp -= 40;
            ShowDeathEffects();
            isDead = true;
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
