using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [Header("Inputs keys")]
    public string playerActionsMap = "Player";
    public string inputHorizontalKey = "Horizontal";
    public string inputRunKey = "Run";
    public string inputJumpKey = "Jump";
    public string inputShootKey = "Shoot";

    private ScarbornInputSystem _inputActions;
    private InputActionMap _playerActionsMap;

    protected InputAction horizontalAction;
    protected InputAction runAction;
    protected InputAction jumpAction;
    protected InputAction shootAction;

    private void Awake()
    {
        LoadInputs();
    }

    protected void LoadInputs()
    {
        _inputActions = new ScarbornInputSystem();
        _playerActionsMap = _inputActions.asset.FindActionMap(playerActionsMap);
        horizontalAction = _playerActionsMap.FindAction(inputHorizontalKey);
        runAction = _playerActionsMap.FindAction(inputRunKey);
        jumpAction = _playerActionsMap.FindAction(inputJumpKey);
        shootAction = _playerActionsMap.FindAction(inputShootKey);
    }

    private void OnEnable()
    {
        horizontalAction.Enable();
        runAction.Enable();
        jumpAction.Enable();
        shootAction.Enable();
    }

    private void OnDisable()
    {
        horizontalAction.Disable();
        runAction.Disable();
        jumpAction.Disable();
        shootAction.Disable();
    }
}
