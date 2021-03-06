﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardState_MatchCheck : BoardStateBase
{
    //Callbacks for each tiles:
    //On-destroy and on-arrived-at-target-location 

    //2. when there is a match, wait for falling animation first

    private bool matchFound;
    //TO DO: Match object for match clusters, or maybe like a list of lists

    public BoardState_MatchCheck(Board board) : base(board) { }

    public override void StateEntry()
    {
        base.StateEntry();
        //Loop through all tiles to see if there are matches
        board.StartCoroutine(DoCheck());
        //If matches are found, then 
        //board.SwitchToState(BoardStates.Fall);
        //If no match are found, then
        //board.SwitchToState(BoardStates.Normal);
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
    }

    public override void StateExit()
    {

    }

    private IEnumerator DoCheck()
    {
        while (!AllTilesAreStill())
        {
            yield return null;
        }

        if (IsThereAMatch())
        {
            board.SwitchToState(BoardStates.Fall);
        }
        else
        {
            board.SwitchToState(BoardStates.Normal);
        }
    }

    private bool AllTilesAreStill()
    {
        foreach (var tile in board.Status.tiles)
        {
            if (tile.IsMoving)
                return false;
        }
        return true;
    }

    private bool IsThereAMatch ()
    {
        //Destroy the tiles
        return false;
    }

    private bool HasTileHere(Vector2Int index) => status.tiles[index.x, index.y] != null;
    private void VacateSlot(Vector2Int index) => status.tiles[index.x, index.y] = null;
    private bool IsTileLocatedAtTopEdge(Vector2Int index) => index.y < status.tiles.GetLength(1);
    private bool IsTileInBound(Vector2Int index) => index.x < status.tiles.GetLength(0) && index.y < status.tiles.GetLength(1);
}