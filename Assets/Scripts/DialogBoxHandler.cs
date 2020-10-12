using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxHandler : MonoBehaviour
{
    // public GameObject popUpBox;
    // public Animator popUpAnimator;
    // public string popUpText;
    PlayerMap player;
    Scouter scouter;

    private void Start()
    {
        player = FindObjectOfType<PlayerMap>();
        scouter = FindObjectOfType<Scouter>();
    }

    public void CloseDialogBox()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmScoutClickHandler()
    {
        scouter.SetMove(true);
        CloseDialogBox();
    }

    public void ConfirmPlayerClickHandler()
    {
        player.SetMove(true);
        CloseDialogBox();
    }
}
