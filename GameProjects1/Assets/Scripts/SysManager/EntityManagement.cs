using System.Collections.Generic;
using UnityEngine;

public class EntityManagement : MonoBehaviour
{
    public List<Light> NrLight = new List<Light>();
    public List<GameObject> Entities = new List<GameObject>();

    public int LightCounter = 0;

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject Entity in Entities)
        {
            if(Entity.gameObject != null)
            {
                foreach(Light L in NrLight)
                {
                    if(L.gameObject != null)
                    {
                        if(L.enabled == false)
                        {
                            LightCounter += 1;
                        }
                    }
                }

                if(LightCounter == NrLight.Count)
                {
                    Entity.SetActive(true);
                }
                else
                {
                    Entity.SetActive(false);
                }

                LightCounter = 0;
            }
        }
    }
}
