using UnityEngine;
using System.Collections;

public class InteractionObjects : MonoBehaviour
{
    public float range = 100f;

    public Camera fpsCam;

    private bool showInteractMsg;
    private GUIStyle guiStyle;
    private string msg;

    void Start()
    {
        //setup GUI style settings for user prompts
        setupGui();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                Debug.Log(hit.collider.name);
                showInteractMsg = true;
            }
            else
            {
                showInteractMsg = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractUse();
        }
    }

    void InteractUse()
    {
        
    }

    #region GUI Config

    //configure the style of the GUI
    private void setupGui()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        msg = "Press E to Interact";
    }

    private string getGuiMsg(bool isOpen)
    {
        string rtnVal;
        if (isOpen)
        {
            rtnVal = "E to turn on Lights";
        }
        else
        {
            rtnVal = "E to turn off Lights";
        }

        return rtnVal;
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
