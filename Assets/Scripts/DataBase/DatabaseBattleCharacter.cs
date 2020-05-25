using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleCharacter", menuName = "Database/New Battle Character", order = 51)]
public class DatabaseBattleCharacter : ScriptableObject
{
    [SerializeField] private Skill _normalSkill;
    [SerializeField] private Skill _specialSkill;

    public Skill NormalSkill => _normalSkill;
    public Skill SpecialSkill => _specialSkill;
}
