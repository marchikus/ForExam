using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Settings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Tooltip("Шанс появления золота на клетке (от 0 до 1)")]
    [Range(0f, 1f)]
    public float goldSpawnChance = 0.5f;

    [Tooltip("Максимальная глубина клеток")]
    public int maxDepth = 10;

    [Header("Cell Settings")]
    [Tooltip("Количество строк в гриде")]
    public int gridRows = 5;

    [Tooltip("Количество столбцов в гриде")]
    public int gridColumns = 5;
}
