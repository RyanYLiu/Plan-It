using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindEnemy : Enemy
{
    private List<Vector2Int> attackTiles = new List<Vector2Int>();
    private int attackIndex = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (battleManager.GetCurrentTurn() == gameObject)
        {
            Attack();
            // battleManager.EndTurn();
        }
    }

    public void Attack()
    {
        attackIndex = Random.Range(0,2);
        if (attackIndex == 0)
        {
            GetComponent<Oh>().UseCard();
        }
        else if (attackIndex == 1)
        {
            GetComponent<Lol>().UseCard();
        }
        else if (attackIndex == 2)
        {
            GetComponent<Die>().UseCard();
        }
    }
}
