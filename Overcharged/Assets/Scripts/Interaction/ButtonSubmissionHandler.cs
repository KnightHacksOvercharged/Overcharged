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

        if (currentMenu == null || menuToOpen == null)
        {
            yield break;
        }

        yield return currentMenu.DOFade(0, .5f).SetEase(Ease.InOutQuad).WaitForCompletion();

        currentMenu.gameObject.SetActive(false);

        menuToOpen.gameObject.SetActive(true);

        yield return menuToOpen.DOFade(1, .5f).SetEase(Ease.InOutQuad).WaitForCompletion();
    }
}
