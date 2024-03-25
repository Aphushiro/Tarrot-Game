using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlockTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OverlapCheck") == false)
        {
            GetComponentInParent<PreRoom>().blockList.Add(gameObject.name[0].ToString());
            //GetComponentInParent<PreRoom>().RemoveDoor(gameObject.name[0]);
            Destroy(gameObject);
        }
    }
}
