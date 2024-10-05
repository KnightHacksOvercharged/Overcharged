using System.Collections;
using System.Collections.Generic;
using Assets.Database;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardDisplay : MonoBehaviour
{
    async void Start()
    {
        var scores = await Database.GetDisplayNamesAndScoresAsync();

        foreach(var (DisplayName, BestScore) in scores)
        {
            GameObject textObject = new GameObject("LeaderboardEntry");
            TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
            textObject.transform.SetParent(transform);
            textComponent.text = $"{DisplayName}: {BestScore}";
            textComponent.color = Color.black;
            textComponent.fontSize = 60;
            textComponent.font = Resources.GetBuiltinResource<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF.asset");
            // GameObject textObject = new GameObject();
            // TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
            // textObject.transform.SetParent(transform);

            // Text textComponent = textObject.AddComponent<Text>();
            // textComponent.text = $"{DisplayName}: {BestScore}";
            // textComponent.color = Color.black;
            // textComponent.fontSize = 60;
            // textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        }
    }
}
