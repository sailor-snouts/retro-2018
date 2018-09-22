using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMenuOnInput : MonoBehaviour {

    [SerializeField]
    EventSystem eventSystem = null;
    [SerializeField]
    GameObject selectedObject = null;

    private bool buttonSelected = false;

    private void Start()
    {
        if( selectedObject != null && eventSystem != null ) {
            eventSystem.SetSelectedGameObject(selectedObject);
        } else {
            Debug.LogWarning("Could not select default menu item - please configure script in Editor");
        }

    }

    void Update () {
        if( (Input.GetAxisRaw("Vertical") != 0.0f) && (!buttonSelected) )	{
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }	
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
