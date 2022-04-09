using UnityEngine;

public class randomSpawn : MonoBehaviour
{
    public Transform[] location;

    // Start is called before the first frame update
    void Start()
    {
        int selection = Mathf.RoundToInt( Random.Range(0.0f, (float)location.Length) );
        this.transform.position = location[selection].position;
    }
}
