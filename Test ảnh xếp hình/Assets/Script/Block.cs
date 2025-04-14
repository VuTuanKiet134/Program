using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector2Int[] cellOffsets;

    void Awake()
    {
        int count = transform.childCount;
        cellOffsets = new Vector2Int[count];

        for (int i = 0; i < count; i++)
        {
            Transform cell = transform.GetChild(i);
            Vector2 localPos = cell.localPosition;

            int x = Mathf.RoundToInt(localPos.x / 100f);  // giả sử cell rộng 100
            int y = Mathf.RoundToInt(localPos.y / 100f);

            cellOffsets[i] = new Vector2Int(x, y);
        }
    }
}
