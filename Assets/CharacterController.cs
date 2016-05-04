using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    Rigidbody2D rb;
    public float Speed;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

        float currentY = rb.velocity.y;

        //Horizontal movement
        float moveHorizontal = (Input.GetAxis("Horizontal"));
        rb.velocity = new Vector2(moveHorizontal * Speed, currentY);

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
}
