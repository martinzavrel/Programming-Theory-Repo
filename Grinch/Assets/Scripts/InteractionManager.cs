using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public bool isInRange;
    public bool isInInventory;
    public bool isDelivered;
    public GameObject objInRange;
    public GameObject childInRange;
    public GameObject[] presents;
    public GameObject[] inventory;
    public string tempInvTag;
    public string[] presentsTags;
    public string[] doorsTags;
    public int i;
    public int index;
    
    
    private void Start()
    {
       // delivered = new GameObject[3];
        inventory = new GameObject[1];
        //doors = new GameObject[3];
        presentsTags= new string[3];
        doorsTags= new string[3];
    }

    private void Update()
    {
        

         if (presentsTags[2] != null)
        {
            End();
        }
        
       
    }

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

  

    void End()
    {
        Debug.Log("End");


        CompareTags();
    }

  void CompareTags()
    {
        
        if ( index <= 2 && (doorsTags[index] == presentsTags[index]))
        {
            
            index++;
            CompareTags();
            
        }
        
       else if( index == 3)
        {
            Debug.Log("Gratz"); 
        }
        else if (index != 3) { Debug.Log("FAIL"); }
       

    }

    

}


        