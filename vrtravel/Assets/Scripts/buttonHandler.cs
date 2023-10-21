using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class buttonHandler : MonoBehaviour
{
    public managePosition ManagePosition;

    public TMP_Dropdown start;
    public TMP_Dropdown end;
    // public InputActionProperty bA; // button A
    // public InputActionProperty bB; // button B
    // public InputActionProperty bC; // button C
    public InputActionProperty reset;
    public InputActionProperty submit;

    // enum Button // hopefully no conflict w/ name
    // {
    //     A, B, C, N
    // }

    // private Button first, second;



    // Start is called before the first frame update
    void Start()
    {
        // ManagePosition.instruction.text = "init";
        // first = Button.N;
        // second = Button.N;
        Debug.Log(start.value);

    }

    byte retLoc(int butval)
    {
        switch(butval) {
            case 0:
                return 0;
            case 1:
                return 6;
            case 2:
                return 8;
            default:
                return 9;
        }
    }

    public void OnButtonPress() {
        if (start.value != end.value) {
            ManagePosition.makeInvis();
            ManagePosition.trail = ManagePosition.getTrail(retLoc(start.value), retLoc(end.value));
            ManagePosition.visTrail(ManagePosition.trail);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // reset the button order if pressed
        // if (reset.action.WasPressedThisFrame())
        // {
        //     ManagePosition.instruction.text = "working";
        //     first = Button.N;
        //     second = Button.N;
        //     return;
        // }

        // if (submit.action.WasPressedThisFrame())
        // {
        //     if (first != Button.N && second != Button.N) 
        //     {
        //         ManagePosition.trail = ManagePosition.getTrail(retLoc(first), retLoc(second));
        //         ManagePosition.visTrail(ManagePosition.trail);

        //         first = Button.N; second = Button.N;

        //         ManagePosition.SendImpulse(0.1f, 0.05f, "right");

        //         return;
        //     }
        // }

        // if (bA.action.WasPressedThisFrame() || bB.action.WasPressedThisFrame() || bC.action.WasPressedThisFrame())
        // {
        //     Button clicked;
        //     if (bA.action.WasPressedThisFrame()) {
        //         clicked = Button.A;
        //     } else if (bB.action.WasPressedThisFrame()) {
        //         clicked = Button.B;
        //     } else { // button C pressed
        //         clicked = Button.C;
        //     }

        //     if (first == Button.N) {
        //         first = clicked;
        //     }
        //     else { // first button clicked
        //         if (clicked != first) {
        //             second = clicked;
        //         }
        //     }
        // }
        
    }
}
