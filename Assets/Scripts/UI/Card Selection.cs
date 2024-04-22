using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [SerializeField] GameObject cardScreen;

    bool cardScreenEnabled = false;


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
        if (PlayerStats.Instance.ExpendMana(1) == false)
        {
            return;
        }
        if (discardPile.Count >= 0)
        {
            foreach (Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();

            DrawCard();
            DrawCard();
            DrawCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cardScreenEnabled = !cardScreenEnabled;

            cardScreen.SetActive(cardScreenEnabled);

            if (cardScreenEnabled == true)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
