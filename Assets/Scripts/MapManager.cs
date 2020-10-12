using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Scouter scouter;
    [SerializeField] Canvas canvas;
    [SerializeField] PlayerMap player;
    private bool dialogWindowOpen = false;
    private bool scoutingMode = false;
    private GameObject selectedNode;

    public bool IsDialogWindowOpen()
    {
        return dialogWindowOpen;
    }

    public void SetDialogWindowStatus(bool status)
    {
        dialogWindowOpen = status;
    }

    public void SetScoutingModeStatus(bool status)
    {
        scoutingMode = status;
        scouter.Focus(status);
        player.Focus(!status);
    }

    public bool GetScoutingModeStatus()
    {
        return scoutingMode;
    }

    public GameObject GetSelectedNode()
    {
        return selectedNode;
    }

    public void SetSelectedNode(GameObject node)
    {
        selectedNode = node;
        if (scoutingMode)
        {
            scouter.SetDestination(node);
        }
        else
        {
            player.SetDestination(node);
        }
    }

}
