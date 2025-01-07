using UnityEngine;

public class GoldSpawn : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GameObject goldPrefab;

    public void SpawnGold(Vector3 position)
    {
        if (gameSettings != null)
        {
            float spawnChance = gameSettings.goldSpawnChance;
            if (Random.Range(0f, 1f) <= spawnChance)
            {
                GameObject goldObject = Instantiate(goldPrefab, position, Quaternion.identity, transform);
                ClickableCell clickableCell = GetComponentInParent<ClickableCell>();

                if (clickableCell != null)
                {
                    clickableCell.SetGolded(true);
                }
            }
        }
    }
}
