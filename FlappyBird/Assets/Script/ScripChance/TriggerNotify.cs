using UnityEngine;

public class TriggerNotify : MonoBehaviour
{
    public GameObject rootPrefab; // Gán GameObject cha của triggerPrefab
    public string playerTag = "Player"; // Gán tag cho nhân vật
    void Awake()
{
    if (rootPrefab == null)
    {
        
        rootPrefab = transform.root.gameObject;
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if  (other.CompareTag(playerTag))
        {
            Debug.Log("✅ Player đã va chạm với trigger");

            if (rootPrefab == null)
            {
                Debug.LogError("❌ rootPrefab chưa được gán!");
                return;
            }

            MapManager manager = FindObjectOfType<MapManager>();
            if (manager != null)
            {
                manager.HandlePrefabTrigger(rootPrefab);
            }
        }
    }

}
