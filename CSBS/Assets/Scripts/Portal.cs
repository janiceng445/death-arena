using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int DoorNumber = 0;
    private int SelectedDoor = 0;
    private bool IsEntering = false;

    void Start() {
        if (DoorNumber == 0) {
            Debug.Log("Need to assign door number.");
        }
    }

    void OnMouseEnter() {
        Debug.Log("Enter");
        // Change animation
        // TODO
        // Set SelectedDoor
        SelectedDoor = DoorNumber;
    }

    void OnMouseExit() {
        // Reset SelectedDoor
        SelectedDoor = 0;
    }

    void OnMouseDown() {
        Debug.Log("Clicked on " + SelectedDoor);
        IsEntering = true;
    }

    void Update() {
        // Check if clicked
        if (IsEntering) {
            ChangeScene(SelectedDoor);
        }
    }

    void ChangeScene(int num) {
        switch(num) {
            case 1:
                SceneManager.LoadScene("Scenes/Rooms/VTA");
            break;
            case 2:
                SceneManager.LoadScene("Scenes/Rooms/Placeholder");
            break;
            case 3:
                SceneManager.LoadScene("Scenes/Rooms/Stomach");
            break;
            case 4:
                SceneManager.LoadScene("Scenes/Rooms/Amygdala");
            break;
            case 5:
                SceneManager.LoadScene("Scenes/MainScene");
            break;
        }
    }

}
