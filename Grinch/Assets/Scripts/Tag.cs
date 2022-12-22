using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    public string childrenTag;
    // Start is called before the first frame update
    void Start()
    {
        childrenTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
