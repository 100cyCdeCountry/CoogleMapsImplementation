using UnityEngine;

public enum ButtonState
{
    Fail,
    Correct,
    Normal,
    Selected    
}

public interface IContestDisplay
{
    void ShowButtons(string[] solutions);
    void DisableButtons();
    void HideButtons();

    void Write(string message);
    int GetLastAnswer();
    void MoveNext();

    void SetColorButton(int i, ButtonState state);
    void SetButtonText(int i, string text);

    GameObject GetCharacter();

    void PlayWin();
    void PlayFail();

}