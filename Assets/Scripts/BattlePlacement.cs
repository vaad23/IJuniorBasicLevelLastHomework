using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlacement 
{
    private CharacterPlacement _teamA;
    private CharacterPlacement _teamB;
    private Dictionary<BattleCharacter, PlacementPosition> _allCharacterTeamA;
    private Dictionary<BattleCharacter, PlacementPosition> _allCharacterTeamB;

    public TargetSearch Search;

    public BattlePlacement(List<BattleCharacterPlacement> teamA, List<BattleCharacterPlacement> teamB)
    {
        _teamA = new CharacterPlacement(new PlacementPosition(4, 3));
        _teamB = new CharacterPlacement(new PlacementPosition(4, 3));
        _allCharacterTeamA = new Dictionary<BattleCharacter, PlacementPosition>();
        _allCharacterTeamB = new Dictionary<BattleCharacter, PlacementPosition>();
        Search = new TargetSearch(this);

        foreach (var member in teamA)
        {
            _allCharacterTeamA.Add(member.Character, member.Position);
            _teamA.TryAddCharacter(member.Character, member.Position);
        }

        foreach (var member in teamB)
        {
            _allCharacterTeamB.Add(member.Character, member.Position);
            _teamB.TryAddCharacter(member.Character, member.Position);
        }
    }


    public class TargetSearch
    {
        private BattlePlacement _parent;

        public TargetSearch(BattlePlacement placement)
        {
            _parent = placement;
        }

        public BattleCharacter FirstEnemyPositionOnMyLine(BattleCharacter character)
        {
            foreach (var key in _parent._allCharacterTeamA.Keys)            
                if (key == character)
                    return FirstPosition(_parent._allCharacterTeamA[key], _parent._teamB);

            foreach (var key in _parent._allCharacterTeamB.Keys)
                if (key == character)
                    return FirstPosition(_parent._allCharacterTeamB[key], _parent._teamA);

            return null;
        }

        public List<BattleCharacter> AllEnemyLivingTeam(BattleCharacter character)
        {
            foreach (var key in _parent._allCharacterTeamA.Keys)
                if (key == character)
                    return AllLivingTeam(_parent._allCharacterTeamB);

            foreach (var key in _parent._allCharacterTeamB.Keys)
                if (key == character)
                    return AllLivingTeam(_parent._allCharacterTeamA);

            return new List<BattleCharacter>();
        }


        public List<BattleCharacter> AllTeam()
        {
            List<BattleCharacter> characters = new List<BattleCharacter>();

            foreach (var member in _parent._allCharacterTeamA.Keys)
                if (member.IsAlive)
                    characters.Add(member);

            foreach (var member in _parent._allCharacterTeamB.Keys)
                if (member.IsAlive)
                    characters.Add(member);

            return characters;            
        }

        private BattleCharacter FirstPosition(PlacementPosition characterPosition, CharacterPlacement placement)
        {
            int line = FirstFoundNotEmptyLine(characterPosition.Line, placement);

            if (line >= 0)
                for (int j = 0; j < placement.LengthColumn; j++)
                    if (placement.TrySearch(out BattleCharacter character, new PlacementPosition(line, j)) && character.IsAlive)
                        return character;

            return null;
        }

        private int FirstFoundNotEmptyLine(int line, CharacterPlacement placement)
        {
            for (int i = line; i < placement.LengthLine; i++)
                for (int j = 0; j < placement.LengthColumn; j++)
                    if (placement.TrySearch(out BattleCharacter character,new PlacementPosition(i,j)) && character.IsAlive)
                        return i;

            for (int i = line-1; i >= 0; i--)
                for (int j = 0; j < placement.LengthColumn; j++)
                    if (placement.TrySearch(out BattleCharacter character, new PlacementPosition(i, j)) && character.IsAlive)
                        return i;

            return -1;
        }

        private List<BattleCharacter> AllLivingTeam(Dictionary<BattleCharacter, PlacementPosition> team)
        {
            List<BattleCharacter> characters = new List<BattleCharacter>();

            foreach (var member in team.Keys)            
                if (member.IsAlive)
                    characters.Add(member);            

            return characters;
        }

        /* public string ToString(CharacterPlacement placement)
         {
             string allString = "";

             for (int i = 0; i < placement.LengthLine; i++)
             {
                 for (int j = 0; j < placement.LengthColumn; j++)
                 {
                     allString += placement.ReturnCharacter(i, j) + " ";
                 }
             }
             return allString;
         }*/
    }
}

public class BattleCharacterPlacement
{
    private BattleCharacter _character;
    private PlacementPosition _position;

    public BattleCharacter Character => _character;
    public PlacementPosition Position => _position;

    public BattleCharacterPlacement(BattleCharacter character, PlacementPosition position)
    {
        _character = character;
        _position = position;
    }
}

