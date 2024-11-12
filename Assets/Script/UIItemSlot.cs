using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{

    public bool isLinked = false;
    public ItemSlot itemSlot;
    public Image slotImage;
    public Image slotIcon;
    public Text slotAmount;

    World world;

    private void Awake()
    {

        world = GameObject.Find("World").GetComponent<World>();

    }

    public bool HasItem
    {

        get
        {

            if (itemSlot == null)
                return false;
            else
                return itemSlot.HasItem;

        }

    }

    public void Link(ItemSlot _itemSlot)
    {

        itemSlot = _itemSlot;
        isLinked = true;
        itemSlot.LinkUISlot(this);
        UpdateSlot();

    }

    public void UnLink()
    {

        itemSlot.unLinkUISlot();
        itemSlot = null;
        UpdateSlot();

    }

    public void UpdateSlot()
    {

        if (itemSlot != null && itemSlot.HasItem)
        {

            slotIcon.sprite = world.blocktypes[itemSlot.stack.id].icon;
            slotAmount.text = itemSlot.stack.amount.ToString();
            slotIcon.enabled = true;
            slotAmount.enabled = true;

        }
        else
            Clear();

    }

    public void Clear()
    {

        slotIcon.sprite = null;
        slotAmount.text = "";
        slotIcon.enabled = false;
        slotAmount.enabled = false;

    }

    private void OnDestroy()
    {

        if (itemSlot != null)
            itemSlot.unLinkUISlot();

    }

}

public class ItemSlot
{

    public ItemStack stack = null;
    private UIItemSlot uiItemSlot = null;

    public bool isCreative;

    public ItemSlot(UIItemSlot _uiItemSlot)
    {

        stack = null;
        uiItemSlot = _uiItemSlot;
        uiItemSlot.Link(this);

    }

    public ItemSlot(UIItemSlot _uiItemSlot, ItemStack _stack)
    {

        stack = _stack;
        uiItemSlot = _uiItemSlot;
        uiItemSlot.Link(this);

    }

    public void LinkUISlot(UIItemSlot uiSlot)
    {

        uiItemSlot = uiSlot;

    }

    public void unLinkUISlot()
    {

        uiItemSlot = null;

    }

    public void EmptySlot()
    {

        stack = null;
        if (uiItemSlot != null)
            uiItemSlot.UpdateSlot();

    }

    // Phương thức lấy block - giảm số lượng
    public int Take(int amt)
    {
        // nếu lấy nhiều hơn hoặc bằng số lượng hiện có
        if (amt > stack.amount)
        {
            int _amt = stack.amount;
            EmptySlot(); // xóa slot nếu lấy hết
            return _amt;
        }
        else if (amt < stack.amount)
        {
            stack.amount -= amt; // cập nhật số lượng
            uiItemSlot.UpdateSlot(); // cập nhật ui
            return amt;
        }
        else
        {
            EmptySlot();
            return amt;
        }

    }

    public void Add(byte blockID, int amount)
    {
        // Kiểm tra nếu stack hiện tại trống, thêm một block mới với số lượng
        if (stack == null)
        {
            stack = new ItemStack(blockID, amount);
        }
        else if (stack.id == blockID) // Nếu stack có cùng loại block
        {
            // Tăng số lượng block, nhưng không vượt quá giới hạn 999
            stack.amount = Mathf.Min(stack.amount + amount, 999);
        }
        else
        {
            Debug.Log("no such block");
        }

        // Cập nhật lại slot sau khi thêm block
        uiItemSlot.UpdateSlot();
    }


    public ItemStack TakeAll()
    {

        ItemStack handOver = new ItemStack(stack.id, stack.amount);
        EmptySlot();
        return handOver;

    }

    public void InsertStack(ItemStack _stack)
    {

        stack = _stack;
        uiItemSlot.UpdateSlot();

    }

    public bool HasItem
    {

        get
        {

            if (stack != null)
                return true;
            else
                return false;

        }

    }

}