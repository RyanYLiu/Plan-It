using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // manages health during battle
    private PlayerManager playerManager;
    private PlayerMovement playerMovement;
    private int totalHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        totalHealth = playerManager.GetTotalHealth();
        currentHealth = totalHealth;
        UpdateHealthDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthDisplay()
    {
        GetComponent<Text>().text = currentHealth.ToString() + "/" + totalHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthDisplay();
        if (currentHealth <= 0)
        {
            // play death animation
            Destroy(playerMovement.gameObject);
        }
    }
}
