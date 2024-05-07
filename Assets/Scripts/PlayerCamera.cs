using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public bool hasBolter = true;
    public bool hasOrbLauncher = false;
    public bool hasTwinSmgs = false;
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    [SerializeField] private GameObject bolter;
    [SerializeField] private GameObject orbLauncher;
    [SerializeField] private GameObject twinSmgs;

    void Start()
    {
        SwitchToBolter();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (bolter == null || orbLauncher == null || twinSmgs == null)
        {
            Debug.LogError("One or more child objects not found.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.playerAlive)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rotate camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToBolter();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToOrbLauncher();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToDualSMGs();
        }        
    }

    public void SwitchToBolter()
    {
        if (hasBolter)
        {
            DisableAllWeapons();
            bolter.SetActive(true);
        }
    }

    public void SwitchToOrbLauncher()
    {
        if (hasOrbLauncher)
        {
            DisableAllWeapons();
            orbLauncher.SetActive(true);
        }
    }

    public void SwitchToDualSMGs()
    {
        if (hasTwinSmgs)
        {
            DisableAllWeapons();
            twinSmgs.SetActive(true);
        }
    }

    void DisableAllWeapons()
    {
        bolter.SetActive(false);
        orbLauncher.SetActive(false);
        twinSmgs.SetActive(false);
    }
}
