using UnityEngine;

public class UI_PrefabSpawner : MonoBehaviour
{
    [Header("UI için prefab")]
    public GameObject prefabToSpawn;

    [Header("Spawn noktalarý (RectTransform içermeli)")]
    public Transform[] spawnPoints;

    [Header("Ýstenilen boyut")]
    public Vector2 desiredSize = new Vector2(100, 100); // UI öðesinin boyutu

    public void TrySpawn()
    {
        foreach (Transform point in spawnPoints)
        {
            if (point.childCount == 0)
            {
                GameObject spawned = Instantiate(prefabToSpawn, point);
                RectTransform rt = spawned.GetComponent<RectTransform>();

                if (rt != null)
                {
                    // UI görünürlüðü ve boyutu için gerekli ayarlar
                    rt.anchoredPosition = Vector2.zero;
                    rt.localScale = Vector3.one;
                    rt.localRotation = Quaternion.identity;
                    rt.sizeDelta = desiredSize; // iþte burasý kritik: görünür büyüklük

                    Debug.Log($"Prefab spawn edildi: {point.name}");
                }
                else
                {
                    Debug.LogWarning("RectTransform bulunamadý. Prefab UI öðesi olmayabilir.");
                }

                return;
            }
        }

        Debug.Log("Tüm UI konumlarý dolu!");
    }
}
