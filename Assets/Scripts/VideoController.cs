using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoController : MonoBehaviour
{
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public Button playButton;
    public Button toggleVideoButton; 
    public Slider videoSlider;

    private bool isDragging = false;

    void Start()
    {
        videoPanel.SetActive(false); // Blendet das Panel aus
        playButton.onClick.AddListener(ToggleVideoPanel);
        toggleVideoButton.onClick.AddListener(ToggleVideoPlayback);

        // Setzt den Maximalwert des Schiebereglers auf die Länge des Videos
        videoPlayer.prepareCompleted += PrepareCompleted; 
        videoSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        // Wenn es nicht gezogen wird, wird der Schiebereglerwert basierend auf der Videowiedergabezeit aktualisiert
        if (!isDragging)
        {
            videoSlider.value = (float)videoPlayer.time;
        }
    }

    // Ändert die Sichtbarkeit des Panels
    void ToggleVideoPanel()
    {
        videoPanel.SetActive(!videoPanel.activeSelf); 
    }

    // Verwaltet die Videowiedergabe und Pause
    void ToggleVideoPlayback()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause(); 
        }
        else
        {
            videoPlayer.Play();
        }
    }

    void PrepareCompleted(VideoPlayer vp)
    {
        videoSlider.maxValue = (float)vp.length;
    }

    // Legt die Videowiedergabezeit auf einen Schiebereglerwert fest
    void OnSliderValueChanged(float value)
    {
        if (isDragging)
        {
            videoPlayer.time = value;
        }
    }

    public void OnPointerDown()
    {
        isDragging = true;
    }

    // Aktualisiert die Videozeit, nachdem das Ziehen abgeschlossen ist
    public void OnPointerUp()
    {
        isDragging = false;
        videoPlayer.time = videoSlider.value;
    }
}

