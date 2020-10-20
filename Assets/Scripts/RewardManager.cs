using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RewardManager : MonoBehaviour
{
    [SerializeField] Image cardSelectionDisplay;
    [SerializeField] int numRewards = 2;
    private CardList cardList;
    private PlayerManager playerManager;
    private int evenCardOffset = 200;
    private int oddCardOffset = 400;
    private int cardOffset = 400;
    private Vector2 scale = new Vector3(2,2,0);

    // Start is called before the first frame update
    void Start()
    {
        cardList = FindObjectOfType<CardList>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRewards()
    {
        // List<GameObject> rewards ;
        cardSelectionDisplay.gameObject.SetActive(true);
        // rewards = new List<GameObject>();
        int startPosition = GetStartPosition();
        for (int i = 0; i < numRewards; i++)
        {
            // rewards.Add(cardList.GetCard());
            Vector3 position = cardSelectionDisplay.transform.parent.transform.position;
            Transform card = Instantiate(cardList.GetRandomCard(), position, Quaternion.identity, cardSelectionDisplay.transform).transform.GetChild(0);
            card.localPosition = new Vector3(startPosition,0,0);
            card.localScale = scale;
            startPosition -= cardOffset;
            card.GetComponent<EventTrigger>().enabled = false;
            // EventTrigger.Entry entry = new EventTrigger.Entry();
            // entry.eventID = EventTriggerType.PointerEnter;
            // EventTrigger.Entry exit = new EventTrigger.Entry();
            // exit.eventID = EventTriggerType.PointerExit;
            // entry.callback.AddListener((eventData) => { MyFunction(j); });
            // x.GetComponent<EventTrigger>().triggers.Add(entry);
            // cardTriggers.OnPointerEnter(cardScript.HighlightCard);
            // cardTriggers.OnPointerExit = cardScript.UnlightCard;
            Button cardButton = card.GetComponent<Button>();
            cardButton.onClick.RemoveAllListeners();
            cardButton.onClick.AddListener(card.GetComponent<Card>().AddToPlayerDeck);
        }
    }

    public int GetStartPosition()
    {
        int startPosition;
        if (numRewards % 2 == 0)
        {
            startPosition = numRewards / 2 * evenCardOffset + 200 * (numRewards / 2 - 1);
        }
        else
        {
            startPosition = numRewards / 2 * oddCardOffset;
        }
        return startPosition;
    }

    public void SkipRewards()
    {
        // Load map scene
    }
}
