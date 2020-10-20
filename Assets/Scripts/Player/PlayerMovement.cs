using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector2Int[] movementDirections = {
        new Vector2Int(1,0), // N
        new Vector2Int(0,1), // E
        new Vector2Int(-1,0), // S
        new Vector2Int(0,-1), // W
        new Vector2Int(1,1), // NE
        new Vector2Int(1,-1), // NW
        new Vector2Int(-1,1), // SE
        new Vector2Int(-1,-1), // SW
    };

    // Player
    private PlayerAP playerAP;
    private Vector2Int startingPos = new Vector2Int(1,0);
    private Vector2Int currentPos;
    private GridManager grid;
    private bool move = false;
    private bool selectingPosition = false;
    private Vector3 destination;

    // Grid
    private GameObject battleGrid;
    [SerializeField] Color flashColor = new Color(255,0,0,0.5f);
    [SerializeField] float moveSpeed = 5f;

    // Managers
    private DeckManager deckManager;
    private BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAP = FindObjectOfType<PlayerAP>();
        battleGrid = GameObject.FindGameObjectWithTag("BattleGrid");
        battleManager = FindObjectOfType<BattleManager>();
        deckManager = FindObjectOfType<DeckManager>();
        grid = FindObjectOfType<GridManager>();
        currentPos = startingPos;
        grid.SetPosition(gameObject, currentPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move();
        }

        // if right mouse button clicked cancel highlight grid
        // re enable card button and hover
        // 
        if (Input.GetMouseButtonUp(1) && selectingPosition)
        {
            CancelMovement();
        }
    }

    private void CancelMovement()
    {
        UnlightGrid();
        selectingPosition = false;
        deckManager.EnableCardUsage();
    }

    public void HighlightGrid()
    {
        if (playerAP.GetCurrentAP() > 0)
        {
            deckManager.DisableCardUsage();
            List<Vector2Int> highlightPositions = PossibleMovementTiles();
            for (int index = 0; index < highlightPositions.Count; index++)
            {
                GameObject tile = battleGrid.transform.GetChild(highlightPositions[index].x).GetChild(highlightPositions[index].y).gameObject;
                tile.GetComponent<Button>().enabled = true;
                tile.GetComponent<Image>().color = flashColor;
                selectingPosition = true;
            }
        }
    }

    public void UnlightGrid()
    {
        List<Vector2Int> highlightPositions = PossibleMovementTiles();
        for (int index = 0; index < highlightPositions.Count; index++)
        {
            GameObject tile = battleGrid.transform.GetChild(highlightPositions[index].x).GetChild(highlightPositions[index].y).gameObject;
            tile.GetComponent<Button>().enabled = false;
            tile.GetComponent<Image>().color = new Color(255,255,255,0);
        }
    }

    public Vector2Int GetCurrentPos()
    {
        return currentPos;
    }

    public void SetCurrentPos(Vector2Int newPosition)
    {
        currentPos = newPosition;
    }

    private List<Vector2Int> PossibleMovementTiles()
    {
        List<Vector2Int> possibleMovement = new List<Vector2Int>();
        List<Vector2Int> enemyPositions = new List<Vector2Int>();
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemyPositions.Add(enemy.GetPosition());
        }
        foreach (Vector2Int direction in movementDirections)
        {
            Vector2Int newPos = currentPos + direction;
            if (enemyPositions.Contains(newPos))
            {
                continue;
            }
            else if (newPos.x < 0 || newPos.x >= 3)
            {
                continue;
            }
            else if (newPos.y < 0 || newPos.y >= 6)
            {
                continue;
            }
            possibleMovement.Add(newPos);
        }
        return possibleMovement;
    }

    public bool SelectingPosition()
    {
        return selectingPosition;
    }

    public void SetDestination(Vector3 destination, Vector2Int position)
    {
        UnlightGrid();
        playerAP.UseAP(1);
        this.destination = destination;
        currentPos = position;
        move = true;
        selectingPosition = false;
    }
    private void Move()
    {
        transform.position = Vector2.MoveTowards(
                                transform.position,
                                destination,
                                moveSpeed * Time.deltaTime);
        battleManager.MoveTurnPointer(transform.position);
        if (transform.position == destination)
        {
            move = false;
            deckManager.EnableCardUsage();
        }
    }
}
