
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType { Star, Coin, SpeedBoost }

    public ItemType itemType;
    public int value = 1;
    public ParticleSystem collectEffect;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[CollectibleItem] Va chạm với: {other.name}");

        // Kiểm tra component CarController
        CarController car = other.GetComponent<CarController>();
        if (car == null)
        {
            Debug.LogWarning($"[CollectibleItem] Không tìm thấy CarController trong {other.name}");
            return;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Tăng điểm, hiệu ứng, boost, v.v.
                Destroy(gameObject);
            }
        }


        Debug.Log($"[CollectibleItem] Đã xác nhận xe hợp lệ, loại vật phẩm: {itemType}");

        // Hiệu ứng và âm thanh
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            Debug.Log("[CollectibleItem] Hiển thị hiệu ứng thu thập");
        }
        else
        {
            Debug.LogWarning("[CollectibleItem] Không có hiệu ứng được gán!");
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Debug.Log("[CollectibleItem] Phát âm thanh thu thập");
        }
        else
        {
            Debug.LogWarning("[CollectibleItem] Không có âm thanh được gán!");
        }

        // Xử lý theo loại vật phẩm
        switch (itemType)
        {
            case ItemType.Star:
                GameManager.Instance.AddStar(value);
                Debug.Log($"[CollectibleItem] Thêm {value} sao");
                break;

            case ItemType.Coin:
                GameManager.Instance.AddCoin(value);
                Debug.Log($"[CollectibleItem] Thêm {value} xu");
                break;

            case ItemType.SpeedBoost:
                car.StartCoroutine(car.SpeedBoost());
                Debug.Log("[CollectibleItem] Kích hoạt tăng tốc");
                break;

            default:
                Debug.LogWarning("[CollectibleItem] Vật phẩm không xác định!");
                break;
        }

        // Xóa vật phẩm sau khi thu thập
        Destroy(gameObject);
        Debug.Log("[CollectibleItem] Đã hủy vật phẩm khỏi scene");
    }
}
