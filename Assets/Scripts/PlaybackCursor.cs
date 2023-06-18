using UnityEngine;   
using UnityEngine.UI;     
using UnityEngine.EventSystems;  
using System.Collections;       
using System.Collections.Generic;

public class PlaybackCursor : MonoBehaviour
{
    //public GameObject movableObject;
    public LayerMask buttonLayer;
    private ButtonView currentButton;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 rayOrigin = rectTransform.position;
        Vector2 rayDirection = Vector2.up; // Example direction (adjust as needed)
        float rayDistance = 1f; // Example distance (adjust as needed)

        RaycastHit2D debugHit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, buttonLayer);
        if (debugHit.collider != null)
        {
            //Debug.Log("Raycast hit: " + debugHit.collider.gameObject.name);
            ButtonView buttonView = debugHit.collider.GetComponent<ButtonView>();
            if (buttonView != null)
            {
                if (currentButton != buttonView)
                {
                    // The movableObject entered a new button
                    currentButton?.PlaybackCursorExited();
                    currentButton = buttonView;
                    currentButton.PlaybackCursorEntered();
                }
            }
            else
            {
                // The ray hit a UI object on the button layer that is not a ButtonView
                currentButton?.PlaybackCursorExited();
                currentButton = null;
            }
        }
        else
        {
            // The ray did not hit any UI object on the button layer
            currentButton?.PlaybackCursorExited();
            currentButton = null;
        }
    }
}
