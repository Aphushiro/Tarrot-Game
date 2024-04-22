using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public int requiredTokens;
    bool unlocked = false;

    Collider2D[] colliders;
    public SpriteRenderer[] doorSprites = new SpriteRenderer[2];
    bool interactable = true;

    private void Start()
    {
        colliders = GetComponents<Collider2D>();

    }

    public void PlayerInteractedWithRay()
    {
        OpenDoor(requiredTokens);
    }

    void OpenDoor (int reqTok)
    {
        // value == reqtok if the player has enough tokens. Otherwise it returns zero.
        int value = PlayerStats.Instance.DepositPentacles(reqTok);

        // If the player does not have enough tokens/penticles
        if (value != reqTok)
        {
            return;
        }
        // When we open the door
        unlocked = true;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

        for (int i = 0; i < doorSprites.Length; i++)
        {
            doorSprites[i].enabled = false;
        }
    }

    void ShutDoor ()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }

        for (int i = 0; i < doorSprites.Length; i++)
        {
            doorSprites[i].enabled = true;
        }
    }

    public void ForceShut ()
    {
        ShutDoor();
    }

    public void ForceOpen ()
    {
        OpenDoor(0);
    }
}
