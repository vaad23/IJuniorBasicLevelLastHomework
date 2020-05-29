using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleCharacter", menuName = "Database/New Battle Character", order = 51)]
public class DatabaseBattleCharacter : ScriptableObject
{
    [SerializeField] private SkillDatabase _normalSkill;
    [SerializeField] private SkillDatabase _specialSkill;

    public SkillDatabase NormalSkill => _normalSkill;
    public SkillDatabase SpecialSkill => _specialSkill;
}
