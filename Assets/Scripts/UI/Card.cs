using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;

    public int handIndex;

    private CardSelection cs;

    private void Start()
    {
        cs = FindObjectOfType<CardSelection>();
    }

    public void DiscardCard()
    {
        if (hasBeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            cs.availableCardSlots[handIndex] = true;
            //Invoke("MoveToDiscardPile", 2f);
            cs.discardPile.Add(this);
            gameObject.SetActive(false);
        }
    }

    /*private void MoveToDiscardPile()
    {
        cs.discardPile.Add(this);
        gameObject.SetActive(false);
    }*/
}
