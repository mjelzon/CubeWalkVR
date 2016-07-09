using UnityEngine;
using System.Collections;

public class BoxGravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(var rigidbody in rigidbodies)
        {
            Vector3 directionToCenter = transform.position - rigidbody.position;

            rigidbody.AddForce(directionToCenter);

            Vector3 gravityDirection = Vector3.zero;

            if(directionToCenter.y == -0.4f)
            {
                gravityDirection = -transform.up;
            }
            else if(directionToCenter.x == -0.4f)
            {
                gravityDirection = -transform.right;
            }



            rigidbody.transform.up = gravityDirection;
        }
    }

}
