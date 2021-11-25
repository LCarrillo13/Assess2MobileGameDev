using NullFrameworkException.Mobile.InputHandling;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Movement stuff
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float strafeSpeed = 5f;
    public float rotationalSpeed = 5f;
    private CharacterController characterController;
    //Camera stuff
    private float cameraRotation;
    public Camera playerCamera;
    public float tiltSpeed = 5f;
    public float maxTiltAngle = 45f;
    // Health stuff
    private int playerHealth = 100;
    private int maxHealth = 100;
    [SerializeField] private Text healthText;
    [SerializeField] private Transform spawnPoint;
    // Mobile stuff
    private JoystickInputHandler joystickInputHandler;
    
    
    
#region Setup
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
       joystickInputHandler = FindObjectOfType<JoystickInputHandler>();
    }

#endregion
    
#region Movement Stuff
    void MovePlayer()
    {
        // Forward Back Left Right movement
        float currentSpeed = IsRunning()
            ? runSpeed
            : walkSpeed;
        float forwardSpeed = ForwardDirection() * currentSpeed;
        Vector3 movementDirection = (transform.forward * forwardSpeed)+(transform.right*SidewaysDirection()*strafeSpeed);
        characterController.Move(movementDirection * Time.deltaTime);
        // Rotational movement
        transform.rotation *= Quaternion.Euler(0,RotationY()*rotationalSpeed,0);
        // Tilt camera
        cameraRotation += TiltCamera() * tiltSpeed;
        cameraRotation = Mathf.Clamp(cameraRotation, -maxTiltAngle, maxTiltAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation,0,0);
    }

    bool IsRunning()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }

        return false;
    }

    float ForwardDirection()
    {
        if(Input.GetKey(KeyCode.W))
        {
            return 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            return -1;
        }

        return OnScreenVertical;
    }

    float SidewaysDirection()
    {
        if(Input.GetKey(KeyCode.D))
        {
            return 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            return -1;
        }

        return OnScreenHorizontal;
    }

    private float onScreenUpValue;
    private float onScreenDownValue;
    private float onScreenLeftValue;
    private float onScreenRightValue;

    private float OnScreenHorizontal
    {
        get { return onScreenRightValue - onScreenLeftValue; }
    }
    private float OnScreenVertical
    {
        get { return onScreenUpValue - onScreenDownValue; }
    }
    
    
    public void OnUpButtonDown()
    {
        onScreenUpValue = 1;
    }
    public void OnUpButtonUp()
    {
        onScreenUpValue = 0;
    }
    
    public void OnDownButtonDown()
    {
        onScreenDownValue = 1;
    }
    public void OnDownButtonUp()
    {
        onScreenDownValue = 0;
    }
    
    public void OnLeftButtonDown()
    {
        onScreenLeftValue = 1;
    }
    
    public void OnLeftButtonUp()
    {
        onScreenLeftValue = 0;
    }
    
    public void OnRightButtonDown()
    {
        onScreenRightValue = 1;
    }
    
    public void OnRightButtonUp()
    {
        onScreenRightValue = 0;
    }
    
    
#endregion

#region Camera Stuff

    float RotationY()
    {
        return joystickInputHandler.Axis.x;
        return Mathf.Clamp(Input.GetAxis("Mouse X") + joystickInputHandler.Axis.x,-1,1);
    }

    float TiltCamera()
    {
        return -joystickInputHandler.Axis.y;
        return Mathf.Clamp( -Input.GetAxis("Mouse Y") + -joystickInputHandler.Axis.y,-1,1);
    }
    
#endregion

#region Health Stuff
    
    void LoseHealth()
    {
        playerHealth -= 10;
        if(playerHealth < 10)
        {
            PlayerDie();
        }
        //TODO UI Update
    }

    void PlayerDie()
    {
        transform.position = spawnPoint.position;
        playerHealth = maxHealth;
        //TODO UI Update
    }
#endregion
    
   
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            LoseHealth();
        }
    }
}
