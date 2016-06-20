using UnityEngine;
using System.Collections;

public class ReplantPlayer : MonoBehaviour {

    public BoxWorld boxWorld;

    void Update()
    {
        Replant();
    }

    void Replant()
    {
        //transform.position = new Vector3(transform.position.x, boxWorld.surfaceCenter.y, transform.position.z);
    }
}
