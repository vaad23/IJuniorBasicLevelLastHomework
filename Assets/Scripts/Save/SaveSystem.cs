using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveBattleSystem
{
    private SaveBattleTeams _battleTeams;
    private SaveBattleTeams _defaultTeams;

    public SaveBattleTeams BattleTeams => _battleTeams; 

    public SaveBattleSystem()
    {
        _defaultTeams = new SaveBattleTeams(
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 0),
                        new SaveFightCharacter("000002", 1, 1, 1, 1),
                        new SaveFightCharacter("000003", 1, 1, 2, 2),
                    }
                ),
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 0),
                        new SaveFightCharacter("000002", 1, 1, 1, 1),
                        new SaveFightCharacter("000003", 1, 1, 2, 2),
                    }
                ));

        LoadingBattleTeams();
    }

    private void LoadingBattleTeams()
    {
        //#if UNITY_EDITOR
        string pathParameters = Application.dataPath + "/BattleTeams.json";
        if (File.Exists(pathParameters))
        {
            string json = File.ReadAllText(pathParameters);
            _battleTeams = JsonUtility.FromJson<SaveBattleTeams>(json);
        }
        else
        {
            _battleTeams = _defaultTeams;
        }
        //#endif
    }

    public void SavingBattleTeams(SaveBattleTeams battleTeams)
    {
        string pathParameters = Application.dataPath + "/BattleTeams.json";
        File.WriteAllText(pathParameters, JsonUtility.ToJson(battleTeams));
        Debug.Log(JsonUtility.ToJson(battleTeams));
    }
}

[Serializable]
public class SaveBattleTeams
{
    [SerializeField] private SaveFightTeam _fightTeamA;
    [SerializeField] private SaveFightTeam _fightTeamB;

    public SaveFightTeam FightTeamA => _fightTeamA;
    public SaveFightTeam FightTeamB => _fightTeamB;

    public SaveBattleTeams(SaveFightTeam fightTeamA, SaveFightTeam fightTeamB)
    {
        _fightTeamA = fightTeamA;
        _fightTeamB = fightTeamB;
    }
}

[Serializable]
public class SaveFightTeam
{
    [SerializeField] private List<SaveFightCharacter> _characters;

    public List<SaveFightCharacter> Characters  => _characters; 

    public SaveFightTeam(List<SaveFightCharacter> characters)
    {
        _characters = characters;
    }
}

[Serializable]
public class SaveFightCharacter
{
    [SerializeField] private string _id;
    [SerializeField] private int _level;
    [SerializeField] private int _linePosition;
    [SerializeField] private int _columnPosition;
    [SerializeField] private int _placeInQueue;

    public string Id => _id;
    public int Level => _level;
    public int LinePosition => _linePosition;
    public int ColumnPosition => _columnPosition;
    public int PlaceInQueue => _placeInQueue;

    public SaveFightCharacter(string id, int level, int linePosition, int columnPosition, int placeInQueue)
    {
        _id = id;
        _level = level;
        _linePosition = linePosition;
        _columnPosition = columnPosition;
        _placeInQueue = placeInQueue;
    }
}

/*
{
private SaveCharacters _characters;

public SaveCharacters Characters => _characters;

public SaveSystem()
{
    LoadCharacters();
}

private void LoadCharacters()
{
    //#if UNITY_EDITOR
    string pathParameters = Application.dataPath + "/saveHeroes.json";
  /*  if (File.Exists(pathParameters))
    {
        string json = File.ReadAllText(pathParameters);
        _saveHeroes = JsonUtility.FromJson<Save>(json);
    }
    else/
    {
        _characters = new SaveCharacters(new List<SaveLevelCharacter>
                                        {
                                            new SaveLevelCharacter("000001", 1),
                                            new SaveLevelCharacter("000002", 1),
                                            new SaveLevelCharacter("000003", 1)
                                        });

        File.WriteAllText(pathParameters, JsonUtility.ToJson(_characters));
        Debug.Log(JsonUtility.ToJson(_characters));
    }
    //#endif
}
}*/
/*
[Serializable]
public class SaveCharacters
{
    [SerializeField] private List<SaveLevelCharacter> _characters;

    public List<SaveLevelCharacter> Characters => _characters; 

    public SaveCharacters(List<SaveLevelCharacter> characters)
    {
        _characters = new List<SaveLevelCharacter>();

        foreach (var character in characters)
        {
            _characters.Add(character);
        }
    }

    public void ChangeLevel(string id, int level)
    {
        foreach (var character in _characters)
            if (character.Id == id)
                character.ChangeLevel(level);
    }

    public void AddCharacter(SaveLevelCharacter character)
    {
        foreach (var member in _characters)
            if (character.Id == character.Id)
                return;

        _characters.Add(character);
    }
}

[Serializable]
public class SaveLevelCharacter
{
    [SerializeField] private string _id;
    [SerializeField] private int _level;

    public string Id  => _id;
    public int Level  => _level; 

    public SaveLevelCharacter(string id, int level)
    {
        _id = id;
        _level = level;
    }

    public void ChangeLevel(int level)
    {
        _level = level;
    }
}*/

/*
[Serializable]
public class SaveCharacters
{
    private List<SaveCharacter> _characters;

    public List<SaveCharacter> Characters => _characters;

    public SaveCharacters()
    {
        _characters = new List<SaveCharacter>();
    }

    public SaveCharacters(List<SaveCharacter> characters)
    {
        _characters = new List<SaveCharacter>();

        foreach (var character in characters)
        {
            _characters.Add(new SaveCharacter(character));
        }
    }

    public void AddCharacter(SaveCharacter save)
    {
        foreach (var character in _characters)
        {
            if (character.Id == save.Id)
                return;
        }

        _characters.Add(save);
    }
}*/
