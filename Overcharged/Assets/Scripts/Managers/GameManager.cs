using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public float electricityBillTotal = 0f;
    [SerializeField] private TextMeshProUGUI electricityBillText;
    [SerializeField] private CanvasGroup leaderboard;

    [Header("All Interactables")]
    [SerializeField] private List<GameObject> interactableObjects = new List<GameObject>();
    private List<IInteractable> interactables = new List<IInteractable>();

    [Header("Time")]
    [SerializeField] private float leaderboardAnimationTime;
    [SerializeField] private float countdownTimer;
    [Space]
    [SerializeField] private float minTimeToTurnOn;
    [SerializeField] private float maxTimeToTurnOn;

    [Header("Number of Items to Turn on at Single Interval")]
    [SerializeField] private int minItemsToTurnOn;
    [SerializeField] private int maxItemsToTurnOn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(IStartGame());
        StartCoroutine(ITurnOnItems());

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Initialize()
    {
        foreach (GameObject interactableObject in interactableObjects)
        {
            IInteractable interactable = interactableObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactables.Add(interactableObject.GetComponent<IInteractable>());
            }
        }
    }

    private IEnumerator IStartGame()
    {
        yield return new WaitForSeconds(countdownTimer);

        GameOver();
    }

    public void GameOver()
    {
        StopAllCoroutines();
        DeactivateAllItems();
        Cursor.lockState = CursorLockMode.None;
        electricityBillText.color = Color.black;
        StartCoroutine(IDisplayLeaderboard());
    }

    private IEnumerator IDisplayLeaderboard()
    {
        var task = Assets.Database.Database.UpdateBestScore((int)electricityBillTotal);

        yield return new WaitUntil(() => task.IsCompleted);

        leaderboard.gameObject.SetActive(true);

        yield return leaderboard.DOFade(1, .5f).SetEase(Ease.InOutQuad).WaitForCompletion();
    }

    private IEnumerator ITurnOnItems()
    {
        TurnOnItems();

        float randomInterval = Random.Range(minTimeToTurnOn, maxTimeToTurnOn);
        yield return new WaitForSeconds(randomInterval);

        StartCoroutine(ITurnOnItems());
    }

    private void TurnOnItems()
    {
        if (interactableObjects.Count == 0)
        {
            return;
        }

        int numDevicesToTurnOn = Random.Range(minItemsToTurnOn, maxItemsToTurnOn);

        for (int i = 0; i < numDevicesToTurnOn; i++)
        {
            int randomIndex = Random.Range(0, interactableObjects.Count);

            if (!interactableObjects[randomIndex].GetComponent<InteractionHandler>().isActive)
            {
                interactables[randomIndex].Activate();
            }
        }
    }

    private void DeactivateAllItems()
    {
        foreach (IInteractable interactable in interactables)
        {
            interactable.Deactivate();
        }
    }

    public float GetCountdownTimerValue()
    {
        return countdownTimer;
    }

    public void AddToScore(int amount)
    {
        GameManager.Instance.electricityBillTotal += amount;
        GameManager.Instance.electricityBillText.text = string.Format("{0:C2}", GameManager.Instance.electricityBillTotal);
    }
}
