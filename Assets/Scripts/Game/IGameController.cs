﻿using UnityEngine;

namespace TennisGame.Game
{
    public interface IGameController
    {
        RaycastHit2D[] Hits { get; }
        bool IsItSelf(GameObject obj, GameObject target);
        bool IsSelfWall(GameObject obj, GameObject target);
        bool IsOppositeWall(GameObject obj, GameObject target);
        float GetOppositeAdversaryPositionX(GameObject obj);
        void OnCollisionDetected(GameObject obj);
    }
}
