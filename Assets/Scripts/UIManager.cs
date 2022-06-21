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
    bool istutorialTurn = true;
    bool isHelpTurn = false;
    bool istutotutoTurn = true;
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
        tutorialText.SetActive(istutorialTurn);
        tutotutoText.SetActive(istutotutoTurn);
        MenuPanel.SetActive(isMenuTurn);
        BGSoundSlider.value = 100;
        EFSoundSlider.value = 100;
    }
    private void Update()
    {
        if (Input.GetButtonDown("P"))
        {
            istutorialTurn = !istutorialTurn;
            tutorialText.SetActive(istutorialTurn);
        }
        if (Input.GetButtonDown("Tab"))
        {
            isHelpTurn = !isHelpTurn;
            if (isHelpTurn)
            {
                Time.timeScale = 0f;
            }
            else
            {
                if (!istutorialTurn)
                {
                    istutotutoTurn = false;
                    tutotutoText.SetActive(false);
                }
                Time.timeScale = 1f;
            }
            helpPanel.SetActive(isHelpTurn);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            MenuTurn(ref isMenuTurn);
        }
        sound[0].volume = BGSoundSlider.value;
        sound[1].volume = EFSoundSlider.value;
    }
    void MenuTurn(ref bool _input)
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
        MenuPanel.SetActive(_input);
    }
    public void OnClickContinueButton()
    {
        MenuTurn(ref isMenuTurn);
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
