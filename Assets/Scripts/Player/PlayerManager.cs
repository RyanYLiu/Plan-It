using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // This script needs to be passed around between the map
    // and battle grid to preserve health, cards, and items between
    // instances
    
    [SerializeField] int playerHealth = 10;
    private int currentHealth;
    [SerializeField] int playerActionPoints = 4;
    [SerializeField] List<GameObject> deck = new List<GameObject>();
    private CardList cardList;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = playerHealth;
        cardList = FindObjectOfType<CardList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseActionPoints(int val)
    {
        playerActionPoints -= val;
    }

    public int GetTotalHealth()
    {
        return playerHealth;
    }
    
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetAP()
    {
        return playerActionPoints;
    }

    public List<GameObject> GetDeck()
    {
        return deck;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
    }

    public void AddToDeck(int index)
    {
        deck.Add(cardList.GetCardByIndex(index));
    }
}
