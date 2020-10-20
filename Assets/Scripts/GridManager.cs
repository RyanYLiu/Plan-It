using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Manages position of objects on the grid
    private GameObject[,] grid = new GameObject[3,6] { {null,null,null,null,null,null}, {null,null,null,null,null,null}, {null,null,null,null,null,null} };
    private BattleManager battleManager;
    private PlayerMovement playerMovement;
    private GameObject battleGrid;
    [SerializeField] PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        battleGrid = GameObject.FindGameObjectWithTag("BattleGrid");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackEnemyPositions(List<Vector2Int> positions, int damage)
    {
        for (int index = 0; index < positions.Count; index++)
        {
            GameObject type = grid[positions[index].x, positions[index].y];
            if (type != null)
            {
                Enemy enemy = type.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    public void AttackPlayerPositions(List<Vector2Int> positions, int damage)
    {
        for (int index = 0; index < positions.Count; index++)
        {
            GameObject type = grid[positions[index].x, positions[index].y];
            if (type != null)
            {
                PlayerMovement player = type.GetComponent<PlayerMovement>();
                if (player)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }

    public void SetPosition(GameObject type, Vector2Int position)
    {
        grid[position.x, position.y] = type;
    }

    public void SelectTile(GameObject tile)
    {
        Vector2Int position = tile.GetComponent<Tile>().GetPosition();
        if (battleManager.GetCurrentTurn() == playerMovement.gameObject)
        {
            if (playerMovement.SelectingPosition())     // for player movement
            {
                SetPosition(null, playerMovement.GetCurrentPos());
                playerMovement.SetDestination(tile.transform.position, position);
                SetPosition(playerMovement.gameObject, position);
            }
            else if (battleManager.CardInUse())     // for player card usage
            {
                battleManager.CardInUse().Attack(new List<Vector2Int>{position});
                battleManager.SetCard(null);
            }
        }
    }
}
