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
    public float sensor = 2.5f;
    public SphereCast checkCast;
    public NavMeshAgent agent;
    public GameObject target;
    public LayerMask layerMask;

    [Header("References Settings")]
    [SerializeField]
    int BodyCountinList = 0;
    public GameObject[] bodyList;

    [Header("GameManager Settings")]
    [SerializeField]
    public gameManager gm;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.missingparts >= 0 && (gameManager.isPaused != true))
        {
            isNearPlayer = Physics.CheckSphere(this.transform.position, sensor, layerMask);
            if (isNearPlayer)
            {
                agent.SetDestination(target.transform.position);
                bool hasArrived = checkDistance(agent);

                foreach (GameObject objectContain in bodyList)
                {
                    if (checkCast.currentHitObject == (objectContain.gameObject))
                    {
                        agent.isStopped = true;
                        BodyCountinList = 0;
                        break;
                    }
                    BodyCountinList = BodyCountinList + 1;
                }

                if (BodyCountinList == bodyList.Length)
                {
                    if (hasArrived)
                    {
                        agent.isStopped = true;
                        //agent.enabled = false;
                        //agent.Stop();
                        gameManager.isPaused = true;
                        this.Invoke(() => gm.GameOver(), 1.5f);
                    }
                    else
                    {
                        agent.isStopped = false;
                        BodyCountinList = 0;
                    }
                }
            }
        }
    }

    public bool checkDistance(NavMeshAgent navMesh)
    {
        // Check if we've reached the destination
        if (!navMesh.pathPending)
        {
            if (navMesh.remainingDistance <= navMesh.stoppingDistance)
            {
                if (!navMesh.hasPath || navMesh.velocity.sqrMagnitude == 0f)
                {
                    //Debug.Log("!navMesh.hasPath || navMesh.velocity.sqrMagnitude == 0f");
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sensor);
    }
}

