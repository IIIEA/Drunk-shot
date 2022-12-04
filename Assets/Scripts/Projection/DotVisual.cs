using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class DotVisual : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetAlpha(float alpha)
    {
        _spriteRenderer.color = Utils.SetSpriteAlpha(_spriteRenderer, alpha);
    }
}
