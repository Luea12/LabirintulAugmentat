using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    private ProfileData profile;
    public Slider volumeSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        profile = ProfileData.Load();
        volumeSlider.value = profile.GetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void SaveChanges() {
        profile.UpdateVolume(volumeSlider.value);
    }

    public void BackToMenu() {
        SaveChanges();
        SceneManager.LoadSceneAsync("start");
    }

}
