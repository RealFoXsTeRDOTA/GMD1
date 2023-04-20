using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
 [SerializeField]
 private AudioMixer audioMixer;
 Resolution[] resolutions;
 [SerializeField]
 private TMP_Dropdown resolutionDropdown; 

 [SerializeField] 
 private Slider volumeSlider;
 public void Start()
 {
  resolutions = Screen.resolutions;
  resolutionDropdown.ClearOptions(); 
  
  List<string> options = new List<string>();
  int currentResolutionIndex = 0;
  //go trough all the resolutions and add them to the list
  for (int i = 0; i < resolutions.Length; i++)
   {
    string option = resolutions[i].width + " x " + resolutions[i].height;
    options.Add(option);
    
    //check if the current resolution is the same as the screen resolution
    if (resolutions[i].width == Screen.currentResolution.width && 
        resolutions[i].height == Screen.currentResolution.height)
    {
     currentResolutionIndex = i;
    }
   }
  //add the list to the dropdown
  resolutionDropdown.AddOptions(options);
  resolutionDropdown.value = currentResolutionIndex;
  resolutionDropdown.RefreshShownValue();
  
  //set the volume to the current volume
  audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume", 0));
  float volumeLevel;
  audioMixer.GetFloat("volume", out volumeLevel);
  volumeSlider.value = volumeLevel;
 }
 
 public void SetResolution(int resolutionIndex)
 {
  Resolution resolution = resolutions[resolutionIndex];
  Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
 }

 public void SetVolume(float volume)
 {
  audioMixer.SetFloat("volume", volume);
 }
 
 public void SetQuality(int qualityIndex)
 {
  QualitySettings.SetQualityLevel(qualityIndex);
 }
 
 public void SetFullscreen(bool isFullscreen)
 {
  Screen.fullScreen = isFullscreen;
 }
}
