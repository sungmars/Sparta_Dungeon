using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float baseSpeed = 5f; // ✅ 기본 속도를 명확하게 고정
    private float currentSpeed; // ✅ 현재 속도를 저장
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContain;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouse;
    public bool canLook = true;

    [Header("Stamina")]
    public float staminaCostPerJump = 10f;

    private Rigidbody rb;
    private Condition condition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        condition = GetComponent<Condition>();
        currentSpeed = baseSpeed; // ✅ 현재 속도를 기본 속도로 초기화
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    void CameraLook()
    {
        camCurXRot += mouse.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContain.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouse.x * lookSensitivity, 0);
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= currentSpeed; // ✅ 무조건 현재 속도를 적용!
        dir.y = rb.velocity.y;
        rb.velocity = dir;

        Debug.Log($"[PlayerController] 이동 중! 현재 이동 속도 적용됨: {currentSpeed}");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouse = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && condition.UseStamina(staminaCostPerJump))
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position+(transform.forward*0.2f)+(transform.up*0.2f),Vector3.down),
            new Ray(transform.position+(-transform.forward*0.2f)+(transform.up*0.2f),Vector3.down),
            new Ray(transform.position+(transform.right*0.2f)+(transform.up*0.2f),Vector3.down),
            new Ray(transform.position+(-transform.right*0.2f)+(transform.up*0.2f),Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

 
    public void ApplySpeedBuff(float amount)
    {
        currentSpeed += amount;
        Debug.Log($"[PlayerController] 속도 증가! 현재 속도: {currentSpeed}");
    }

    public void ResetSpeed()
    {
        currentSpeed = baseSpeed;
        Debug.Log($"[PlayerController] 속도 복귀! 현재 속도: {currentSpeed}");
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
