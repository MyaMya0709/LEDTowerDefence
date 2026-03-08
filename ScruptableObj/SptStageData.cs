using UnityEngine;

[CreateAssetMenu(fileName = "StoStageData", menuName = "Data/StoStageData")]
public class SptStageData : ScriptableObject
{
    public GameObject enemyPrf;
    public bool isBossStage;
    public int spawnCount;
    public int stageTime;
}
