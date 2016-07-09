using UnityEngine;
using System.Collections;

public class MoveableBoxObject : MonoBehaviour {

    public bool isPlayer = false;

    private Floor currentSurface;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
    {
        if (GetComponentInChildren<Rigidbody>())
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        FallToFloor();
        MatchFloorRotation();
    }

    void MatchFloorRotation()
    {
        if(currentSurface != null)
        {
            //transform.up = currentFloor.transform.up;
            if (!isRotating)
            {
                StartCoroutine(RotateToStanding());
            }
        }
    }

    public float rotateSpeed = 1f;
    private bool isRotating = false;
    IEnumerator RotateToStanding()
    {
        isRotating = true;

        while (Vector3.Angle(transform.up, currentSurface.transform.up) > 0.01f)
        {
            var axis = Vector3.Cross(transform.up, currentSurface.transform.up);
            var angle = Vector3.Angle(transform.up, currentSurface.transform.up);

            transform.Rotate(axis, angle * rotateSpeed * Time.deltaTime, Space.World);

            yield return null;
        }
        

        isRotating = false;
    }

    public float fallForce = 5f;
    void FallToFloor()
    {
        if(currentSurface != null)
            rigidbody.AddForce(-currentSurface.transform.up * fallForce, ForceMode.Force);
    }

    private bool hasLeftCurrentFloor = true; //must initialize to true to start gravity on current floor
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            hasLeftCurrentFloor = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor")
        {
            if (hasLeftCurrentFloor)
            {
                if (other.GetComponentInChildren<Floor>())
                {
                    currentSurface = other.GetComponent<Floor>();
                    if (isPlayer)
                    {
                        WorldRotation.NewSurface = other.GetComponent<Floor>();
                    }
                }
                hasLeftCurrentFloor = false;
            }
        }
    }


}
