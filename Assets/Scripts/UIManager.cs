using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private Transform MenuON;
    private void Awake()
    {
        helpPanel.transform.position = MenuON.position;
        MenuPanel.transform.position = MenuON.position;
        helpPanel.SetActive(isHelpTurn);
        tutorialText.SetActive(istutorialTurn);
        tutotutoText.SetActive(istutotutoTurn);
        MenuPanel.SetActive(isMenuTurn);
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
            isMenuTurn = !isMenuTurn;
            if (isMenuTurn)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            MenuPanel.SetActive(isMenuTurn);
        }
    }
}
