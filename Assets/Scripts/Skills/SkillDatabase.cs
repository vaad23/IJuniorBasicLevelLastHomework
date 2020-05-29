using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillDatabase : ScriptableObject
{
    public abstract Skill Skill();
}

public abstract class Skill
{
    public abstract bool Application(BattleCharacter whoUsing, BattlePlacement battlePlacement);
}
