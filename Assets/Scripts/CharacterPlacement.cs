using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacement 
{
    private BattleCharacter[,] _placements;

    public int LengthLine => _placements.GetLength(0);
    public int LengthColumn => _placements.GetLength(1);
    
    public CharacterPlacement(PlacementPosition maxPosition)
    {
        _placements = new BattleCharacter[maxPosition.Line, maxPosition.Column];
    }

    public CharacterPlacement(CharacterPlacement newPlacement)
    {
        _placements = new BattleCharacter[newPlacement.LengthLine, newPlacement.LengthColumn];

        for (int i = 0; i < newPlacement.LengthLine; i++)
            for (int j = 0; j < newPlacement.LengthColumn; j++)
                _placements[i, j] = newPlacement.ReturnCharacter(new PlacementPosition(i, j));
    }

    public BattleCharacter ReturnCharacter(PlacementPosition position)
    {
        if (PlaceNotExists(position))
            return null;

        return _placements[position.Line, position.Column];
    }

    private bool PlaceNotExists(PlacementPosition position)
    {
        return LengthLine < position.Line || LengthColumn < position.Column;
    }

    public bool TryAddCharacter(BattleCharacter character, PlacementPosition position)
    {
        if (PlaceNotExists(position))
            return false;

        _placements[position.Line, position.Column] = character;
        return true;
    }

    public bool TrySearch(out BattleCharacter character, PlacementPosition position)
    {
        character = null;
        if (PlaceNotExists(position))
            return false;

        if (_placements[position.Line,position.Column] == null)
            return false;

        character = _placements[position.Line, position.Column];
        return true;
    }
}

public class PlacementPosition
{
    private int _line;
    private int _column;

    public int Line => _line; 
    public int Column  => _column; 

    public PlacementPosition(int line, int column)
    {
        _line = line;
        _column = column;
    }
}
