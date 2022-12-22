using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class IsInRange : MonoBehaviour
{
    [SerializeField] TextMeshPro interactionNote;
    public bool isInRange;
    [SerializeField] GameObject children;
    public string childTag;
    
    

    private void Awake()
    {
        interactionNote = GetComponentInChildren<TextMeshPro>();

        childTag = children.tag;

    }

    private void Update()
    {


        if (isInRange)
        {
            
            DisplayInteraction();
        }
        else HideInteraction();

        if (gameObject.GetComponent<IsInRange>().isInRange) // This tells to go to the GameObject's attached script called 'ScriptName' and check if its boolean called 'booleanName' is set to true.
        {
            //InteractionManager.Instance.objInRange = gameObject.name; // This sets the value of the string 'theName' to the GameObjects' name when 'booleanName' is true.
            InteractionManager.Instance.objInRange = gameObject;
            InteractionManager.Instance.childInRange = children;
        }

    }
    

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
        InteractionManager.Instance.isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        InteractionManager.Instance.isInRange = false;
        InteractionManager.Instance.objInRange = null;
        InteractionManager.Instance.childInRange = null;
    }

    void DisplayInteraction()
    {
       // interactionNote.enabled = true;

    }

    void HideInteraction()
    {
       // interactionNote.enabled = false;
    }



}
