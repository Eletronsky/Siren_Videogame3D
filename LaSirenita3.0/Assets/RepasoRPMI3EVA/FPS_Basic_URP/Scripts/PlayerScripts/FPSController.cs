using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    //Referencias privadas
    Rigidbody rb;
    Animator anim;
    //Valores privados
    Vector2 move;
    Vector2 look;
    float lookRotation;

    [Header("Movement & Look Stats")]
    [SerializeField] GameObject camHolder;
    public float speed, maxForce, sensitivity;

    [Header("Jumping & GroundCheck Configuration")]
    public float jumpForce;
    //Groundcheck
    [SerializeField] GameObject groundCheck;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDetectRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        camHolder = GameObject.Find("CameraHolder");
        groundCheck = GameObject.Find("GroundCheck");
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        
    }

    private void LateUpdate()
    {
        

    }

    void Movement()
    {
        
    }

    void CameraMoveLook()
    {


    }

    void Jump()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }
}
