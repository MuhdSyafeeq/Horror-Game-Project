using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class randomSpawn : MonoBehaviour
{
    public List<Transform> location;

    // Start is called before the first frame update
    void Start()
    {
        bool isEmpty = !location.Any();

        if (isEmpty)
        {
            if (location.Count == 0 || location == null)
            {
                foreach (Transform obj in transform)
                {
                    if (obj.gameObject.name == "Location_Random") { location.Add(obj.GetComponent<Transform>());}
                }

                int selection = Mathf.RoundToInt(Random.Range(0.0f, ((float)location.Count - 1.0f)));
                Debug.Log($"" +
                    $"<color=yellow>    Item { gameObject.name } selected index -></color>" +
                    $"<color=lightblue> { selection }</color>" +
                    $"<color=yellow>    out of </color>" +
                    $"<color=lightblue> { location.Count }</color>");

                this.transform.position = location[selection].position;

                //Delete Other Datas
                foreach (Transform obj in transform)
                {
                    if (obj.gameObject.name == "Location_Random")
                    {
                        location.Remove(obj.GetComponent<Transform>());
                        Destroy(obj.gameObject, 1.5f);
                    }
                }
            }
        }
        else
        {
            Debug.Log($"<color=Red> List is NULLED! </color>");
        }
    }
}
