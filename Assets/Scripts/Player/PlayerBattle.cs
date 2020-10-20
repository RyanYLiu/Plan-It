using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    private Vector2Int startingPos = new Vector2Int(1,0);
    GridManager grid;
    PlayerHealth healthManager;
    PlayerAP apManager;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<PlayerHealth>();
        apManager = FindObjectOfType<PlayerAP>();
        grid = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }

    public Vector2Int GetStartPosition()
    {
        return startingPos;
    }
}
