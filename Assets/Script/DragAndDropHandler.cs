using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.UI;  
using UnityEngine.EventSystems;  

public class DragAndDropHandler : MonoBehaviour  
{  
    // Tham chiếu đến slot trên con trỏ chuột
    [SerializeField] private UIItemSlot cursorSlot = null;  
    private ItemSlot cursorItemSlot;  // Slot của vật phẩm hiện đang được giữ bởi con trỏ

    // Tham chiếu đến GraphicRaycaster để kiểm tra tương tác với UI
    [SerializeField] private GraphicRaycaster m_Raycaster = null;  
    private PointerEventData m_PointerEventData;  // Dữ liệu con trỏ chuột khi nhấp
    [SerializeField] private EventSystem m_EventSystem = null;  // Hệ thống quản lý sự kiện

    // Tham chiếu đến đối tượng World trong game
    World world;  

    // Hàm Start được gọi khi script bắt đầu
    private void Start()  
    {  
        world = GameObject.Find("World").GetComponent<World>();  // Lấy tham chiếu đến đối tượng World

        cursorItemSlot = new ItemSlot(cursorSlot);  // Tạo slot cho con trỏ
    }  

    // Hàm Update được gọi mỗi khung hình
    private void Update()  
    {  
        // Nếu người chơi không đang ở UI, không xử lý drag-and-drop
        if (!world.inUI)  
            return;  

        // Di chuyển vị trí của cursorSlot theo vị trí con trỏ chuột
        cursorSlot.transform.position = Input.mousePosition;  

        // Kiểm tra nếu người chơi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))  
        {  
            HandleSlotClick(CheckForSlot());  // Xử lý nhấp chuột lên slot
        }  
    }  

    // Hàm xử lý khi người chơi nhấp chuột vào slot
    private void HandleSlotClick(UIItemSlot clickedSlot)  
    {  
        // Nếu không nhấp vào slot nào, return
        if (clickedSlot == null)  
            return;  

        // Nếu cả hai slot đều không có vật phẩm, return
        if (!cursorSlot.HasItem && !clickedSlot.HasItem)  
            return;  

        // Nếu slot được nhấp vào là Creative Inventory, chuyển stack vào con trỏ
        if (clickedSlot.itemSlot.isCreative)  
        {  
            cursorItemSlot.EmptySlot();  // Làm trống slot con trỏ
            cursorItemSlot.InsertStack(clickedSlot.itemSlot.stack);  // Chuyển stack vào con trỏ
        }  

        // Nếu con trỏ không có vật phẩm nhưng slot được nhấp có, chuyển stack vào con trỏ
        if (!cursorSlot.HasItem && clickedSlot.HasItem)  
        {  
            cursorItemSlot.InsertStack(clickedSlot.itemSlot.TakeAll());  
            return;  
        }  

        // Nếu con trỏ có vật phẩm nhưng slot được nhấp không có, chuyển stack từ con trỏ vào slot
        if (cursorSlot.HasItem && !clickedSlot.HasItem)  
        {  
            clickedSlot.itemSlot.InsertStack(cursorItemSlot.TakeAll());  
            return;  
        }  

        // Nếu cả con trỏ và slot đều có vật phẩm, hoán đổi stack giữa hai slot
        if (cursorSlot.HasItem && clickedSlot.HasItem)  
        {  
            if (cursorSlot.itemSlot.stack.id != clickedSlot.itemSlot.stack.id)  
            {  
                ItemStack oldCursorSlot = cursorSlot.itemSlot.TakeAll();  // Lưu stack của con trỏ
                ItemStack oldSlot = clickedSlot.itemSlot.TakeAll();  // Lưu stack của slot được nhấp

                clickedSlot.itemSlot.InsertStack(oldCursorSlot);  // Hoán đổi stack
                cursorSlot.itemSlot.InsertStack(oldSlot);  
            }  
        }  
    }  

    // Kiểm tra xem con trỏ có đang trỏ vào slot nào không
    private UIItemSlot CheckForSlot()  
    {  
        m_PointerEventData = new PointerEventData(m_EventSystem);  // Khởi tạo dữ liệu sự kiện chuột
        m_PointerEventData.position = Input.mousePosition;  // Lấy vị trí chuột

        List<RaycastResult> results = new List<RaycastResult>();  // Danh sách kết quả raycast
        m_Raycaster.Raycast(m_PointerEventData, results);  // Thực hiện raycast

        // Lặp qua các kết quả và trả về slot nếu có
        foreach (RaycastResult result in results)  
        {  
            if (result.gameObject.tag == "UIItemSlot")  
                return result.gameObject.GetComponent<UIItemSlot>();  
        }  

        return null;  // Trả về null nếu không có slot
    }  
}  
