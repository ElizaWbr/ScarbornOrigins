using UnityEngine;
using TMPro;
using Core.Singleton;

public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextFairies;
    public TextMeshProUGUI uiTextPotions;
    public TextMeshProUGUI uiTextPapers;

    public static void UpdateTextFairies(string fairies) 
    {
        Instance.uiTextFairies.text = fairies;
    }
    public static void UpdateTextPotions(string potions)
    {
        Instance.uiTextPotions.text = potions;
    }
    public static void UpdateTextPapers(string papers)
    {
        Instance.uiTextPapers.text = papers;
    }
}
