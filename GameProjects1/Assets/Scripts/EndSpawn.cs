using UnityEngine;

public class EndSpawn : MonoBehaviour
{
    [Header("Game Event Items")]
    [SerializeField]
    public CapsuleCollider capsuleCollider; 
    [Space(1)]
    //public GameObject[] itemParts;
    public GameObject[] aura;

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
        }
    }
}
