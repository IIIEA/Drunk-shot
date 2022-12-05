using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ThemePresentor : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    private Button _button;

    public event Action<Sprite> ButtonClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => ButtonClicked?.Invoke(_sprite));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => ButtonClicked?.Invoke(_sprite));
    }
}
