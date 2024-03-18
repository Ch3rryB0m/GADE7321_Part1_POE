using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Transform dest;
    private bool inRange = false;
    private bool holdingFlag = false;

    void Update()
    {
        Debug.Log("inRange: " + inRange);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (holdingFlag)
            {
                Debug.Log("F key pressed - dropping flag");
                Drop();
            }
            else if (inRange)
            {
                Debug.Log("F key pressed - picking up flag");
                Pickup();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered range");
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited range");
            inRange = false;
        }
    }

    void Pickup()
    {
        Debug.Log("Picking up flag");
        // Disable the collider and gravity
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

        // Move the object to the destination position
        this.transform.position = dest.position;

        // Set the object's parent to the destination
        this.transform.parent = dest;

        // Disable any movement or rotation
        GetComponent<Rigidbody>().isKinematic = true;

        holdingFlag = true;
    }

    void Drop()
    {
        Debug.Log("Dropping flag");
        // Remove the parent
        this.transform.parent = null;

        // Enable gravity and collider
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;

        // Enable movement and rotation
        GetComponent<Rigidbody>().isKinematic = false;

        holdingFlag = false;
    }
    //public Transform dest;

    //void OnMouseDown()
    //{

    //    GetComponent<BoxCollider>().enabled = false;
    //    GetComponent<Rigidbody>().useGravity = false;
    //    this.transform.position = dest.position;
    //    this.transform.parent = GameObject.Find("Destination").transform;


    //}
    //void OnMouseUp()
    //{

    //    this.transform.parent = null;
    //    GetComponent<Rigidbody>().useGravity = true;
    //    GetComponent<BoxCollider>().enabled = true ;

    //}
} 
