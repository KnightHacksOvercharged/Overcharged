using Assets.Database;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardDisplay : MonoBehaviour
{
    async void Start()
    {
        var scores = await Database.GetDisplayNamesAndScoresAsync();

        for (int i = 0; i < scores.Count; i++)
        {
            var (DisplayName, BestScore) = scores[i];

            GameObject textObject = new("LeaderboardEntry");
            textObject.transform.SetParent(transform);

            HorizontalLayoutGroup layoutGroup = textObject.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.padding = new RectOffset(30, 30, 0, 0);

            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(750, rectTransform.sizeDelta.y);
     
            GameObject rankObject = new("Rank");
            rankObject.transform.SetParent(textObject.transform);
            TextMeshProUGUI rankText = rankObject.AddComponent<TextMeshProUGUI>();
            rankText.text = "" + (i + 1);
            rankText.fontSize = 45;
            rankText.color = Color.black;
            rankText.alignment = TextAlignmentOptions.Left;
        
            GameObject nameObject = new("DisplayName");
            nameObject.transform.SetParent(textObject.transform);
            TextMeshProUGUI nameText = nameObject.AddComponent<TextMeshProUGUI>();
            nameText.text = DisplayName;
            nameText.fontSize = 45;
            nameText.color = Color.black;
            nameText.alignment = TextAlignmentOptions.Center;
        
            GameObject scoreObject = new("BestScore");
            scoreObject.transform.SetParent(textObject.transform);
            TextMeshProUGUI scoreText = scoreObject.AddComponent<TextMeshProUGUI>();
            scoreText.text = "$" + BestScore.ToString();
            scoreText.fontSize = 45;
            scoreText.color = Color.black;
            scoreText.alignment = TextAlignmentOptions.Right;
        }
    }

    
}
