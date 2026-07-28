using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [Header("Inputs keys")]
    public string playerActionsMap = "Player";
    public string inputHorizontalKey = "Horizontal";
    public string inputRunKey = "Run";
    public string inputJumpKey = "Jump";

    private ScarbornInputSystem _inputActions;
    private InputActionMap _playerActionsMap;

    protected InputAction horizontalAction;
    protected InputAction runAction;
    protected InputAction jumpAction;

    private void Awake()
    {
        _inputActions = new ScarbornInputSystem();
        _playerActionsMap = _inputActions.asset.FindActionMap(playerActionsMap);
        horizontalAction = _playerActionsMap.FindAction(inputHorizontalKey);
        runAction = _playerActionsMap.FindAction(inputRunKey);
        jumpAction = _playerActionsMap.FindAction(inputJumpKey);
    }

    private void OnEnable()
    {
        horizontalAction.Enable();
        runAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        horizontalAction.Disable();
        runAction.Disable();
        jumpAction.Disable();
    }
}
