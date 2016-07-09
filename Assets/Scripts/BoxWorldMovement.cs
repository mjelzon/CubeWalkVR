using UnityEngine;
using System.Collections;
using System.Linq;

public class BoxWorldMovement : MonoBehaviour {

    public float moveSpeed = 0.05f;

    //public Transform boxWorld;
    //private Transform boxWorldGraphics;
    //private Vector3 surfaceCenter;
    //private float edgeDistance;

    public BoxWorld boxWorld;

    private bool lockMovement = false;

	// Use this for initialization
	void Start ()
    {
        transform.position = boxWorld.surfaceCenter;
	}

    float xAxis, yAxis;
	// Update is called once per frame
	void Update ()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

	}

    void FixedUpdate()
    {
        Move();
    }

    void Move() //relative to boxWorld
    {
        if (lockMovement) return;

        transform.position += boxWorld.transform.right * xAxis * moveSpeed * Time.deltaTime;
        //transform.position += boxWorld.transform.forward * yAxis * moveSpeed * Time.deltaTime;


        //need to walk based on the world surface


        //RotateBoxWorld();
    }

    void PlantFeet()
    {
    }

    bool worldIsRotating = false;
    void RotateBoxWorld()
    {
        if (worldIsRotating)
            return;

        float xDistance = transform.position.x - boxWorld.surfaceCenter.x;
        float yDistance = transform.position.z - boxWorld.surfaceCenter.z;

        //check if at left edge
        if (xDistance < -boxWorld.edgeDistance)
        {
            StartCoroutine(RotateBoxWorldRoutine(boxWorld.transform.forward, -90));
        }
        else if (xDistance > boxWorld.edgeDistance)
        {
            StartCoroutine(RotateBoxWorldRoutine(boxWorld.transform.forward, 90));
        }

        if (yDistance < -boxWorld.edgeDistance)
        {
            StartCoroutine(RotateBoxWorldRoutine(boxWorld.transform.right, 90));
        }
        else if (yDistance > boxWorld.edgeDistance)
        {
            StartCoroutine(RotateBoxWorldRoutine(boxWorld.transform.right, -90));
        }
    }

    public float rotateSpeed = 1f;
    IEnumerator RotateBoxWorldRoutine(Vector3 axis, float degrees)
    {
        float rotationDegrees = 90;

        worldIsRotating = true;
        lockMovement = true;

        var currentRotation = boxWorld.graphics.eulerAngles;

        while (rotationDegrees > 0)
        {
            rotationDegrees -= Mathf.Abs(degrees * rotateSpeed * Time.deltaTime);
            boxWorld.graphics.Rotate(axis, degrees * rotateSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        Vector3 preciseRotation = new Vector3(
            roundUp(boxWorld.graphics.eulerAngles.x, 90),
            roundUp(boxWorld.graphics.eulerAngles.y, 90),
            roundUp(boxWorld.graphics.eulerAngles.z, 90));

        boxWorld.graphics.eulerAngles = preciseRotation;

        float xDistance = transform.position.x - boxWorld.surfaceCenter.x;
        float yDistance = transform.position.z - boxWorld.surfaceCenter.z;

        //if(xDistance < 0)
        //{
        //    var vec = new Vector3(-boxWorld.edgeDistance + 0.01f, transform.position.y, transform.position.z);
        //    transform.position = vec;
        //}

        worldIsRotating = false;
        lockMovement = false;
        yield return null;
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = boxWorld.graphics.rotation;
        var toAngle = Quaternion.Euler(boxWorld.graphics.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            boxWorld.graphics.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    float roundUp(float numToRound, int multiple)
    {
        if (multiple == 0)
            return numToRound;

        float remainder = Mathf.Abs(numToRound) % multiple;
        if (remainder == 0)
            return numToRound;

        if (numToRound < 0)
            return -(Mathf.Abs(numToRound) - remainder);
        else
            return numToRound + multiple - remainder;
    }

}
