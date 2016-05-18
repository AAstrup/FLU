using UnityEngine;
using System.Collections;

public class FollowPlayerScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.position = PlayerController.instance.transform.position;	
	}
}
