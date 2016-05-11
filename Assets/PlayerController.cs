using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    void Awake() { instance = this; }

    Rigidbody2D rb;
    
    Animator anim;
    bool isWalking;

    public float JumpHeight;
    public float Speed;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isWalking = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("q"))
            ButtonPressed();

        float currentY = rb.velocity.y;

        //Horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal != 0)
        {
            isWalking = true;
            anim.SetBool("IsWalking", true);
        } else
        {
            isWalking = false;
            anim.SetBool("IsWalking", false);
        }
        
        rb.velocity = new Vector2(moveHorizontal * Speed, currentY);

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            float currentX = rb.velocity.x;
            rb.velocity = new Vector2(currentX, JumpHeight);
        }

        //Flip        
        if (rb.velocity.x > 0)
        {
            
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
            
        }
    }

    void ButtonPressed()
    {
        DialogSystem.instance.PlayerInput();
    }
}
