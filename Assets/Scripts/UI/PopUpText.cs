using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PopUpText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _yOffset;

    public void Init(string text)
    {
        _text.text = text;
    }

    private void OnEnable()
    {
        StartCoroutine(PopUp());
    }

    private IEnumerator PopUp()
    {
        var moveTween = transform.DOMoveY(transform.position.y + _yOffset, 0.5f);

        yield return moveTween.WaitForCompletion();

        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));

        while (true)
        {
            _text.color = Utils.SetAlpha(_text, 0);

            yield return null;
        }
    }
}
