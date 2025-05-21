using UnityEngine;
using System.Collections.Generic;

public class BackGroundManager : MonoBehaviour
{
    public static BackGroundManager Instance;

    // Kéo các GameObject Map1_Easy, Map1_Normal, ... vào đây trong Inspector
    public List<GameObject> map1Variants;
    public List<GameObject> map2Variants;

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
                Transform day = map.transform.Find("BackGroundDay");
                Transform night = map.transform.Find("BackGroundNight");

                if (day != null) dayList.Add(day.gameObject);
                if (night != null) nightList.Add(night.gameObject);
            }
        }
    }

    public void ToggleBackgrounds()
    {
        ToggleList(map1Day);
        ToggleList(map1Night);
        ToggleList(map2Day);
        ToggleList(map2Night);
    }

    private void ToggleList(List<GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            if (obj != null) obj.SetActive(!obj.activeSelf);
        }
    }
}
