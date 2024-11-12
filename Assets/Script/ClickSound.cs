using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource leftClickAudio;  // Âm thanh cho chuột trái
    public AudioSource rightClickAudio; // Âm thanh cho chuột phải

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 là nút chuột trái
        {
            if (leftClickAudio != null) // Kiểm tra xem AudioSource có tồn tại không
            {
                leftClickAudio.Play(); // Phát âm thanh chuột trái
            }
        }

        if (Input.GetMouseButtonDown(1)) // 1 là nút chuột phải
        {
            if (rightClickAudio != null) // Kiểm tra xem AudioSource có tồn tại không
            {
                rightClickAudio.Play(); // Phát âm thanh chuột phải
            }
        }
    }
}
