using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAP : MonoBehaviour
{
    // manages AP during battle
    PlayerManager playerManager;
    int totalAP;
    int currentAP;
    private Text apText;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        apText = GetComponent<Text>();
        totalAP = playerManager.GetAP();
        currentAP = totalAP;
        UpdateAPDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateAPDisplay()
    {
        apText.text = currentAP.ToString() + "/" + totalAP.ToString();
    }

    public void UseAP(int apCost)
    {
        currentAP -= apCost;
        UpdateAPDisplay();
    }

    public int GetCurrentAP()
    {
        return currentAP;
    }

    public void ResetAP()
    {
        currentAP = totalAP;
        UpdateAPDisplay();
    }
}
