using UnityEngine;

public class eyetracker : MonoBehaviour
{
    [SerializeField]private Camera mainCams;
    // Update is called once per frame
    void Update(){
        Ray ray = mainCams.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
    }
}
