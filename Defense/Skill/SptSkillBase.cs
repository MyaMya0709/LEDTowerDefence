using UnityEditor;
using UnityEngine;

public abstract class SptSkillBase : MonoBehaviour
{
    public Sprite skillIcon;
    public string skillName;
    public string skillDescription;

    public float skillDamage;
    public ETowerType skillType;
    public float skillRange;

    public abstract void OnActive(ETowerType type, float damage, float range);
}
