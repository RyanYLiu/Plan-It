using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // Base class for cards
    [SerializeField] protected int damage;
    [SerializeField] protected int apCost = 1;
    protected GridManager gridManager;
    protected GameObject battleGrid;
    protected BattleManager battleManager;
    protected PlayerManager playerManager;
    [SerializeField] protected List<Vector2Int> attackOffset;
    protected int handIndex;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        battleGrid = GameObject.FindGameObjectWithTag("BattleGrid");
        gridManager = FindObjectOfType<GridManager>();
    }

    virtual public void Attack(List<Vector2Int> positions)
    {

    }

    virtual public void SetHandIndex(int index)
    {
        handIndex = index;
    }

    virtual public void HighlightCard()
    {
        transform.parent.GetComponent<Image>().enabled = true;
    }

    virtual public void UnlightCard()
    {
        transform.parent.GetComponent<Image>().enabled = false;
    }

    virtual public void AddToPlayerDeck()
    {

    }
}
