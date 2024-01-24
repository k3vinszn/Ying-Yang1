using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip Highlight_botao;
    public AudioClip PressDown_botao;

    private AudioSource audioSource;
    private Button button;

    void Start()
    {
        audioSource = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        button = GetComponent<Button>();

        // Subscribe to button events
        button.onClick.AddListener(OnButtonPressed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play the highlight sound
        audioSource.PlayOneShot(Highlight_botao);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play the press sound
        audioSource.PlayOneShot(PressDown_botao);

        // Add any additional functionality for button press here
    }

    void OnButtonPressed()
    {
        // Add any additional functionality for button press here
    }
}


