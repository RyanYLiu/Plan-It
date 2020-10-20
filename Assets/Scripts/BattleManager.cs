using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    // This script is to keep track of the current battle
    
    [SerializeField] GameObject currentTurn;
    [SerializeField] GameObject endTurnButton;
    [SerializeField] Text turnText;
    [SerializeField] Text turnCounterText;
    [SerializeField] GameObject turnPointerPrefab;
    [SerializeField] float turnPointerOffset = 130f;
    [SerializeField] GameObject battleUI;
    private int turnCounter = 1;
    private List<GameObject> turnOrder = new List<GameObject>();
    private int turnOrderIndex = 0;
    private PlayerAP playerAP;
    private PlayerMovement playerMovement;
    private DeckManager deckManager;
    private Card card = null;
    private GameObject turnPointer;

    // Start is called before the first frame update
    void Start()
    {
        playerAP = FindObjectOfType<PlayerAP>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        deckManager = FindObjectOfType<DeckManager>();
        currentTurn = playerMovement.gameObject;
        turnPointer = Instantiate(turnPointerPrefab, playerMovement.transform.position, Quaternion.identity, battleUI.transform);
        turnPointer.transform.localPosition += new Vector3(0,turnPointerOffset,0);
        turnOrder.Add(playerMovement.gameObject);
        turnCounterText.text = "Turn " + turnCounter.ToString();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            turnOrder.Add(enemy.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCurrentTurn()
    {
        return currentTurn;
    }

    public List<GameObject> GetTurnOrder()
    {
        return turnOrder;
    }

    public void EndTurn()
    {
        turnOrderIndex += 1;

        if (currentTurn == playerMovement.gameObject)
        {
            turnText.text = "Enemy Turn";
            // endTurnButton.gameObject.SetActive(false);
            deckManager.DiscardRestOfHand();
        }

        if (turnOrderIndex >= turnOrder.Count)
        {
            turnOrderIndex = 0;
            turnCounter += 1;
            turnCounterText.text = "Turn " + turnCounter.ToString();
            deckManager.StartTurn();
            // endTurnButton.gameObject.SetActive(true);
            turnText.text = "Player Turn";
            playerAP.ResetAP();
        }
        currentTurn = turnOrder[turnOrderIndex];
        MoveTurnPointer(currentTurn.transform.position);
    }

    public void MoveTurnPointer(Vector3 position)
    {
        turnPointer.transform.position = position;
        turnPointer.transform.localPosition += new Vector3(0,turnPointerOffset,0);
    }

    public void RemoveFromTurnOrder(GameObject enemy)
    {
        turnOrder.Remove(enemy);
    }

    public Card CardInUse()
    {
        return card;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }
}
