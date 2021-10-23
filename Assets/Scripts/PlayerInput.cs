using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEngine.InputSystem.PlayerInput input;

    [SerializeField] private HeadLeg headLeg;
    [SerializeField] private CharacterSelection characterSelection;

    private int counter = 0;

    public int Id => input.playerIndex;

    private void Awake()
    {
        gameObject.SetActive(true);

        input.onDeviceLost += playerInput => Destroy(gameObject);
    }

    public void Movement(InputAction.CallbackContext ctx)
    {
        headLeg.rotationInput = -ctx.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        headLeg.ContractLeg(ctx.canceled);
        if (ctx.started)
            characterSelection.Approve();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
