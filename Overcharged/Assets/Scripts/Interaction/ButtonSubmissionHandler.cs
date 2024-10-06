using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSubmissionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip buttonClick;
    [SerializeField]
    [Range(0, 1f)] private float volume;

    public void HandleButtonSubmission()
    {
        SoundManager.Instance.PlayUIInteractionClip(buttonClick, this.transform, volume);

    }
}
