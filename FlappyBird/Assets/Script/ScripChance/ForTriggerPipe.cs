using UnityEngine;
using System.Collections.Generic;

public class ForTriggerPipe : MonoBehaviour
{
    [Header("Tên các GameObject cần TẮT")]
    public List<string> toDisableNames;

    [Header("Tên các GameObject cần BẬT")]
    public List<string> toEnableNames;

    private List<GameObject> toDisable = new List<GameObject>();
    private List<GameObject> toEnable = new List<GameObject>();

    void Start()
    {
        // Tìm các GameObject trong Scene theo tên
        foreach (string name in toDisableNames)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
                toDisable.Add(obj);
        }

        foreach (string name in toEnableNames)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
                toEnable.Add(obj);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCam"))
        {
            foreach (GameObject obj in toDisable)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            foreach (GameObject obj in toEnable)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
