using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public bool isInRange;
    public bool isInInventory;
    public bool isDelivered;
    public string objInRange;


    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void Update()
    {

    }
}


        