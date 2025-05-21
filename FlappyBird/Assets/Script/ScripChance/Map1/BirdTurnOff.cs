using UnityEngine;
using System.Collections.Generic;

public class BirdTurnOff : MonoBehaviour
{
    public static BirdTurnOff Instance;

    // Kéo các GameObject Map1_Easy, Map1_Normal, ... vào đây trong Inspector
    public List<GameObject> map1Variants;
    public List<GameObject> map2Variants;

    [Header("Tên GameObject con cần xử lý")]
    public string dayObjectName = "BackGroundDay";
    public string nightObjectName = "BackGroundNight";

    private List<GameObject> map1Day = new List<GameObject>();
    private List<GameObject> map1Night = new List<GameObject>();
    private List<GameObject> map2Day = new List<GameObject>();
    private List<GameObject> map2Night = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CollectBackgrounds(map1Variants, map1Day, map1Night);
        CollectBackgrounds(map2Variants, map2Day, map2Night);
    }

    private void CollectBackgrounds(List<GameObject> mapList, List<GameObject> dayList, List<GameObject> nightList)
    {
        foreach (GameObject map in mapList)
        {
            if (map != null)
            {
                Transform day = map.transform.Find(dayObjectName);
                Transform night = map.transform.Find(nightObjectName);

                if (day != null) dayList.Add(day.gameObject);
                if (night != null) nightList.Add(night.gameObject);
            }
        }
    }

    public void ApplyBackgroundSetting()
    {
        SetListState(map1Day, true);
        SetListState(map1Night, false);
        SetListState(map2Day, true);
        SetListState(map2Night, false);
    }

    private void SetListState(List<GameObject> objs, bool state)
    {
        foreach (GameObject obj in objs)
        {
            if (obj != null)
                obj.SetActive(state);
        }
    }
}
