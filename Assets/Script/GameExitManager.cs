using UnityEngine;

public class GameExitManager : MonoBehaviour
{
    void Update()
    {
        // Nếu người chơi nhấn phím Escape, thoát game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Thoát game trên bản build
            Application.Quit();
        }
    }
}
