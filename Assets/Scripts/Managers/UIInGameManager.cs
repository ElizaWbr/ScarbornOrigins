using UnityEngine;
using TMPro;
using Core.Singleton;

public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextFairies;
    public static void UpdateTextFairies(string fairies) 
    {
        Instance.uiTextFairies.text = fairies;
    }
}
