using UnityEngine;
using UnityEngine.UI;

public class ThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemePresentor[] _themeButtons;
    [SerializeField] private Image _theme;

    private void OnEnable()
    {
        foreach (var theme in _themeButtons)
        {
            theme.ButtonClicked += OnThemeButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (var theme in _themeButtons)
        {
            theme.ButtonClicked -= OnThemeButtonClicked;
        }
    }

    private void OnThemeButtonClicked(Sprite sprite)
    {
        _theme.sprite = sprite;
    }
}
