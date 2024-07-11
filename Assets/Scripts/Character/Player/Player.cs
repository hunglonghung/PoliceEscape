using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    public bool IsDead = false;

    [SerializeField] GameObject SmokingEffects;
    [SerializeField] GameObject BurningEffects;
    [SerializeField] GameObject Explosion;
    [SerializeField] SpriteRenderer CarSprite;
    void Start()
    {
        OnInit();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet")
        {
            health -= 20f; 
        }
        else
        {
            health -= 10f; 
        }

        //health interaction
        if (health <= 0)
        {
            ShowDeath();
        }
        else if (health <= 30)
        {
            ShowBurning();
        }
        else if (health > 30 && health <= 60)
        {
            ShowSmoke();
        }
    }
    private void ShowDeath()
    {
        IsDead = true;
        CarSprite.color = Color.black;
        Explosion.SetActive(true);
    }
    private void ShowBurning()
    {
        BurningEffects.SetActive(true);
    }
    private void ShowSmoke()
    {
        SmokingEffects.SetActive(true);
    }
    private void OnInit()
    {
        IsDead = false;
        SmokingEffects.SetActive(false);
        BurningEffects.SetActive(false);
        Explosion.SetActive(false);
        CarSprite.color = Color.white;
    }
}
