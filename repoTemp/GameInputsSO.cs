using UnityEngine;

[CreateAssetMenu(fileName = "GameInput", menuName = "InputSO/GameInputSO")]
public class GameInputsSO : ScriptableObject
{
    public InputActions GameInputsObj;

    private void OnEnable()
    {
        GameInputsObj = new InputActions();
    }
}