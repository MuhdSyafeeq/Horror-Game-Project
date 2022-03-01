using UnityEngine;

public class SphereCast : MonoBehaviour
{
    public GameObject currentHitObject;

    public float sphereRadius;
    public float maxDistance = 5.5f;
    public LayerMask layerMask;

    public Vector3 origin;
    public Vector3 direction;

    public float currentHitDistance;

    private float fearMult = .3f;
    private float TimeLook_Ent = 0f;
    public Insanity playerSanity;

    public ActiveKeysMovement playerKeys;

    private void Start()
    {
        playerSanity.SetMaxSanity(100f);
    }

    void Update()
    { 
        if(gameManager.isPaused != true)
        {
            origin = transform.position;
            direction = transform.forward;
            RaycastHit hit;
            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                //Debug.Log($"Ent@Look -> {TimeLook_Ent} || @Lights -> {TimeAt_Light}");
                currentHitObject = hit.transform.gameObject;
                currentHitDistance = hit.distance;
                if (hit.transform.gameObject.layer == 11) // If Entities
                {
                    //Debug.Log("Hit An Entity");
                    if (playerSanity.slider.value >= 0)
                    {
                        playerSanity.reduceSanity((TimeLook_Ent * fearMult) * Time.deltaTime);
                        TimeLook_Ent += .17f; //.08f
                    }
                }
            }
            else
            {
                currentHitDistance = maxDistance;
                currentHitObject = null;

                TimeLook_Ent = 0f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
