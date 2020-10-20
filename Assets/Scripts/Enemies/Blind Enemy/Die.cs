using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : Card
{
    [SerializeField] List<Vector2Int> attackTiles;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseCard()
    {
        // play attack animation
        gridManager.AttackPlayerPositions(attackTiles, damage);
        HighlightGrid();
    }

    public void HighlightGrid()
    {
        for (int index = 0; index < attackTiles.Count; index++)
        {
            Image tile = battleGrid.transform.GetChild(attackTiles[index].x).GetChild(attackTiles[index].y).GetComponent<Image>();
            tile.color = new Color(255,0,0,0.5f);
        }
    }
}
