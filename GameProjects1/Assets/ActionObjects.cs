using UnityEngine;

public class ActionObjects : MonoBehaviour
{
    public GameObject inRangeObjects;
    public GameObject player;
    public bool playerEntered;

    public bool getBoolean()
    {
        return playerEntered;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerEntered)
        {
            inRangeObjects.GetComponent<BoxCollider>().enabled = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerEntered = false;
        }
    }
}
