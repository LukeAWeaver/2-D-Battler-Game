﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {
    bool overlap;
    private SpriteRenderer sprite;


    void Start()
    {
        overlap = false;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (overlap)
        {
          //  gameObject.SetActive(false);
            sprite.sortingLayerName = "Foreground";
        }
        else
        {
           // gameObject.SetActive(true);
            sprite.sortingLayerName = "Background";
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        overlap = true;

    }
    private void OnTriggerExit(Collider collision)
    {
        overlap = false;

    }
}
