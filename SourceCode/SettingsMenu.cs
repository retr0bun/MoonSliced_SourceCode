using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audio;
    public Dropdown video;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        video.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].Equals(Screen.currentResolution))
            {
                currentResIndex = i;
            }
        }

        video.AddOptions(options);
        video.value = currentResIndex;
        video.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume (float volume)
    {
        audio.SetFloat("Volume", volume);
    }

    public void Fullscreen (bool FullscreenOn)
    {
        Screen.fullScreen = FullscreenOn;
    }
}
