using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class Player : MonoBehaviour
{
    public float health = 100f;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 10f;
        }
    }
}