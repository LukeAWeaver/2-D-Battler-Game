﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinInterface : MonoBehaviour {
    public int hp;
    public int energy;
    public float ms;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(hp==1)
        {
            ms = 0.01f;
        }
	}

}
