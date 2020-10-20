using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // test enemy script
    [SerializeField] protected int totalHealth = 1;
    [SerializeField] protected Vector2Int position;
    [SerializeField] List<Vector2Int> attackOffset;
    protected int health;
    protected GridManager gridManager;
    protected BattleManager battleManager;
    private RewardManager rewardManager;
    private Text healthText;
    private string status;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = totalHealth;
        healthText = transform.GetChild(0).GetComponent<Text>();
        UpdateHealthDisplay();
        gridManager = FindObjectOfType<GridManager>();
        battleManager = FindObjectOfType<BattleManager>();
        rewardManager = FindObjectOfType<RewardManager>();
        gridManager.SetPosition(gameObject, position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthDisplay()
    {
        healthText.text = health.ToString() + "/" + totalHealth.ToString();
    }

    virtual public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthDisplay();
        if (health <= 0)
        {
            // play death animation
            battleManager.RemoveFromTurnOrder(gameObject);
            CheckWinCondition();
            Destroy(gameObject);
        }
        Debug.Log(health);
    }

    public void CheckWinCondition()
    {
        if (battleManager.GetTurnOrder().Count == 1)
        {
            rewardManager.ShowRewards();
        }
    }

    public Vector2Int GetPosition()
    {
        return position;
    }
}
