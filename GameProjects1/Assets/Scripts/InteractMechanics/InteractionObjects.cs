using UnityEngine;
using System.Collections;

public class InteractionObjects : MonoBehaviour
{
    public float range = 100f;

    public Camera fpsCam;

    private bool showInteractMsg;
    private GUIStyle guiStyle;
    public string msg;

    public ActionObjects actObj;
    public GameObject theHitObj;
    public GameObject hands;
    public GameObject viewObject;

    void Start()
    {
        //setup GUI style settings for user prompts
        setupGui();
    }

    // Update is called once per frame
    void Update()
    {
        showInteractMsg = false;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            theHitObj = hit.collider.gameObject;
            if (hit.collider.gameObject.layer == 10)
            {
                if (theHitObj.TryGetComponent<ActionObjects>(out actObj))
                {
                    actObj = theHitObj.GetComponent<ActionObjects>();
                    msg = actObj.currMsg;
                    showInteractMsg = true;
                }
                else
                {
                    Debug.Log("Action Object are Missing, No Interaction will Occured");
                }
            }
            if (hit.collider.gameObject.layer != 10)
            {
                showInteractMsg = false;
                theHitObj = null;
                actObj = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && theHitObj != null && showInteractMsg != false)
        {
            gameManager.Instance.lastHitObj = theHitObj;
            //gameManager.Instance.AObj = actObj;

            actObj.InteractUse();
        }
        else if(gameManager.Instance.isViewArea == true)
        {
            //gameManager.Instance.lastHitObj.GetComponent<ActionObjects>().InteractUse();
            if (gameManager.Instance.lastHitObj.TryGetComponent<ActionObjects>(out actObj))
            {
                actObj = gameManager.Instance.lastHitObj.GetComponent<ActionObjects>();
                msg = actObj.currMsg;
                showInteractMsg = true;

                //Re-Entering Data
                theHitObj = gameManager.Instance.lastHitObj;

                if (Input.GetKeyDown(KeyCode.E)){ actObj.InteractUse(); }
            }
        }
    }

    #region GUI Config

    //configure the style of the GUI
    private void setupGui()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 18;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        msg = "Press E to Interact";
    }

    void OnGUI()
    {
        if (showInteractMsg)  //show on-screen prompts to user for guide.
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
        }
    }
    //End of GUI Config --------------
    #endregion
}
