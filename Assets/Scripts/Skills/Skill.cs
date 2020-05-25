using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public abstract bool Application(BattleCharacter whoUsing, BattlePlacement battlePlacement);
}
