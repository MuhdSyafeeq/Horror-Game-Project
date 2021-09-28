using System.Collections.Generic;
using UnityEngine;

public class EntityManagement : MonoBehaviour
{
    public Light NrLight;
    public List<GameObject> Entities = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject Entity in Entities)
        {
            if(Entity.gameObject != null)
            {
                if (NrLight.enabled != true) { Entity.SetActive(true); }
                else { Entity.SetActive(false); }
            }
        }
    }
}
