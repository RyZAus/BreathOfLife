using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShaderTransition : MonoBehaviour
{
    //Private Vars
    private Renderer thisRenderer;
    private float swapAmount;
    private float swapAmount2;
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
        swapAmount = 0;
        //Grab the renderer
        thisRenderer = gameObject.GetComponent<Renderer>();
        thisRenderer.material = firstMaterial;
    }
    
    //Used for outside objects
    public void ChangeColor()
    {
        //We are now swapping color
        Debug.Log(" Changing color");
        swapColor = true;
        swapAmount2 = .01f / timeToSwap; //HACK A1 - Need to fix
    }

    public void StopColor()
    {
        //We are not swapping color
        swapColor = false;
    }

    private void Update()
    {
        //if currently swapping color
        if (swapColor)
        {
            //If we aren't finished swapping
            if (swapAmount <= 1)
            {
                //Swap the color slowly
                swapAmount += swapAmount2; //HACK A1
                thisRenderer.material.Lerp(firstMaterial, secondMaterial, swapAmount);
            }
        }
    }

    public IEnumerator DelayStartColour()
    {
        yield return new WaitForSeconds(5);
        ChangeColor();
    }
}
