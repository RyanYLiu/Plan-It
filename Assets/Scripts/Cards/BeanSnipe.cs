using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeanSnipe : Card
{
    private int cardIndex = 2;
    private PlayerAP playerAP;
    private DeckManager deckManager;
    [SerializeField] Color flashColor = new Color(255,0,0,0.5f);
    private List<Vector2Int> attackTiles = new List<Vector2Int>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // GetComponent<Button>().onClick.AddListener(UseCard);
        playerAP = FindObjectOfType<PlayerAP>();
        deckManager = FindObjectOfType<DeckManager>();
        CalculateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1) && battleManager.CardInUse() == this)
        {
            // cancel action
            CancelCard();
        }
    }

    public void UseCard()
    {
        if (playerAP.GetCurrentAP() >= apCost)
        {
            GetComponent<EventTrigger>().enabled = false;
            deckManager.DisableCardUsage();
            battleManager.SetCard(this);
            for (int index = 0; index < attackTiles.Count; index++)
            {
                battleGrid.transform.GetChild(attackTiles[index].x).GetChild(attackTiles[index].y).GetComponent<Button>().enabled = true;
            }
            // play attack animation
            // gridManager.AttackEnemyPositions(attackTiles, damage);
            // UnlightCard();
        }
    }

    private void CancelCard()
    {
        GetComponent<EventTrigger>().enabled = true;
        battleManager.SetCard(null);
        deckManager.EnableCardUsage();
        for (int index = 0; index < attackTiles.Count; index++)
        {
            battleGrid.transform.GetChild(attackTiles[index].x).GetChild(attackTiles[index].y).GetComponent<Button>().enabled = false;
        }
        UnlightGrid();
    }

    override public void Attack(List<Vector2Int> positions)
    {
        playerAP.UseAP(apCost);
        UnlightGrid();
        gridManager.AttackEnemyPositions(positions, damage);
        deckManager.DiscardCard(handIndex);
        deckManager.DisplayCards();
        Destroy(transform.parent.gameObject);
    }
    
    public void HighlightGrid()
    {
        HighlightCard();
        for (int index = 0; index < attackTiles.Count; index++)
        {
            battleGrid.transform.GetChild(attackTiles[index].x).GetChild(attackTiles[index].y).GetComponent<Image>().color = flashColor;
        }
    }

    private void CalculateTiles()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            attackTiles.Add(enemy.GetPosition());
        }
    }

    public void UnlightGrid()
    {
        UnlightCard();
        for (int index = 0; index < attackTiles.Count; index++)
        {
            battleGrid.transform.GetChild(attackTiles[index].x).GetChild(attackTiles[index].y).GetComponent<Image>().color = new Color(255,255,255,0);
        }
    }

    override public void AddToPlayerDeck()
    {
        playerManager.AddToDeck(cardIndex);
    }
}
