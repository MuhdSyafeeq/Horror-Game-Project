using UnityEngine;

public class randomSpawn : MonoBehaviour
{
    public Transform[] location;

    // Start is called before the first frame update
    void Start()
    {
        int selection = Mathf.RoundToInt( Random.Range(0.0f, ( (float)location.Length - 1.0f) ));
        Debug.Log($"" +
            $"<color=yellow>    Item { gameObject.name } selected index -></color>" +
            $"<color=lightblue> { selection }</color>" +
            $"<color=yellow>    out of </color>" +
            $"<color=lightblue> { location.Length }</color>");

        this.transform.position = location[selection].position;
    }
}
