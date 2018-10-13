using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField]
    GameObject arrowIcon = null;
    [SerializeField]
    float menuSpacing = 0.875f;
    [SerializeField]
    float menuOffset = -2.575f;

    [SerializeField]
    GameObject[] menuOptions = null;
    [SerializeField]
    EventSystem eventSystem = null;

    [SerializeField]
    string axisName = "PlayerOne";
    private string controlAxis = "Joystick";

    int selectedMenuOption;

    void Start () {
        eventSystem.SetSelectedGameObject(menuOptions[0]);
    }

    public void ChangeSelection(float vertical)
    {
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
            Debug.Log("Selection changed");

            if( arrowIcon )
                arrowIcon.transform.position = new Vector3(arrowIcon.transform.position.x, menuOffset - (menuSpacing * selectedMenuOption));
            eventSystem.SetSelectedGameObject(menuOptions[selectedMenuOption]);
        }
    }

    public void SelectOption() {
        Button button = menuOptions[selectedMenuOption].GetComponent<Button>();
        button.OnSubmit(null);
    }
}
