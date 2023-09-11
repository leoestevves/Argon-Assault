using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General setup settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 25f;
    [Tooltip("How far player moves")]
    [SerializeField] float xRange = 9f;
    [SerializeField] float yMinRange = -3f;
    [SerializeField] float yMaxRange = 10.5f;    

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 1f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;

    [SerializeField] float rotationFactor = 1f;

    [Header("Lasers gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {       
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        //transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //Forma mais efetiva de fazer a rotação

        //Deixa a rotação mais suave
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);  //Forma mais efetiva de fazer a rotação
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffSet = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffSet;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffSet = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffSet;
        float clampedYPos = Mathf.Clamp(rawYPos, yMinRange, yMaxRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);            
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
