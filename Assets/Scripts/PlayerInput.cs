using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEngine.InputSystem.PlayerInput input;

    [SerializeField] private HeadLeg headLeg;
    [SerializeField] private CharacterSelection characterSelection;

    public static bool disabledHeadLegInput;

    private int counter = 0;

    public int Id => input.playerIndex;

    private void Awake()
    {
        gameObject.SetActive(true);

        //input.onDeviceLost += playerInput => Destroy(gameObject);
    }

    public void Movement(InputAction.CallbackContext ctx)
    {
        if (disabledHeadLegInput) return;
        headLeg.rotationInput = -ctx.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            characterSelection.Approve();
            SceneSelection.Instance.Approve();
        }

        if (disabledHeadLegInput) return;
        headLeg.ContractLeg(ctx.canceled);
    }

    public void NextCharacter(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            characterSelection.ChangeCharacter(1);
            SceneSelection.Instance.ChangeSelection(1);
        }
    }
    
    public void PreviousCharacter(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            characterSelection.ChangeCharacter(-1);
            SceneSelection.Instance.ChangeSelection(-1);
        }
    }

    public void Revert(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            characterSelection.ResetApprove();
        }
    }

    public void Restart(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            GameManager.Instance.endGameCanvas.ResetGame();
        }
    }
}
