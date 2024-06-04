using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Vy/PlayerCollection")]


public class PlayerCollection : MonoBehaviour
{
    [SerializeField] int increase;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.IncreaseCoin(increase);
        }
    }
}
