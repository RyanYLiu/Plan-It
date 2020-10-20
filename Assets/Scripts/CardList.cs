using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    [SerializeField] List<GameObject> cards;

    // private void Start()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }

    public GameObject GetRandomCard()
    {
        return cards[Random.Range(0, cards.Count)];
    }

    public GameObject GetCardByIndex(int index)
    {
        return cards[index];
    }
}
