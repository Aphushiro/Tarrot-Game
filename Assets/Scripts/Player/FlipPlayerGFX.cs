using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayerGFX : MonoBehaviour
{
    public SpriteRenderer[] flipWithCursor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            for (int i = 0; i < flipWithCursor.Length; i++)
            {
                flipWithCursor[i].flipX = false;
            }
        } else
        {
            for (int i = 0; i < flipWithCursor.Length; i++)
            {
                flipWithCursor[i].flipX = true;
            }
        }
    }
}
