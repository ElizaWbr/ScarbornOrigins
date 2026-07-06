using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;

public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animations")]
    public float duration = .5f;
    public float delay = .2f;
    public Ease ease = Ease.OutBack;

    void Awake()
    {
        HideAllButtons();
        DisplayAllButtons();
    }
    private void HideAllButtons()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    private void DisplayAllButtons() 
    {
        foreach (var b in buttons)
        {
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetEase(ease).SetDelay(delay);
        }
    }
}
