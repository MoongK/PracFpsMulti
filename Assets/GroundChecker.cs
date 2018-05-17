using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

	void Start () {
        gameObject.layer = 11;
	}

    private void OnTriggerEnter(Collider other)
    {
        transform.root.GetComponent<PlayerController>().isJumping = false;
        transform.root.GetComponent<PlayerController>().isgrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        
        transform.root.GetComponent<PlayerController>().isJumping = true;
        transform.root.GetComponent<PlayerController>().isgrounded = false;
    }
}
