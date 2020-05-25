using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Database/New Character", order = 51)]
public class DatabaseCharacter : ScriptableObject
{
    [SerializeField] private string _id = "000000";
    [SerializeField] private DatabaseBattleCharacter _battleCharacter;
    [SerializeField] private DatabaseCharacterCharacteristics _characteristics;
    [SerializeField] private DatabaseCharacterDescription _description;

    public string Id => _id;
    public DatabaseBattleCharacter BattleCharacter => _battleCharacter;
    public DatabaseCharacterCharacteristics Characteristics => _characteristics;
    public DatabaseCharacterDescription Description => _description;
}
