using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    public Slider volume_Slider;
    public GameObject startMenu;
    public GameObject settingsMenu;
    public GameObject levelsMenu;


    private void Start()
    {
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
        levelsMenu.SetActive(false);
    }


    public void ChangeVolume()
    {
        AudioListener.volume = volume_Slider.value;
    }


    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
