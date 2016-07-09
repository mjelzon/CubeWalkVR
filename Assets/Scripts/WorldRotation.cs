using UnityEngine;
using System.Collections;

public class WorldRotation : MonoBehaviour {

    public Floor CurrentSurface;

    public static Floor NewSurface;

    private Vector3 RotationAxis
    {
        get { return Vector3.Cross(CurrentSurface.transform.up, NewSurface.transform.up); }
    }

    private float RotationAngle
    {
        get { return Vector3.Angle(CurrentSurface.transform.up,NewSurface.transform.up); }
    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Spin();

        Rotate();

	}

    void Spin()
    {
        if (isRotating) return;

        if (Input.GetButtonDown("RightBumper"))
        {
            StartCoroutine(RotateSide(Dir.RIGHT));
        }
        else if (Input.GetButtonDown("LeftBumper"))
        {
            StartCoroutine(RotateSide(Dir.LEFT));
        }
    }

    void Rotate()
    {
        if (CurrentSurface == NewSurface)
        {
            return;
        }

        if (!isRotating)
        {
            StartCoroutine(RotateCurrentSurfaceToTop());
        }
    }

    private bool isRotating = false;
    public float rotateSpeed = 3f;
    IEnumerator RotateCurrentSurfaceToTop()
    {
        isRotating = true;

        var axis = Vector3.Cross(CurrentSurface.transform.up, NewSurface.transform.up);

        while (Vector3.Distance(Vector3.up,NewSurface.transform.up) > 0.05f)
        {
            transform.Rotate(axis, -RotationAngle * rotateSpeed * Time.deltaTime,Space.World);
            yield return null;
        }
        transform.Rotate(axis, -Vector3.Angle(Vector3.up,NewSurface.transform.up), Space.World);

        CurrentSurface = NewSurface;

        isRotating = false;
    }

    IEnumerator RotateToTop()
    {
        isRotating = true;

        var timeRemaining = rotateTime;
        while(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        isRotating = false;
    }


    public float rotateTime = 3f;
    enum Dir { LEFT = -1,RIGHT = 1};
    IEnumerator RotateSide(Dir dir)
    {
        isRotating = true;

        var targetRot = transform.eulerAngles + new Vector3(0, (int)dir * 90, 0);

        var timeRemaining = rotateTime;
        while(timeRemaining > 0)
        {
            transform.Rotate(Vector3.up, ((int)dir * 90 * Time.deltaTime) / rotateTime, Space.World);
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = targetRot;

        isRotating = false;
    }

}
