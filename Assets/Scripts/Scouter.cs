using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scouter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float nodeHoverXOffset = 0.8f;
    [SerializeField] float nodeHoverYOffset = 0.3f;
    private bool move = false;
    private GameObject destination;

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

    public void Focus(bool val)
    {
        if (val)
        {
            GetComponent<SpriteRenderer>().color = new Color(255f,255f,255,1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255f,255f,255,0.5f);
        }
        transform.GetChild(0).gameObject.SetActive(val);
    }

    public void SetMove(bool val)
    {
        GetComponent<Animator>().enabled = !val;
        move = val;
    }

    public void SetDestination(GameObject node)
    {
        destination = node;
    }
    
    private void MoveToNode()
    {
        Vector3 destinationPos = destination.transform.position + new Vector3(nodeHoverXOffset,nodeHoverYOffset,0);
        transform.position = Vector2.MoveTowards(
                                transform.position,
                                destinationPos,
                                moveSpeed * Time.deltaTime);
        if (transform.position == destinationPos)
        {
            SetMove(false);
        }
    }
}
