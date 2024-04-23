using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            FindObjectOfType<StatUpgradeUI>().OpenStatSelectionPanel();
            Destroy(gameObject);
        }
    }
}
