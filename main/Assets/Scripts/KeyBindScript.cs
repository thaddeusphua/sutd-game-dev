using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindScript : MonoBehaviour
{
    public CustomInputManager customInputManager;

    public TextMeshProUGUI up, left, down, right, pickup, inventory, drop;
    private GameObject currentKey;

    void Start()
    {
        DisplayControlTexts();
    }
    private void DisplayControlTexts()
    {
        up.SetText(customInputManager.Up.ToString());
        down.SetText(customInputManager.Down.ToString());
        left.SetText(customInputManager.Left.ToString());
        right.SetText(customInputManager.Right.ToString());
        pickup.SetText(customInputManager.PickUp.ToString());
        inventory.SetText(customInputManager.Inventory.ToString());
        drop.SetText(customInputManager.Drop.ToString());
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                Debug.Log(currentKey.name);
                switch (currentKey.name)
                {
                    case "Up": 
                        customInputManager.Up = e.keyCode;
                        break;
                    case "Down": 
                        customInputManager.Down = e.keyCode;
                        break;
                    case "Left": 
                        customInputManager.Left = e.keyCode;
                        break;
                    case "Right": 
                        customInputManager.Right = e.keyCode;
                        break;
                    case "PickUp": 
                        customInputManager.PickUp = e.keyCode;
                        break;
                    case "Inventory": 
                        customInputManager.Inventory = e.keyCode;
                        break;
                    case "Drop": 
                        customInputManager.Drop = e.keyCode;
                        break;
                    default: 
                        Debug.Log("no such key in keymappings" + currentKey.name);
                        break;
                }
                currentKey = null;
            }
        }

    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}