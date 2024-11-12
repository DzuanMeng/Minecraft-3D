using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreativeInventory : MonoBehaviour
{
    // Tham chiếu đến prefab của slot để tạo ra các slot trong Inventory
    public GameObject slotPrefab;
     // Tham chiếu đến đối tượng World trong game
    World world;

    // Danh sách chứa các ItemSlot trong Creative Inventory
    List<ItemSlot> slots = new List<ItemSlot>();

    private void Start()
    {
        // Lấy thành phần World từ GameObject có tên "World"
        world = GameObject.Find("World").GetComponent<World>();

        for (int i = 1; i < world.blocktypes.Length; i++)
        {
            // Lặp qua các loại khối
            // Tạo một slot mới từ prefab và gắn vào Inventory
            GameObject newSlot = Instantiate(slotPrefab, transform);
            // Tạo một ItemStack mới với id là i và số lượng 999
            ItemStack stack = new ItemStack((byte)i, 999);

            // Tạo một ItemSlot mới và đặt trạng thái là Creative
            ItemSlot slot = new ItemSlot(newSlot.GetComponent<UIItemSlot>(), stack);
            slot.isCreative = true;

        }


    }

}