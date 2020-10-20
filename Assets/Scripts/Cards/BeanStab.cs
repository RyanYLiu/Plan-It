using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanStab : Card
{
    private int cardIndex = 1;
    private PlayerAP playerAP;
    private PlayerMovement playerMovement;
    private DeckManager deckManager;
    [SerializeField] Color flashColor = new Color(255,0,0,0.5f);

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // GetComponent<Button>().onClick.AddListener(UseCard);
        playerAP = FindObjectOfType<PlayerAP>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        deckManager = FindObjectOfType<DeckManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseCard()
    {
        if (playerAP.GetCurrentAP() >= apCost)
        {
            Vector2Int playerPos = playerMovement.GetCurrentPos();
            List<Vector2Int> attackTiles = CalculateTiles(playerPos);
            // play attack animation
            gridManager.AttackEnemyPositions(attackTiles, damage);
            UnlightCard();
            UnlightGrid();
            playerAP.UseAP(apCost);
            deckManager.DiscardCard(handIndex);
            deckManager.DisplayCards();
            Destroy(transform.parent.gameObject);
        }
    }
    
    public void HighlightGrid()
    {
        HighlightCard();
        Vector2Int playerPos = playerMovement.GetCurrentPos();
        List<Vector2Int> highlightTiles = CalculateTiles(playerPos);
        
        for (int index = 0; index < attackOffset.Count; index++)
        {
            battleGrid.transform.GetChild(highlightTiles[index].x).GetChild(highlightTiles[index].y).GetComponent<Image>().color = flashColor;
        }
    }

    private List<Vector2Int> CalculateTiles(Vector2Int playerPos)
    {
        List<Vector2Int> attackTiles = new List<Vector2Int>();
        for (int index = 0; index < attackOffset.Count; index++)
        {
            Vector2Int attackPos = new Vector2Int(playerPos.x + attackOffset[index].x, playerPos.y + attackOffset[index].y);
            attackTiles.Add(attackPos);
        }
        return attackTiles;
    }

    public void UnlightGrid()
    {
        UnlightCard();
        Vector2Int playerPos = playerMovement.GetCurrentPos();
        List<Vector2Int> highlightTiles = CalculateTiles(playerPos);
        for (int index = 0; index < attackOffset.Count; index++)
        {
            battleGrid.transform.GetChild(highlightTiles[index].x).GetChild(highlightTiles[index].y).GetComponent<Image>().color = new Color(255,255,255,0);
        }
    }

    override public void AddToPlayerDeck()
    {
        playerManager.AddToDeck(cardIndex);
    }
}
