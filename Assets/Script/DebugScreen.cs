using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{

    World world;
    Text text;

    float frameRate;
    float timer;

    int halfWorldSizeInVoxels;
    int halfWorldSizeInChunks;

    void Start()
    {

        world = GameObject.Find("World").GetComponent<World>();
        text = GetComponent<Text>();

        halfWorldSizeInVoxels = VoxelData.WorldSizeInVoxels / 2;
        halfWorldSizeInChunks = VoxelData.WorldSizeInChunks / 2;

    }

    void Update()
    {

        // Thông tin về nhóm và các thành viên
        string debugText = "NHOM 9 : XAY DUNG MO PHONG GAME MINECRAFT TRONG UNITY";
        debugText += "\n";  // Xuống dòng
        debugText += "Nguyen Tuan Minh";
        debugText += "\n";  // Xuống dòng
        debugText += "Cao Tran Duc Manh";
        debugText += "\n";  // Xuống dòng
        debugText += "Nguyen Vu Minh Long";
        debugText += "\n";  // Xuống dòng
        debugText += "Nguyen Thanh Nam";
        debugText += "\n";  // Xuống dòng
        debugText += "Vu Hai Long";
        debugText += "\n\n";  // Xuống dòng 2 lần để tạo khoảng trống

        debugText += frameRate + " fps";
        debugText += "\n\n";
        debugText += "XYZ: " + (Mathf.FloorToInt(world.player.transform.position.x) - halfWorldSizeInVoxels) + " / " + Mathf.FloorToInt(world.player.transform.position.y) + " / " + (Mathf.FloorToInt(world.player.transform.position.z) - halfWorldSizeInVoxels);
        debugText += "\n";
        debugText += "Chunk: " + (world.playerChunkCoord.x - halfWorldSizeInChunks) + " / " + (world.playerChunkCoord.z - halfWorldSizeInChunks);



        text.text = debugText;

        if (timer > 1f)
        {

            frameRate = (int)(1f / Time.unscaledDeltaTime);
            timer = 0;

        }
        else
            timer += Time.deltaTime;

    }
}