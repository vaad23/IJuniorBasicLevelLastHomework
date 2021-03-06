﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Characteristics", menuName = "Database/New Characteristics", order = 51)]
public class DatabaseCharacterCharacteristics : ScriptableObject
{
    [SerializeField] private CharacteristicsReadOnly _firstLevel;

    public CharacteristicsReadOnly FirstLevel => _firstLevel;
}
