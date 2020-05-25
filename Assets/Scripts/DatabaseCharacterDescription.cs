using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Description", menuName = "Database/New Character Description", order = 51)]
public class DatabaseCharacterDescription : ScriptableObject
{
    [SerializeField] private string _characterName = "New name";
    [SerializeField] private string _description = "New description";

    public string CharacterName => _characterName;
    public string Description => _description;
}
