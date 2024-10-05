using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public float electricityBillTotal = 0f;

    [Header("All Interactables")]
    [SerializeField] private List<IInteractable> interactableObjects = new List<IInteractable>();

    [Header("Time")]
    public float remainingTime;
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
        StartCoroutine(IStartGame());
        StartCoroutine(ITurnOnItems());
    }

    private IEnumerator IStartGame()
    {
        yield return new WaitForSeconds(remainingTime);

        StopAllCoroutines();

        Debug.Log("END GAME");
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

            interactableObjects[randomIndex].Activate();
        }
    }
}
