using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierAnimationStateController : MonoBehaviour
{
    [SerializeField] Animator animator;

    string nameStateWalking = "isWalking";
    string nameStateRunning = "isRunning";

    void Start()
    {
        
    }

    void Update()
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
