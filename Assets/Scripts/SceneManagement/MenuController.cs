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

    PlayerInputManager inputManager;

    void Start () {
<<<<<<< HEAD

        inputManager = ScriptableObject.CreateInstance<PlayerInputManager>();
        inputManager.Initialize(0);

=======
>>>>>>> 014c752cf2cbd75c3b7373c9d70f0ff9787cefc0
        eventSystem.SetSelectedGameObject(menuOptions[0]);
    }

    public void ChangeSelection(float vertical)
    {
<<<<<<< HEAD
        if (inputLagRemaining >= Mathf.Epsilon)
        {
            inputLagRemaining -= Time.deltaTime;
            return;
        }

        float vertical = inputManager.GetAxis("Vertical_" + controlAxis);
        Debug.Log("Input detected? " + vertical);

        bool selectionChanged = false;

        if (vertical < 0 && Mathf.Abs(vertical) >= inputLag)
=======
        bool selectionChanged = false;
        if (vertical < 0)
>>>>>>> 014c752cf2cbd75c3b7373c9d70f0ff9787cefc0
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
<<<<<<< HEAD
                arrowIcon.transform.position = new Vector3(arrowIcon.transform.position.x, 
                                                           menuOffset - (menuSpacing * selectedMenuOption));
            inputLagRemaining = inputLag;
=======
                arrowIcon.transform.position = new Vector3(arrowIcon.transform.position.x, menuOffset - (menuSpacing * selectedMenuOption));
>>>>>>> 014c752cf2cbd75c3b7373c9d70f0ff9787cefc0
            eventSystem.SetSelectedGameObject(menuOptions[selectedMenuOption]);
        }
    }

<<<<<<< HEAD
    void HandleMenuEnterInput() {
        bool enter = inputManager.Enter();
        if( enter ) {
            Button button = menuOptions[selectedMenuOption].GetComponent<Button>();
            button.OnSubmit(null);
        }
=======
    public void SelectOption() {
        Button button = menuOptions[selectedMenuOption].GetComponent<Button>();
        button.OnSubmit(null);
>>>>>>> 014c752cf2cbd75c3b7373c9d70f0ff9787cefc0
    }
}
