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
    float inputLag = 0.3f;

    [SerializeField]
    string axisName = "PlayerOne";
    private string controlAxis = "Joystick";

    float inputLagRemaining = 0.0f;

    int selectedMenuOption;

    PlayerInputManager inputManager;

    void Start () {

        inputManager = ScriptableObject.CreateInstance<PlayerInputManager>();
        inputManager.Initialize(0);

        eventSystem.SetSelectedGameObject(menuOptions[0]);


        if (Input.GetJoystickNames().Length == 0)
        {
            controlAxis = axisName + "_Keyboard";
        } else {
            int controllerIndex = (axisName == "PlayerOne") ? 0 : 1;
            if (Input.GetJoystickNames()[controllerIndex] != null)
            {
                Debug.Log("Using controller for " + axisName);
                controlAxis = axisName + "_Joystick";
            }
            else
            {
                Debug.Log("Using keyboard for " + axisName);
                controlAxis = axisName + "_Keyboard";
            }
        }
    }

    // Update is called once per frame
    void Update () {
        HandleMenuSelectInput();
        HandleMenuEnterInput();
    }

    void HandleMenuSelectInput()
    {
        if (inputLagRemaining >= Mathf.Epsilon)
        {
            inputLagRemaining -= Time.deltaTime;
            return;
        }

        float vertical = inputManager.GetAxis("Vertical_" + controlAxis);
        bool selectionChanged = false;

        if (vertical < 0 && Mathf.Abs(vertical) >= inputLag)
        {
            Debug.Log("Input 'down' detected: " + vertical);

            // select character color Down Arrow
            selectedMenuOption++;
            if (selectedMenuOption >= menuOptions.Length)
                selectedMenuOption = 0;

            selectionChanged = true;
        }

        if (vertical > 0 && (vertical >= inputLag))
        {
            Debug.Log("Input 'up' detected: " + vertical);

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
            inputLagRemaining = inputLag;
            eventSystem.SetSelectedGameObject(menuOptions[selectedMenuOption]);
        }
    }

    void HandleMenuEnterInput() {
        bool enter = inputManager.Fire();
        if( enter ) {
            Button button = menuOptions[selectedMenuOption].GetComponent<Button>();
            button.OnSubmit(null);
        }
    }
}
