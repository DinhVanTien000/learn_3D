using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierAnimationStateController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animator Parameters")]
    [SerializeField] private string nameStateWalking = "isWalking";
    [SerializeField] private string nameStateRunning = "isRunning";
    [SerializeField] private string nameParamVelocity = "Velocity";

    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float deceleration = 0.5f;
    [SerializeField] private float accelerationTow = 2f;
    [SerializeField] private float decelerationTow = 2f;
    [SerializeField] private float maxWalkVelocity = 0.5f;
    [SerializeField] private float maxRunVelocity = 2f;

    private float velocity = 0f;
    private float velocityZ = 0f;
    private float velocityX = 0f;

    private void Update()
    {
        TwoDimensionalBlendTrees();
    }

    private void TwoDimensionalBlendTrees()
    {
        // --- Input ---
        bool forwardPressed = Keyboard.current.wKey.isPressed;
        bool leftPressed = Keyboard.current.dKey.isPressed;
        bool rightPressed = Keyboard.current.aKey.isPressed;
        bool runPressed = Keyboard.current.hKey.isPressed;

        float maxVelocityCurrent = runPressed ? maxRunVelocity : maxWalkVelocity;

        // --- Xử lý Z (tiến/lùi) ---
        if (forwardPressed)
        {
            if (velocityZ < maxVelocityCurrent)
            {
                velocityZ += Time.deltaTime * accelerationTow;
                if (velocityZ > maxVelocityCurrent) velocityZ = maxVelocityCurrent;
            }
            else if (velocityZ > maxVelocityCurrent)
            {
                velocityZ -= Time.deltaTime * decelerationTow;
                if (velocityZ < maxVelocityCurrent) velocityZ = maxVelocityCurrent;
            }
        }
        else
        {
            if (velocityZ > 0)
            {
                velocityZ -= Time.deltaTime * decelerationTow;
                if (velocityZ < 0) velocityZ = 0;
            }
        }

        // --- X (trái/phải) ---
        float targetVelocityX = 0f;
        if (leftPressed) targetVelocityX = -maxVelocityCurrent;
        if (rightPressed) targetVelocityX = maxVelocityCurrent;

        // Tăng tốc hoặc giảm tốc về targetVelocityX
        if (velocityX < targetVelocityX)
        {
            velocityX += Time.deltaTime * accelerationTow;
            if (velocityX > targetVelocityX) velocityX = targetVelocityX;
        }
        if (velocityX > targetVelocityX)
        {
            velocityX -= Time.deltaTime * decelerationTow;
            if (velocityX < targetVelocityX) velocityX = targetVelocityX;
        }

        // Dừng hẳn khi gần 0
        if (!leftPressed && !rightPressed && Mathf.Abs(velocityX) < 0.05f) velocityX = 0;

        // --- Update Animator ---
        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }


    private void OneDimensionalBlendTrees()
    {
        bool forwardPressed = Keyboard.current.wKey.isPressed;
        bool runPressed = Keyboard.current.dKey.isPressed;

        if (forwardPressed && velocity < 1)
        {
            velocity += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocity > 0)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!forwardPressed && velocity < 0)
        {
            velocity = 0;
        }

        animator.SetFloat(nameParamVelocity, velocity);
    }

    private void ControllState()
    {
        bool forwardPressed = Keyboard.current.wKey.isPressed;
        bool runPressed = Keyboard.current.dKey.isPressed;

        animator.SetBool(nameStateWalking, forwardPressed);
        animator.SetBool(nameStateRunning, runPressed);
    }
}
