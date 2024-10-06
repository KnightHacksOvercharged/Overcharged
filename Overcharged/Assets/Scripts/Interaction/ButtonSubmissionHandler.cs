using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement; // To handle scene loading

public class ButtonSubmissionHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup currentMenu;
    [SerializeField] private CanvasGroup menuToOpen;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField]
    [Range(0, 1f)] private float volume;
    private Tween fadeTween;

    public void HandleButtonSubmission()
    {
        // SoundManager.Instance.PlayUIInteractionClip(buttonClick, this.transform, volume);

        // Start the fade-out effect and load the leaderboard scene after fading out
        // FadeOut(1f, LoadLeaderboardScene);

        StartCoroutine(IHandleButtonSubmission());
    }

    private IEnumerator IHandleButtonSubmission()
    {
        yield return null;

        SoundManager.Instance.PlayUIInteractionClip(buttonClick, this.transform, volume);

        yield return currentMenu.DOFade(0, .5f).SetEase(Ease.InOutQuad).WaitForCompletion();

        yield return menuToOpen.DOFade(1, .5f).SetEase(Ease.InOutQuad).WaitForCompletion();




    }

    // // Method to fade in when the new scene starts
    // public void FadeIn(float duration)
    // {
    //     fade(1f, duration, () => {
    //         canvasGroup.interactable = true;
    //         canvasGroup.blocksRaycasts = true;
    //     });
    // }

    // // Method to fade out and execute a callback (like loading the new scene) afterward
    // public void FadeOut(float duration, TweenCallback onComplete)
    // {
    //     fade(0f, duration, () => {
    //         canvasGroup.interactable = false;
    //         canvasGroup.blocksRaycasts = false;
    //         onComplete(); // Load the new scene after fade-out completes
    //     });
    // }

    // // Helper function to fade in or out
    // private void fade(float endValue, float duration, TweenCallback onEnd)
    // {
    //     if (fadeTween != null)
    //     {
    //         fadeTween.Kill(false); // Stop any previous tween
    //     }

    //     fadeTween = canvasGroup.DOFade(endValue, duration);
    //     fadeTween.onComplete += onEnd;
    // }

    // // Callback to load the leaderboard scene after the fade-out completes
    // private void LoadLeaderboardScene()
    // {
    //     SceneManager.LoadScene(""); // Use the correct scene name or build index
    // }

    // // Optional: In the new scene, you can use this to fade in once the scene loads
    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     FadeIn(1f);
    // }

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }
}
