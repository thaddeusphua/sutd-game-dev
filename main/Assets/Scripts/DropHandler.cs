﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{

    private List<string> itemList = new List<string>();

    public static bool dropStatus = false;

    public Item item;

    public void OnDrop(PointerEventData pointerevent){
        print("Image " + GetComponent<RectTransform>().anchoredPosition.ToString());
        print("Drop pointer "+pointerevent.pointerDrag.GetComponent<RectTransform>().anchoredPosition.ToString());
        if(pointerevent.pointerDrag != null){
            pointerevent.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            dropStatus = true;
            // EventManager.onCraftClick
            itemList.Add(item.ItemName);
        }    
        
    }


    // Start is called before the first frame update
    private void Awake()
    {
        // itemList = InventoryManager.Instance.getInventory;
        
    }

    // Update is called once per frame
}
