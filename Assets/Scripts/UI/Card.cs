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

    public void Fool()
    {
        //Spawn mana orbs.
        Debug.Log("The fool");
    }

    public void Magician()
    {
        //Your wand attacks can pierce for 10 seconds.
        Debug.Log("Magician");
    }

    public void HighPriestess()
    {
        //You heal 50% of your max HP.
        Debug.Log("High Priestess");
    }

    public void Empress()
    {
        //Enemies’ movement and attacks are significantly slowed for 10 seconds.
        Debug.Log("Empress");
    }

    public void Emperor()
    {
        //You deal more damage and attack rate.
        Debug.Log("Emperor");
    }

    public void Hierophant()
    {
        //You push all enemies away
        Debug.Log("Hierophant");
    }
}
