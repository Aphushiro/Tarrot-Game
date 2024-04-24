using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    public static CardSelection Instance;
    [SerializeField] GameObject cardScreen;

    bool cardScreenEnabled = false;

    public List<Card> allCards = new List<Card>();
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileText;

    // Do not set this value below 4
    int deckSizeStart = 5;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Set starting deck size
        for (int i = 0; i < deckSizeStart; i++)
        {
            AcquireNewCard();
        }
        GameMng.Instance.tarotCardsAvailable = allCards.Count;
    }

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
        ClearHand();
        foreach (Card card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();

        DrawCard();
        DrawCard();
        DrawCard();
    }

    public void ClearHand ()
    {

        foreach (Card card in FindObjectsOfType<Card>())
        {
            card.DiscardCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCardScreen();
        }
    }

    public void ToggleCardScreen ()
    {
        cardScreenEnabled = !cardScreenEnabled;

        cardScreen.SetActive(cardScreenEnabled);

        if (cardScreenEnabled == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void AcquireNewCard ()
    {
        if (allCards.Count <= 0) { Debug.Log("No more cards available");  return; }
        deck.Add(allCards[0]);
        allCards.RemoveAt(0);
    }
}
