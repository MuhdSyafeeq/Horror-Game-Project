using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class HorrorSanity : MonoBehaviour
{
    public Light[] nearLights;
    public List<GameObject> horrorProps;

    private void Update()
    {
        if(gameManager.isPaused != true)
        {
            int counter = 0;
            foreach(Light curLight in nearLights)
            {
                if(curLight.enabled == false) { counter++; }
            }

            //Debug.Log($"Counter -> {counter} / Light Counts -> {nearLights.Count()}");
            if(counter == nearLights.Length)
            {
                GameObject creepyPuppet = horrorProps.Where(obj => obj.name == "Dead_man(anim)").SingleOrDefault();
                creepyPuppet.SetActive(true);
            }
            else
            {
                counter = 0;
                GameObject creepyPuppet = horrorProps.Where(obj => obj.name == "Dead_man(anim)").SingleOrDefault();
                creepyPuppet.gameObject.SetActive(false);
            }

            if(gameManager.missingparts == 3)
            {
                GameObject crawler = horrorProps.Where(obj => obj.name == "crawler").SingleOrDefault();
                crawler.gameObject.SetActive(true);
            }
            else if(gameManager.missingparts < 3)
            {
                GameObject crawler = horrorProps.Where(obj => obj.name == "crawler").SingleOrDefault();
                crawler.gameObject.SetActive(false);
            }
        }
    }
}
