using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/New Database", order = 51)]
public class Database : ScriptableObject
{
    [SerializeField] private List<DatabaseCharacter> _characters;

    public List<DatabaseCharacter> Characters => _characters;

    public DatabaseCharacter GetDatabaseCharacter(string id)
    {
        foreach (var character in _characters)
        {
            if (character.Id == id)
                return character;
        }

        return null;
    }
}
