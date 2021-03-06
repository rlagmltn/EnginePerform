using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject tutorialText;
    public GameObject helpPanel;
    public GameObject tutotutoText;
    public GameObject MenuPanel;
    bool isHelpTurn = false;
    bool isMenuTurn = false;

    [SerializeField]
    Slider BGSoundSlider;
    [SerializeField]
    Slider EFSoundSlider;
    [SerializeField]
    AudioSource[] sound;

    [SerializeField]
    private Transform MenuON;
    private void Awake()
    {
        helpPanel.transform.position = MenuON.position;
        MenuPanel.transform.position = MenuON.position;
        helpPanel.SetActive(isHelpTurn);
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
    void OnValueChanged(string _key, Slider _slider)
    {
        PlayerPrefs.SetFloat(_key, _slider.value);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Tab"))
        {
            MenuTurn(ref isHelpTurn, helpPanel);
            tutotutoText.SetActive(false);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            MenuTurn(ref isMenuTurn, MenuPanel);
        }
        sound[0].volume = BGSoundSlider.value;
        sound[1].volume = EFSoundSlider.value;
    }
    void MenuTurn(ref bool _input, GameObject _obj)
    {
        _input = !_input;
        if (_input)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        _obj.SetActive(_input);
    }
    public void OnClickContinueButton()
    {
        MenuTurn(ref isMenuTurn, MenuPanel);
    }
    public void OnClickMenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void OnClickQuitButton()
    {
        Application.Quit(); 
    }
}
