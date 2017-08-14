﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class str : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button sp;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<KnightStats>().gameObject;
        Button btn = sp.GetComponent<Button>();
        btn.onClick.AddListener(AB1Unlock);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<KnightStats>().UnlockAB1 == 1)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = new Color32(128, 113, 113, 255);
        }
    }
    void AB1Unlock() //ability1
    {
        if (player.GetComponent<KnightStats>().UnlockAB1 < 1 && player.GetComponent<KnightStats>().SkillPoints > 0) //max upgrades is 1
        {
            player.GetComponent<KnightStats>().UnlockAB1++;
            gameObject.GetComponent<Image>().color = Color.white;
            player.GetComponent<KnightStats>().SkillPoints--;
        }
    }
    //tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<strDescription>().gameObject.GetComponent<Text>().text = "Strength will help you hit harder. (Upgradable at Attribution Trainer)"; //waaa this was crazy to code
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<strDescription>().gameObject.GetComponent<Text>().text = "";
    }
}