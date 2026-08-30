using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.indianRed;
    public float duration = .2f;

    private Tween _currentTween;

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();

        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        { 
            spriteRenderers.Add(child);
        }
    }

    public void Flash()
    { 
        if(_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(i => i.color = Color.white);
        }

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            _currentTween = spriteRenderer.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
