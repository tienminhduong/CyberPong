using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] GameObject buttonUI, inGameUI;
    public void Entrance() {
        inGameUI.SetActive(true);
    }
    public void BackMainMenu() {
        buttonUI.SetActive(true);
    }
}
