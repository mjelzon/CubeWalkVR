using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 0.3f;

    private Rigidbody rigidbody;
    private float xAxis, yAxis;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        //CheckForGround();
        Jump();
        
    }

    void FixedUpdate()
    {
        Move();
    }

    public float rotSpeed = 1f;
    public bool lockMovement = true;
    void Move() //relative to boxWorld
    {
        if (lockMovement) return;

        //transform.position += transform.right * xAxis * moveSpeed * Time.deltaTime;
        //transform.position += transform.forward * yAxis * moveSpeed * Time.deltaTime;

        Vector3 lookAt = new Vector3(xAxis, 0, yAxis).normalized;
        transform.LookAt(transform.position + lookAt);

        transform.position += lookAt * moveSpeed * Time.deltaTime;

    }

    public float jumpForce = 5f;
    private bool isGrounded = false;
    void Jump()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rigidbody.AddForce(transform.up * jumpForce);
        }

    }

    void CheckForGround()
    {
        if (isGrounded) return;

        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position, -transform.up), out hit))
        {
            Debug.Log(hit.distance);
            if(hit.distance > 0.005f)
            {
                isGrounded = false;
            }
            else
            {
                isGrounded = true;
            }
        }
    }

    
}
