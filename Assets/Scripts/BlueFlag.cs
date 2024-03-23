using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlag : MonoBehaviour
{
    public Transform blueFlag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<Score>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="ai")
        {
            blueFlag.transform.position = new Vector3 (0f, 0.303f, 5.729f);
        }
    }
}
