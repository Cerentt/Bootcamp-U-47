using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    private bool isFlashing = false;

    public float flashDuration = 5f; // Flash süresi (saniye)
    public float flashDelay = 4f; // Fla? kapan?p aç?lma aral??? (saniye)
    public AudioClip flashlightSound; // Flashlight sesi

    private AudioSource audioSource;

    private void Start()
    {
        flashlight = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = flashlightSound;
        StartFlashing();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFlashing)
                StopFlashing();
            else
                StartFlashing();
        }
    }

    private void StartFlashing()
    {
        isFlashing = true;
        StartCoroutine(FlashRoutine());
    }

    private void StopFlashing()
    {
        isFlashing = false;
        StopCoroutine(FlashRoutine());
        flashlight.enabled = false;
    }

    private IEnumerator FlashRoutine()
    {
        while (isFlashing)
        {
            flashlight.enabled = true;
            PlayFlashlightSound();
            yield return new WaitForSeconds(flashDuration);

            flashlight.enabled = false;
            yield return new WaitForSeconds(flashDelay - flashDuration);
        }
    }

    private void PlayFlashlightSound()
    {
        if (flashlightSound != null)
        {
            audioSource.PlayOneShot(flashlightSound);
        }
    }
}
