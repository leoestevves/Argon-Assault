using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 25f;
    [SerializeField] float xRange = 9f;
    [SerializeField] float yMinRange = -3f;
    [SerializeField] float yMaxRange = 10.5f;

    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        float xOffSet = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffSet;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffSet = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffSet;
        float clampedYPos = Mathf.Clamp(rawYPos, yMinRange, yMaxRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
