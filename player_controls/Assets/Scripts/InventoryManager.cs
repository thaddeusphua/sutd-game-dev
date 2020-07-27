﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance{get; private set;}
    private List<Transform> itemPlaceholders = new List<Transform>();
    private List<Image> inventoryImages = new List<Image>();

    private List<GameObject> inventoryItems = new List<GameObject>();
    //Should have an event that tells when an item has been added and then update the inventory.
    private GameObject player;

    private InventoryObject inventoryObject;

    private bool itemAdded;
    Transform inventory;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=1; i<9; i++){
            itemPlaceholders.Add(GameObject.FindGameObjectWithTag("Item"+i.ToString()).transform);
        }
        player = GameObject.FindGameObjectWithTag("Player");

        foreach(Transform slot in itemPlaceholders){
            Image slotimage = slot.GetComponent<Image>();
            slotimage.enabled = false;
            inventoryImages.Add(slotimage);
        }
        

    }
    
    void SetInventory(){
        for(int j=0; j<inventoryImages.Count; j++){
            if(!inventoryImages[j].IsActive()){
                inventoryImages[j].enabled = true;
                //Get the most recent object from the list. This is the actual gameobject item, not placeholder image.
                var toAdd = inventoryObject.PickupList[inventoryObject.PickupList.Count - 1];
                inventoryImages[j].sprite = toAdd.transform.GetComponent<SpriteRenderer>().sprite;
                inventoryItems.Add(toAdd.gameObject);
                break;
            }
        }
    }

    public List<GameObject> getInventory{
        get{
            return inventoryItems;
        }
    }



    // Update is called once per frame
    void Update()
    {
        itemAdded = player.GetComponent<CollectItem>().itemAdded;
        if(itemAdded){
            SetInventory();
        }
        player.GetComponent<CollectItem>().itemAdded = false;
    }
}
