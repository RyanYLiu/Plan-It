using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Vector2Int position;

    public Vector2Int GetPosition()
    {
        return position;
    }
}
