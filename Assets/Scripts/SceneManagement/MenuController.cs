using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {

    [SerializeField]
    GameObject arrowIcon = null;
    [SerializeField]
    float menuSpacing = 0.875f;
    [SerializeField]
    float menuOffset = -2.575f;

    [SerializeField]
    GameObject[] menuOptions;
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    float inputLag = 0.1f;

    [SerializeField]
    string axisName = "PlayerOne";

    float inputLagRemaining = 0.0f;

    int selectedMenuOption;

    void Start () {
        eventSystem.SetSelectedGameObject(menuOptions[0]);
    }

    // Update is called once per frame
    void Update () {
        HandleMenuSelectInput();
    }

    void HandleMenuSelectInput()
    {
        if (inputLagRemaining >= Mathf.Epsilon)
        {
            inputLagRemaining -= Time.deltaTime;
            return;
        }

        float vertical = Input.GetAxisRaw("Vertical_" + axisName);

        bool selectionChanged = false;

        if (vertical < 0)
        {
            // select character color Down Arrow
            selectedMenuOption++;
            if (selectedMenuOption >= menuOptions.Length)
                selectedMenuOption = 0;

            selectionChanged = true;
        }

        if (vertical > 0)
        {
            // select character color Up Arrow
            selectedMenuOption--;
            if (selectedMenuOption < 0)
                selectedMenuOption = (menuOptions.Length - 1);

            selectionChanged = true;
        }

        if (selectionChanged)
        {
            if( arrowIcon )
                arrowIcon.transform.position = new Vector3(arrowIcon.transform.position.x, menuOffset - (menuSpacing * selectedMenuOption));
            inputLagRemaining = inputLag;
            eventSystem.SetSelectedGameObject(menuOptions[selectedMenuOption]);
        }
    }

}
