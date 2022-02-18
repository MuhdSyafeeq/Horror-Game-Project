using UnityEngine;
using UnityEngine.AI;

public class Mannequin : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField]
    bool isWalking = false;
    //public Animator skeleton;

    [Header("Movement Settings")]
    [SerializeField]
    bool isNearPlayer = false;
    float sensor = 2.5f;
    public SphereCast checkCast;
    public NavMeshAgent agent;
    public GameObject target;
    public LayerMask layerMask;

    [Header("References Settings")]
    [SerializeField]
    int arrInBodyList = 0;
    public GameObject[] bodyList;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.missingparts >= 0 && (!gameManager.isPaused))
        {
            isNearPlayer = Physics.CheckSphere(this.transform.position, sensor, layerMask);
            if (isNearPlayer)
            {
                agent.SetDestination(target.transform.position);
                foreach(GameObject objectContain in bodyList)
                {
                    if (checkCast.currentHitObject == (objectContain.gameObject))
                    {
                        agent.isStopped = true;
                        arrInBodyList = 0;
                        break;
                    }
                    arrInBodyList = arrInBodyList + 1;
                }

                if(arrInBodyList == bodyList.Length)
                {
                    agent.isStopped = false;
                    arrInBodyList = 0;
                }
            }
        }
    }
}
