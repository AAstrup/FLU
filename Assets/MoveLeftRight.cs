using UnityEngine;
using System.Collections;

public class MoveLeftRight : MonoBehaviour {

    public int PaseoLenght;
    SpriteRenderer sprite;
    public float SlowDown;

    public float pingpongValue;
    float currentX;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        currentX = GetComponent<Transform>().position.x;
        
	}
	
	// Update is called once per frame
	void Update () {

        pingpongValue = Mathf.PingPong(Time.time/SlowDown, PaseoLenght);
        transform.position = new Vector2 (currentX + pingpongValue, transform.position.y);

        if (pingpongValue < 0.01 && !sprite.flipX)
        {
            sprite.flipX = !sprite.flipX;
        }

        if (pingpongValue > PaseoLenght- 0.01 && sprite.flipX)
        {
            sprite.flipX = !sprite.flipX;
        }

        
	
	}
}
