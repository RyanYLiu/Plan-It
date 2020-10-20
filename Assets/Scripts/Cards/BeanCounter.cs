using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanCounter : Card
{
    private int cardIndex = 4;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseCard()
    {
        if (playerAP.GetCurrentAP() >= apCost)
        {
            // Vector2Int playerPos = playerMovement.GetCurrentPos();
            // List<Vector2Int> attackTiles = CalculateTiles(playerPos);
            // play attack animation
            // gridManager.AttackEnemyPositions(attackTiles, damage);
            // UnlightGrid();
            playerAP.UseAP(apCost);
            deckManager.DiscardCard(handIndex);
            deckManager.DisplayCards();
            Destroy(transform.parent.gameObject);
        }
    }

    override public void AddToPlayerDeck()
    {
        playerManager.AddToDeck(cardIndex);
    }
}
