using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [SerializeField] GameObject cardScreen;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileText;

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].position;
                    randCard.hasBeenPlayed = false;

                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    public void Shuffle()
    {
        if (discardPile.Count >= 1)
        {
            foreach (Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cardScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
