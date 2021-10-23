using UnityEngine;
using UnityEngine.InputSystem;

public class PressToContinue : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current.IsPressed() || Keyboard.current.IsPressed())
        {
            Destroy(gameObject);
        }
    }
}
