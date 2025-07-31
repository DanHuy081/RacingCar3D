using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MSSceneController : MonoBehaviour
{
    // Danh sách các xe trong scene mà controller sẽ quản lý
    [SerializeField]
    public List<MSVehicleControllerFree> vehicles = new List<MSVehicleControllerFree>();

    void Start()
    {
        // Nếu danh sách chưa có xe nào, tự động tìm và thêm vào
        if (vehicles.Count == 0)
        {
            vehicles.AddRange(FindObjectsOfType<MSVehicleControllerFree>());
            UnityEngine.Debug.Log("MSSceneController: Tự động thêm " + vehicles.Count + " xe vào danh sách quản lý.");
        }
    }

    void Update()
    {

    }
}
