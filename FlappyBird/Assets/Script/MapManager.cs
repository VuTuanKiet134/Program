using UnityEngine;
using System.Collections.Generic;

public enum MapID { Map1, Map2, Map3, Map4 }
public enum Difficulty { Easy, Normal, Hard, Insane }

[System.Serializable]
public class MapTransition
{
    public MapID fromMap;
    public Difficulty fromDifficulty;
    public string triggerPrefabName;
    public MapID toMap;
}

public class MapManager : MonoBehaviour
{
    [System.Serializable]
    public class MapEntry
    {
        public MapID mapID;
        public Difficulty difficulty;
        public GameObject mapObject;
    }

    public List<MapEntry> mapEntries;
    public List<MapTransition> transitions;

    private Dictionary<(MapID, Difficulty), GameObject> mapLookup;
    private Dictionary<MapID, Difficulty> currentDifficulties;
    private MapID currentMap;

    void Start()
    {
        // Khởi tạo mapLookup
        mapLookup = new Dictionary<(MapID, Difficulty), GameObject>();
        foreach (var entry in mapEntries)
        {
            mapLookup[(entry.mapID, entry.difficulty)] = entry.mapObject;
            entry.mapObject.SetActive(false);
        }

        // Khởi tạo độ khó ban đầu
        currentDifficulties = new Dictionary<MapID, Difficulty>();
        foreach (MapID mapID in System.Enum.GetValues(typeof(MapID)))
        {
            currentDifficulties[mapID] = Difficulty.Easy;
        }

        // Bắt đầu ở Map1_Easy
        currentMap = MapID.Map1;
        mapLookup[(MapID.Map1, Difficulty.Easy)].SetActive(true);
    }

    public void HandlePrefabTrigger(GameObject triggeredPrefab)
{
    string triggeredName = triggeredPrefab.name.Replace("(Clone)", "").Trim();
    Debug.Log($"🎯 Triggered Name: {triggeredName}");

    MapID oldMap = currentMap;
    Difficulty oldDifficulty = currentDifficulties[oldMap];

    foreach (MapTransition t in transitions)
    {
        if (t.fromMap == oldMap &&
            oldDifficulty == t.fromDifficulty &&
            t.triggerPrefabName == triggeredName)
        {
            // Tắt map hiện tại theo độ khó hiện tại
            mapLookup[(oldMap, oldDifficulty)].SetActive(false);

            // Tăng độ khó cho map cũ nếu chưa phải max
            if (currentDifficulties[oldMap] < Difficulty.Insane)
            {
                currentDifficulties[oldMap]++;
                Debug.Log($"⬆️ Tăng độ khó của {oldMap} lên {currentDifficulties[oldMap]}");
            }
            else
            {
                Debug.Log($"🔒 Độ khó của {oldMap} đã đạt max: {currentDifficulties[oldMap]}");
            }

            // Xoá trigger, enemy, noBomb như bạn đã có
            GameObject[] oldTriggers = GameObject.FindGameObjectsWithTag("PassMap");
            foreach (GameObject trigger in oldTriggers)
            {
                Destroy(trigger);
            }

            GameObject[] oldpipe = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (GameObject pipe in oldpipe)
            {
                Destroy(pipe);
            }

            GameObject[] noBombs = GameObject.FindGameObjectsWithTag("NoBomb");
            foreach (GameObject bomb in noBombs)
            {
                Destroy(bomb);
            }

            GameObject[] bullet = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bul in bullet)
            {
                Destroy(bul);
            }

            GameObject[] las = GameObject.FindGameObjectsWithTag("Laser");
            foreach (GameObject laser in las)
            {
                Destroy(laser);
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            // Chuyển sang map mới
            currentMap = t.toMap;

            // Reset trạng thái game
            GameManager.Instance.ResetGameState();

            // Lấy độ khó hiện tại của map mới (đã lưu trong currentDifficulties)
            Difficulty newDifficulty = currentDifficulties[currentMap];

            // Bật map mới với độ khó hiện tại
            GameObject newMap = mapLookup[(currentMap, newDifficulty)];
            newMap.SetActive(true);

            Debug.Log($"➡️ Chuyển đến {currentMap} - {newDifficulty}");

            // Cập nhật camera
            Invoke(nameof(NotifyCameraToRefresh), 0.1f);

            return;
        }
    }

    Debug.LogWarning("⚠️ Không tìm thấy chuyển map phù hợp với prefab này!");
}




    // Gửi tín hiệu cho camera tự tìm PlayerCam mới
    void NotifyCameraToRefresh()
    {
        FollowCamera cameraScript = Camera.main?.GetComponent<FollowCamera>();
        if (cameraScript != null)
        {
            cameraScript.SendMessage("FindActiveTarget");
        }
        else
        {
            Debug.LogWarning("📷 Không tìm thấy FollowCamera trên Camera chính!");
        }
    }
}
