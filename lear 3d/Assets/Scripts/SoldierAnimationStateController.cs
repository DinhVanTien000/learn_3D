using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierAnimationStateController : MonoBehaviour
{
    [SerializeField] Animator animator;

    string nameStateWalking = "isWalking";
    string nameStateRunning = "isRunning";
    string nameParamVelocity = "Velocity";

    float velocity = 0f;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float deceleration = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OneDimensionalBlendTrees()
    {
        bool forwatdPressed = Keyboard.current.wKey.isPressed;
        bool runPressed = Keyboard.current.dKey.isPressed;

        if (forwatdPressed && velocity < 1)
        {
            velocity += Time.deltaTime * acceleration;
        }

        if (!forwatdPressed && velocity > 0)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!forwatdPressed && velocity < 0) velocity = 0;

        animator.SetFloat(nameParamVelocity, velocity);
    }

    void ControllState()
    {
        bool forwatdPressed = Keyboard.current.wKey.isPressed;
        bool runPressed = Keyboard.current.dKey.isPressed;

        if (forwatdPressed)
        {
            animator.SetBool(nameStateWalking, true);
        }

        if (!forwatdPressed)
        {
            animator.SetBool(nameStateWalking, false);
        }

        if (runPressed)
        {
            animator.SetBool(nameStateRunning, true);
        }

        if (!runPressed)
        {
            animator.SetBool(nameStateRunning, false);
        }
    }
}
