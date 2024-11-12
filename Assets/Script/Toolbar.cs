using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{

    // Mảng chứa các slot trong toolbar, mỗi slot là một UIItemSlot
    public UIItemSlot[] slots;
    // Tham chiếu đến RectTransform của highlight để làm nổi bật slot đang được chọn
    public RectTransform highlight;
    // Tham chiếu đến Player, đại diện cho người chơi
    public Player player;
    // Biến lưu chỉ số của slot hiện đang được chọn
    public int slotIndex = 0;

    private void Start()
    {
        byte index = 1;
        // Lặp qua từng slot trong toolbar
        foreach (UIItemSlot s in slots)
        {
            Debug.Log($"Creating slot for index: {index}");
            // Tạo một ItemStack mới với id là index và số lượng
            ItemStack stack = new ItemStack(index, Random.Range(100, 150));
            // Tạo một ItemSlot mới với UIItemSlot và ItemStack
            ItemSlot slot = new ItemSlot(s, stack);
            index++;

        }
    }

    private void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // CUộn lên
            if (scroll > 0)
                slotIndex--;
            else
                // Cuộn xuống
                slotIndex++;

            if (slotIndex > slots.Length - 1)
                slotIndex = 0;
            if (slotIndex < 0)
                slotIndex = slots.Length - 1;

            // Cập nhật vị trí highlight
            highlight.position = slots[slotIndex].slotIcon.transform.position;
            
        }


    }

    public void AddItem(int blockID, int amount) {
        // Lặp qua từng slot trong toolbar
        for (int i = 0; i < slots.Length; i++)
        {
            // Kiểm tra nếu slot đã có item
            if (slots[i].HasItem)
            {
                // Nếu slot có cùng loại item, tăng số lượng
                if (slots[i].itemSlot.stack.id == blockID)
                {
                    slots[i].itemSlot.Add((byte)blockID, amount);
                    return;
                }
            }
            else
            {
                // Nếu slot trống, thêm item mới vào đây
                slots[i].itemSlot.InsertStack(new ItemStack((byte)blockID, amount));
                return;
            }
        }
    }

}