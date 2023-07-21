using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public GameObject scopeOverlay; // Reference to the scope overlay object
    public Animator animator; // Reference to the Animator component attached to the player or relevant object

    // Start is called before the first frame update
    void Start()
    {
        // Hide the scope overlay at the start
        scopeOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the right mouse button to toggle the scope
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToggleScope();
        }
    }

    void ToggleScope()
    {
        // Toggle the visibility of the scope overlay
        scopeOverlay.SetActive(!scopeOverlay.activeSelf);

        // Set the "isAiming" boolean parameter in the animator
        animator.SetBool("isAiming", scopeOverlay.activeSelf);

        // Perform any additional actions you want when toggling the scope
        // For example, adjusting the player's field of view or enabling/disabling shooting mechanics
    }
}
