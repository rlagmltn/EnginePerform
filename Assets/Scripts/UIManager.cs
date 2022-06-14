using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TutorialText;
    public GameObject Inventory;
    bool istutorialTurn = true;
    bool isInventoryTurn = false;

    private void Update()
    {
        if (Input.GetButtonDown("P"))
        {
            istutorialTurn = !istutorialTurn;
            TutorialText.SetActive(istutorialTurn);
        }
        if (Input.GetButtonDown("Tab"))
        {
            isInventoryTurn = !isInventoryTurn;
            Inventory.SetActive(isInventoryTurn);
        }
    }
}
