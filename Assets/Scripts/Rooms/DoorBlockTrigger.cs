using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlockTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OverlapCheck"))
        {
            return;
        }
        GetComponentInParent<PreRoom>().RemoveDoor(gameObject.name[0]);
        Destroy(gameObject);
    }
}
