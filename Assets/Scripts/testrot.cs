using UnityEngine;
using System.Collections;

public class testrot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    float xAxis;
    float yAxis;
	void Update ()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        Rotate();
    }

    void Rotate()
    {
        var lookAtRot = new Vector3(xAxis, 0, yAxis);

        transform.LookAt(transform.position + lookAtRot, transform.up);
    }
}
