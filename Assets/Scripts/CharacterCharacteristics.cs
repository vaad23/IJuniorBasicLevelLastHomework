using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterData", menuName = "Enemy/Character Data", order = 51)]
public class CharacterCharacteristics : ScriptableObject
{
    [SerializeField] private CharacteristicsReadOnly _firstLevel;

    public CharacteristicsReadOnly FirstLevel => _firstLevel;
}
