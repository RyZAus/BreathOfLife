using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShaderTransition : MonoBehaviour
{
    //Private Vars
    private Renderer thisRenderer;
    private float swapAmount;
    private bool swapColour;
    //Public Vars
    [Tooltip("This is how long to swap materials for. Default 3.")]
    public float timeToSwap = 3;
    [Tooltip("Starting material.")]
    public Material firstMaterial;
    [Tooltip("End material.")]
    public Material secondMaterial;

    private void Awake()
    {
        //We are not swapping colour
        swapColour = false;
        //Grab the renderer
        thisRenderer = GetComponent<Renderer>();
        thisRenderer.material = firstMaterial;
        //HACK This must be removed for outside function
        ChangeColour();
    }
    
    //Used for outside objects
    public void ChangeColour()
    {
        //We are now swapping colour
        swapColour = true;
    }

    private void Update()
    {
        if (swapColour == true)
        {
            //If we aren't finished swapping
            if (swapAmount != 1)
            {
                //Swap the colour slowly
                swapAmount = Mathf.Lerp(0, 1, (Time.time) / timeToSwap);
                thisRenderer.material.Lerp(firstMaterial, secondMaterial, swapAmount);
            }
        }
    }
}
