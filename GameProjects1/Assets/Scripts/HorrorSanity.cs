using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class HorrorSanity : MonoBehaviour
{
    [Header("Game Event Items")]
    [SerializeField]
    public CapsuleCollider capsuleCollider; 
    [Space(1)]
    public GameObject[] itemParts;
    public GameObject[] aura;

    [Header("Lights Horror Props")]
    [SerializeField]
    public Light[] nearLights;

    [Header("Horror Entities")]
    [SerializeField]
    public List<GameObject> horrorProps;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("FINSIHED");
        gameManager.Instance.Finish();
    }

    private void Start()
    {
        if(capsuleCollider == null) { capsuleCollider = this.GetComponent<CapsuleCollider>(); capsuleCollider.enabled = false; }
    }

    private void Update()
    {
        if(gameManager.Instance.isPaused != true)
        {
            if (gameManager.Instance.missingparts == 6)
            {
                capsuleCollider.enabled = true;
                foreach (GameObject auras in aura)
                {
                    auras.SetActive(true);
                }
            }

            int counter = 0;
            foreach(Light curLight in nearLights)
            {
                if(curLight.enabled == false) { counter++; }
            }

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

            if(gameManager.Instance.missingparts >= 3)
            {
                GameObject crawler = horrorProps.Where(obj => obj.name == "crawler").SingleOrDefault();
                crawler.gameObject.SetActive(true);
            }
        }
    }
}
