using UnityEngine;
using System.Collections;
using System.Linq;

public class BoxWorld : MonoBehaviour {

    public Transform graphics;

    public Vector3 surfaceCenter;

    public float edgeDistance;

    public Vector3 surfaceDirection;

	// Use this for initialization
	void Start ()
    {
	    graphics = GetComponentsInChildren<Transform>().First(x => x.tag == "Graphics");
        edgeDistance = graphics.lossyScale.x / 2;

        surfaceCenter = transform.position;
        surfaceCenter.y += graphics.lossyScale.x / 2;

        surfaceDirection = graphics.up;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
