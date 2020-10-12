using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHandler : MonoBehaviour
{
    [SerializeField] GameObject scouter;
    [SerializeField] GameObject player;
    [SerializeField] MapManager mapManager;
    [SerializeField] GameObject scoutDialogWindow;
    [SerializeField] GameObject playerDialogWindow;

    public void HandleNodeClick(GameObject node)
    {
        if (mapManager.GetScoutingModeStatus())
        {
            scoutDialogWindow.SetActive(true);
        }
        else
        {
            playerDialogWindow.SetActive(true);
        }
        mapManager.SetDialogWindowStatus(true);
        mapManager.SetSelectedNode(node);
    }

}
