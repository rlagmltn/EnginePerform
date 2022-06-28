using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject MenuPanel;
    bool isMenuTurn = false;
    [SerializeField]
    Slider BGSoundSlider;
    [SerializeField]
    Slider EFSoundSlider;

    [SerializeField]
    private Transform MenuON;
    [SerializeField]
    AudioSource[] sound;
    void Awake()
    {
        MenuPanel.transform.position = MenuON.position;
        MenuPanel.SetActive(isMenuTurn);
        if (!PlayerPrefs.HasKey("BGSOUND"))
        {
            PlayerPrefs.SetFloat("BGSOUND", 100);
        }
        else
        {
            BGSoundSlider.value = PlayerPrefs.GetFloat("BGSOUND");

        }
        if (!PlayerPrefs.HasKey("EFSOUND"))
        {
            PlayerPrefs.SetFloat("EFSOUND", 100);
        }
        else
        {
            EFSoundSlider.value = PlayerPrefs.GetFloat("EFSOUND");
        }
        BGSoundSlider.onValueChanged.AddListener(delegate { OnValueChanged("BGSOUND", BGSoundSlider); });
        EFSoundSlider.onValueChanged.AddListener(delegate { OnValueChanged("EFSOUND", EFSoundSlider); });
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            MenuTurn(ref isMenuTurn, MenuPanel);
        }
    }
    void OnValueChanged(string _key, Slider _slider)
    {
        PlayerPrefs.SetFloat(_key, _slider.value);
    }
    void MenuTurn(ref bool _input, GameObject _obj)
    {
        _input = !_input;
        _obj.SetActive(_input);
    }
    public void OnClickOptionButton()
    {
        MenuTurn(ref isMenuTurn, MenuPanel);
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
