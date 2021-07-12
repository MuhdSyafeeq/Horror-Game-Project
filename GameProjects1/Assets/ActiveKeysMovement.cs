using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveKeysMovement : MonoBehaviour
{
    public float speedModifier = 1.2f;
    public GameObject myTerrain;
    
    private int numTerr = 0;
    private List<TerrainData> myTerrData = new List<TerrainData>();

    private void Awake()
    {
        numTerr = myTerrain.GetComponentInChildren<Transform>().childCount;
        for (int i = 0; i < numTerr; i++)
        {
            myTerrData.Add(myTerrain.transform.GetChild(i).GetComponent<TerrainCollider>().terrainData);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Flash Sensory (Reduce All Audio to 0.5) (Create Emmitter of Blink when any entity moves)
        if(Input.GetKeyDown(KeyCode.F)) {
            //Debug.Log("Active/Diactive Flash Sensory!");
            Transform treeFab;
            foreach(TerrainData data_ in myTerrData)
            {
                foreach(TreeInstance tree_ in data_.treeInstances)
                {
                    var myObj = Instantiate(this.gameObject);
                    Debug.Log(myObj.name);
                }
            }
        }
        
        //if (Input.GetKeyDown(KeyCode.Q)) { Debug.Log("Active/Diactive Hearing Ultra-Sensor!"); }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { GetComponent<PlayerMovement>().speed *= speedModifier; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { GetComponent<PlayerMovement>().speed /= speedModifier; }
    }
}
