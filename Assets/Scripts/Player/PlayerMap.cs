using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMap : MonoBehaviour
{
    private bool move = false;
     private GameObject destination;
     [SerializeField] float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            MoveToNode();
        }
    }

    public void Focus(bool status)
    {
        if (status)
        {
            GetComponent<SpriteRenderer>().color = new Color(255f,255f,255,1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255f,255f,255,0.5f);
        }
    }

    public void SetMove(bool val)
    {
        GetComponent<Animator>().enabled = !val;
        move = val;
    }

    private void MoveToNode()
    {
        transform.position = Vector2.MoveTowards(
                                transform.position,
                                destination.transform.position,
                                moveSpeed * Time.deltaTime);
        if (transform.position == destination.transform.position)
        {
            SetMove(false);
        }
    }
    
    public void SetDestination(GameObject node)
    {
        destination = node;
    }
}
