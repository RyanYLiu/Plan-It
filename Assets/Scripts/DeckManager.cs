using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour
{
    // manages player deck in battle
    private List<GameObject> deck;
    private List<GameObject> discard = new List<GameObject>();
    private PlayerManager playerManager;
    private System.Random rng = new System.Random();
    private List<GameObject> cardsInHand = new List<GameObject>();
    [SerializeField] int drawAmount = 5;
    [SerializeField] GameObject cardUI;
    [SerializeField] int evenCardOffset = 100;
    [SerializeField] int oddCardOffset = 200;
    [SerializeField] int spaceBetweenCards = 200;
    [SerializeField] Text numCardsText;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        deck = new List<GameObject>(playerManager.GetDeck());
        SetNumCardsInDeckText();
        Shuffle();
        StartTurn();
    }

    private void SetNumCardsInDeckText()
    {
        numCardsText.text = deck.Count.ToString();
    }

    private void DisplayInitialDraw()
    {
        
        int startPosition = GetStartPosition();
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            GameObject card = Instantiate(cardsInHand[i], new Vector3(0,0,0), Quaternion.identity, cardUI.transform);
            card.transform.localPosition = new Vector3(startPosition,0,0);
            card.transform.GetChild(0).GetComponent<Card>().SetHandIndex(i);
            startPosition -= spaceBetweenCards;
        }
    }

    public void StartTurn()
    {
        if (deck.Count == 0)
        {
            ReshuffleDiscard();
        }
        DrawCards(drawAmount);
        SetNumCardsInDeckText();
        DisplayInitialDraw();
    }

    private IEnumerator DisplayAfterCardDestroy()
    {
        yield return new WaitUntil(() => cardUI.transform.childCount == cardsInHand.Count);
        int startPosition = GetStartPosition();
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            GameObject card = cardUI.transform.GetChild(i).gameObject;
            card.transform.localPosition = new Vector3(startPosition,0,0);
            card.transform.GetChild(0).GetComponent<Card>().SetHandIndex(i);
            startPosition -= spaceBetweenCards;
        }
    }
    public void DisplayCards()
    {
        StartCoroutine(DisplayAfterCardDestroy());
    }

    public void DisableCardUsage()
    {
        foreach (Transform card in cardUI.transform)
        {
            card.GetChild(0).GetComponent<EventTrigger>().enabled = false;
            card.GetChild(0).GetComponent<Button>().enabled = false;
        }
    }
    
    public void EnableCardUsage()
    {
        foreach (Transform card in cardUI.transform)
        {
            card.GetChild(0).GetComponent<EventTrigger>().enabled = true;
            card.GetChild(0).GetComponent<Button>().enabled = true;
        }
    }

    public int GetStartPosition()
    {
        int startPosition;
        if (cardsInHand.Count % 2 == 0)
        {
            startPosition = cardsInHand.Count / 2 * evenCardOffset + 100 * (cardsInHand.Count / 2 - 1);
        }
        else
        {
            startPosition = cardsInHand.Count / 2 * oddCardOffset;
        }
        return startPosition;
    }

    private void DrawCards(int numCards)
    {
        int limit = Mathf.Clamp(numCards, 1, deck.Count);
        for (int index = 0; index < limit; index++)
        {
            cardsInHand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }

    private void Shuffle()
    {
        for (int index = deck.Count-1; index > 0 ; index--)
        {
            int swapIndex = rng.Next(0, index);
            GameObject swapCard = deck[swapIndex];
            deck[swapIndex] = deck[index];
            deck[index] = swapCard;
        }
    }

    private void ReshuffleDiscard()
    {
        deck = new List<GameObject>(discard);
        discard.Clear();
        Shuffle();
    }

    public void DiscardCard(int index)
    {
        discard.Add(cardsInHand[index]);
        cardsInHand.RemoveAt(index);
    }

    public void DiscardRestOfHand()
    {
        foreach (GameObject card in cardsInHand)
        {
            discard.Add(card);
        }
        cardsInHand.Clear();

        for(int i = 0; i < cardUI.transform.childCount; i++)
        {
            Destroy(cardUI.transform.GetChild(i).gameObject);
        }
    }
}
