using System.Collections.Generic;
using UnityEngine;

public class EntityManagement : MonoBehaviour
{
    public Light NrLight;
    public GameObject Nightmare;

    public List<GameObject> Entities = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(NrLight.enabled != true) { Nightmare.SetActive(true); }
        else { Nightmare.SetActive(false); }
    }
}
