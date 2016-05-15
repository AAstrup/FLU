using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    void Awake() { instance = this; }

    Rigidbody2D rb;

    Animator anim;
    bool isWalking;
    bool isFlying;

    float flyCurrentFallSpeed = 0f;

    //Set in inspector
    public float JumpHeight;
    public float Speed;
    public float flyHorizontalSpeed = 1f;
    public float flyVerticalSpeed = 1f;
    public float flyMaxFallSpeed = 1f;
    public string talkButtonLetter = "q";
    public string equipJetPackButtonLetter = "e";
    public float minJetPackXCoord = 0f;

    //state
    protected enum movementState { walking, flying}
    [SerializeField]
    protected movementState currentMovementState = movementState.walking;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isWalking = false;
        isFlying = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(talkButtonLetter))
            TalkButtonPressed();
        else if (Input.GetKeyDown(equipJetPackButtonLetter) || (transform.position.x < minJetPackXCoord && currentMovementState == movementState.flying))
            EquipButtonPressed();

        //Horizontal movement
        if (currentMovementState == movementState.walking)
            HorizontalWalkMovement();
        else if (currentMovementState == movementState.flying)
            HorizontalFlyMovement();
        else
            throw new System.Exception("MovementState not set!");
        //Jump
        if (currentMovementState == movementState.walking)
            VerticalWalkMovement();
        else if (currentMovementState == movementState.flying)
            VerticalFlyMovement();
        else
            throw new System.Exception("MovementState not set!");
    }

    void HorizontalWalkMovement()
    {
        //movement
        rb.gravityScale = 2f;

        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal != 0)
        {
            isWalking = true;
            anim.SetBool("IsWalking", true);
        }
        else
        {
            isWalking = false;
            anim.SetBool("IsWalking", false);
        }

        rb.velocity = new Vector2(moveHorizontal * Speed, rb.velocity.y);
        //Flip        
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (rb.velocity.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
    }

    void HorizontalFlyMovement()
    {
        rb.gravityScale = 0f;

        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal != 0)
        {
            isFlying = true;
            if (moveHorizontal > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (moveHorizontal < 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }

        rb.velocity = new Vector2(flyHorizontalSpeed*moveHorizontal, rb.velocity.y);
    }

    void VerticalWalkMovement()
    {
        if (Input.GetButtonDown("Jump"))
        {
            float currentX = rb.velocity.x;
            rb.velocity = new Vector2(currentX, JumpHeight);
        }
    }

    void VerticalFlyMovement()
    {
        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, flyVerticalSpeed);
            flyCurrentFallSpeed = 0f;
        }
        else
        {
            if (flyCurrentFallSpeed < flyMaxFallSpeed)
                flyCurrentFallSpeed += Time.deltaTime;
            else
                flyCurrentFallSpeed = flyMaxFallSpeed;
            rb.velocity = new Vector2(rb.velocity.x, -flyCurrentFallSpeed);
        }
    }

    void TalkButtonPressed()
    {
        DialogSystem.instance.TalkPlayerInput();
    }

    void EquipButtonPressed()
    {
        if (currentMovementState == movementState.walking && transform.position.x > minJetPackXCoord)
        {
            anim.ResetTrigger("UnequipJetPack");
            anim.SetTrigger("EquipJetPack");
            currentMovementState = movementState.flying;
            CameraScript.instance.ChangeToMapView();
        }
        else if (currentMovementState == movementState.flying || transform.position.x < minJetPackXCoord)
        {
            anim.ResetTrigger("EquipJetPack");
            anim.SetTrigger("UnequipJetPack");
            currentMovementState = movementState.walking;
            CameraScript.instance.ChangeToLevelView(transform.position.y);
        }
        else
            throw new System.Exception("Movement state not set!!");
    }
}
