using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOverlapFix : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("OverlapCheck"))
        {
            Destroy(transform.parent.transform.parent.gameObject);
            Debug.Log("Removed Overlap");
        }
    }
}
