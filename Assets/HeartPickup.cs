﻿using UnityEngine;
using System.Collections;

public class HeartPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Health.Instance.AddHealth();

            Destroy(gameObject);
        }
    }
}