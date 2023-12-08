using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private GameObject _joystickTutorial;
    [SerializeField] private GameObject _keyboardTutorial;
    [SerializeField] private GameObject _tutorialMenu;

    public void ShowJoystickTutorial()
    {
        _joystickTutorial.SetActive(true);
        _keyboardTutorial.SetActive(false);
        _tutorialMenu.SetActive(true);
    }

    public void ShowKeyboardTutorial()
    {
        _joystickTutorial.SetActive(false);
        _keyboardTutorial.SetActive(true);
        _tutorialMenu.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorialMenu.SetActive(false);
    }
}
