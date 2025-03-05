using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;


public class ButtonManager : MonoBehaviour
{
    [SerializeField] private VirtualButtonBehaviour virtualButton;
    //public UnityEvent OnButtonPressed;
    //public UnityEvent OnButtonReleased;
    public Animator mariposaAnimator;
    public string variableChangeAnimation;

    private void OnEnable()
    {
        virtualButton.RegisterOnButtonPressed(ButtonPressed);
        virtualButton.RegisterOnButtonReleased(ButtonReleased);
    }

    private void OnDestroy()
    {
        virtualButton.UnregisterOnButtonPressed(ButtonPressed);
        virtualButton.UnregisterOnButtonReleased(ButtonReleased);
    }

    private void ButtonPressed(VirtualButtonBehaviour button)
    {
        mariposaAnimator.SetBool(variableChangeAnimation, true);
        Debug.Log("Button Pressed");
    }

    private void ButtonReleased(VirtualButtonBehaviour button)
    {
        mariposaAnimator.SetBool(variableChangeAnimation, false);
        Debug.Log("Button Realeased");
    }
}
