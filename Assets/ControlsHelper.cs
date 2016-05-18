using UnityEngine;
using System.Collections;

public class ControlsHelper : MonoBehaviour {
    public static ControlsHelper instance;
    void Awake() { instance = this; }

    enum GUIState { NotActive, IsActive, WasActive };
    GUIState movementControls = GUIState.IsActive;
    GUIState talkControls = GUIState.NotActive;
    GUIState equipJetpackControls = GUIState.NotActive;
    GUIState flyJetpackControls = GUIState.NotActive;

    public GameObject GMJ_movementControls;
    public GameObject GMJ_talkControls;
    public GameObject GMJ_equipJetpackControls;
    public GameObject GMJ_flyJetpackControls;

    // Use this for initialization
    void Start () {
        StartCoroutine("KeyShown", GMJ_movementControls);
        GMJ_talkControls.SetActive(false);
        GMJ_equipJetpackControls.SetActive(false);
        GMJ_flyJetpackControls.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (movementControls == GUIState.IsActive)
            if (Input.GetAxis("Horizontal") != 0)
                DeactivateMovement();

        if (equipJetpackControls != GUIState.WasActive)
            if (PlayerController.instance.transform.position.x > PlayerController.instance.minJetPackXCoord)
                ActivateEquipControls();
    }

    void DeactivateMovement()
    {
        movementControls = GUIState.WasActive;
        //GMJ_movementControls.SetActive(false);
        StartCoroutine("KeyPressed_Delayed", GMJ_movementControls);
    }

    public void ActivateTalkControls()
    {
        if (talkControls != GUIState.NotActive)
            return;
        talkControls = GUIState.IsActive;
        //GMJ_talkControls.SetActive(true);
        StartCoroutine("KeyShown", GMJ_talkControls);
    }

    public void DeactivateTalkControls()
    {
        if (talkControls != GUIState.IsActive)
            return;
        talkControls = GUIState.WasActive;
        //GMJ_talkControls.SetActive(false);
        StartCoroutine("KeyPressed", GMJ_talkControls);
    }

    public void ActivateEquipControls()
    {
        if (equipJetpackControls != GUIState.NotActive)
            return;
        equipJetpackControls = GUIState.IsActive;
        //GMJ_equipJetpackControls.SetActive(true);
        StartCoroutine("KeyShown", GMJ_equipJetpackControls);
    }

    public void DeactivateEquipControls()
    {
        if (equipJetpackControls != GUIState.IsActive)
            return;
        equipJetpackControls = GUIState.WasActive;
        //GMJ_equipJetpackControls.SetActive(false);
        StartCoroutine("KeyPressed", GMJ_equipJetpackControls);
    }

    public void ActivateFlyControls()
    {
        if (flyJetpackControls != GUIState.NotActive)
            return;
        flyJetpackControls = GUIState.IsActive;
        //GMJ_flyJetpackControls.SetActive(true);
        StartCoroutine("KeyShown",GMJ_flyJetpackControls);
    }

    public void DeactivateFlyControls()
    {
        if (flyJetpackControls != GUIState.IsActive)
            return;
        flyJetpackControls = GUIState.WasActive;
        //GMJ_flyJetpackControlss.SetActive(false);
       StartCoroutine ("KeyPressed",GMJ_flyJetpackControls);
    }

    IEnumerator KeyShown(GameObject gmj)
    {
        gmj.SetActive(true);
        //yield return new WaitForEndOfFrame();
        Vector3 startScale = gmj.transform.localScale;
        print("startScale " + startScale.ToString());
        float scale = 0f;
        while (scale < 1f)
        {
            scale += Time.deltaTime * 10f;
            gmj.transform.localScale = new Vector3(scale * startScale.x, scale * startScale.y, scale * startScale.z);
            yield return new WaitForEndOfFrame();
        }
        gmj.transform.localScale = startScale;
    }
    IEnumerator KeyPressed_Delayed(GameObject gmj)
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine("KeyPressed",gmj);
    }
    IEnumerator KeyPressed(GameObject gmj)
    {
        Vector3 startScale = gmj.transform.localScale;
        float scale = 1.2f;
        while(scale > 0f)
        {
            scale -= Time.deltaTime * 10f;
            gmj.transform.localScale = new Vector3(scale* startScale.x, scale * startScale.y, scale * startScale.z);
            yield return new WaitForEndOfFrame();
        }
        gmj.SetActive(false);
    }
}
