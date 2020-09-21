using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDestroy : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.transform.position.z > this.gameObject.transform.position.z)
        {
            Destroy(this.gameObject);
        }
        
    }
}
