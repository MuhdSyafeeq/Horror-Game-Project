using UnityEngine;
using UnityEngine.AI;

public class Mannequin : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField]
    bool isWalking = false;
    public Animator skeleton;

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


    [Header("Body-Replacing Settings")]
    [SerializeField]
    bool startChase = false;
    bool activateUpdate = false;
    int numCol = 0;

    public GameObject bodyChange;
    public SkinnedMeshRenderer[] meshRendr;
    public MeshCollider[] meshColl;

    [Header("GameOver Settings")]
    [SerializeField]

    #region Camera Object
    public Camera playerCams;
    public Camera deathCams;
    #endregion

    #region Body Changes
    public GameObject MainBody;
    public GameObject GameOverBody;
    #endregion



    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.missingparts >= 0 && (gameManager.Instance.isPaused != true))
        {
            if (activateUpdate)
            {
                foreach (SkinnedMeshRenderer skinned in meshRendr)
                {
                    Mesh colliderMesh = new Mesh();
                    skinned.BakeMesh(colliderMesh);
                    meshColl[numCol].sharedMesh = null;
                    meshColl[numCol].sharedMesh = colliderMesh;
                    numCol += 1;
                }
                numCol = 0;
            }

            if(startChase == true) {
                foreach(GameObject currentObject in bodyList)
                {
                    if(BodyCountinList == 6) { break; }

                    currentObject.SetActive(false);
                    BodyCountinList += 1;
                }
                bodyChange.SetActive(true);
                startChase = false;
                activateUpdate = true;
            }

            isNearPlayer = Physics.CheckSphere(this.transform.position, sensor, layerMask);
            if (isNearPlayer)
            {
                if (!activateUpdate) { startChase = true; }

                agent.SetDestination(target.transform.position);
                bool hasArrived = checkDistance(agent);

                foreach (GameObject objectContain in bodyList)
                {
                    if (checkCast.currentHitObject == (objectContain.gameObject))
                    {
                        skeleton.enabled = false;

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

                    #region Change Camera
                        playerCams.gameObject.SetActive(false);
                        deathCams.gameObject.SetActive(true);

                        MainBody.SetActive(false);
                        GameOverBody.SetActive(true);
                    #endregion

                        gameManager.Instance.isPaused = true;
                        this.Invoke(() => gameManager.Instance.GameOver(), 1.5f);
                    }
                    else
                    {
                        skeleton.enabled = true;

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

