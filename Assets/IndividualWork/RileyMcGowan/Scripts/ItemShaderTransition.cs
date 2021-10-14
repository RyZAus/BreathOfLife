using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShaderTransition : MonoBehaviour
{
    //Private Vars
    private Renderer thisRenderer;
    private float swapAmount;
    private bool swapColor;
    //Public Vars
    [Tooltip("This is how long to swap materials for. Default 3.")]
    public float timeToSwap = 3;
    [Tooltip("Starting material.")]
    public Material firstMaterial;
    [Tooltip("End material.")]
    public Material secondMaterial;

    private void Awake()
    {
        //We are not swapping color
        swapColor = false;
        //Grab the renderer
        thisRenderer = gameObject.GetComponent<Renderer>();
        thisRenderer.material = firstMaterial;
        //HACK This must be removed for outside function
        //ChangeColor();
    }
    
    //Used for outside objects
    public void ChangeColor()
    {
        //We are now swapping color
        Debug.Log(" Changing color");
        swapColor = true;
    }

    public void StopColor()
    {
        //We are not swapping color
        swapColor = true;
    }

    private void Update()
    {
        //if currently swapping color
        if (swapColor)
        {
            //If we aren't finished swapping
            if (swapAmount != 1)
            {
                //Swap the color slowly
                swapAmount = Mathf.Lerp(0, 1, (Time.time) / timeToSwap);
                thisRenderer.material.Lerp(firstMaterial, secondMaterial, swapAmount);
            }
        }
    }
}
