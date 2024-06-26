using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CardSelection.Instance.AcquireNewCard();
            Destroy(gameObject);
        }
    }
}
