using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] float rayLength;
    LayerMask includeOnly;

    private void Start()
    {
        includeOnly = LayerMask.NameToLayer("Door");
    }

    void Update()
    {
        

        // Interact with doors
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckAndOpenDoor();
        }
    }

    void CheckAndOpenDoor()
    {
        Vector2 dir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        //Debug.Log(dir);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayLength, includeOnly);

        Debug.DrawRay(transform.position, dir*rayLength, Color.red, 1f);

        if (hit.collider != null)
        {
            if (hit.collider.transform.GetComponent<DoorInteract>() != null)
            {
                hit.collider.transform.GetComponent<DoorInteract>().PlayerInteractedWithRay();
            }
            
        }
    }
}
