﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2d;

    public List<string> itemNames = new List<string>();
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public LayerMask layerMask;

    private float inverseMoveTime;

    public float moveTime = 0.1f;

    // check for if the movement coroutine is actually moving
    private bool coroutineMoveRunning;

    protected virtual void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
        coroutineMoveRunning = false;
    }
    public bool playerMovement(float x, float y, out RaycastHit2D hit)
    {

        var boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false; //set current object's collider to be false so that it doesnt intefere with line


        hit = Physics2D.Linecast(transform.position, transform.position + new Vector3(x, y));

        boxCollider.enabled = true;
        Vector3 currentPos = transform.position;
        // last check of untagged is for acid rivers, if not they don't kill the player
        if (hit.transform == null || hit.transform.tag == "Item" || hit.transform.tag == "Player" || hit.transform.tag == "Enemy" || hit.transform.tag == "Untagged" || hit.transform.tag == "SpawnPoint")
        {
            // old movement method
            // rb2d.MovePosition(transform.position + new Vector3(x, y));
            // new movement method


            if (coroutineMoveRunning == false)
            {
                if (hit.transform == null || hit.transform.tag == "Enemy" || hit.transform.CompareTag("SpawnPoint"))
                {
                    Debug.Log("Move");
                    StartCoroutine(coroutineMove(x, y));
                }
                else
                {
                    Debug.Log("Player moving into gameobject with tag of " + hit.transform.tag);
                }
                //Debug.Log("Player moving into null ");
                //if (hit.transform != null) 

            }
            return true;
        }
        else
        {
            transform.position = currentPos;
            Debug.Log("Player can't move into gameObject with name " + hit.transform.gameObject.name + " and tag of " + hit.transform.gameObject.tag);
            return false;
        }
    }

    IEnumerator coroutineMove(float x, float y)
    {
        // Debug.Log("Started coroutineMove");
        coroutineMoveRunning = true;
        //round to nearest 0.5
        // perform movement of position
        rb2d.MovePosition(transform.position + new Vector3(x, y));
        //transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        // perform movement of position
        // add a delay so it will not accept any more movement during this time
        yield return new WaitForSeconds(0.1f);
        coroutineMoveRunning = false;
        // Debug.Log("Finished coroutineMove");
        //Debug.Log("Cant move");
    }

}


