﻿using UnityEngine;
using TennisGame.Actors;

namespace TennisGame.AI.Simple
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AdversaryComponent))]
    public class SimpleAIComponent: MonoBehaviour
    {
        private AdversaryComponent adversary;
        [SerializeField]
        private AIQuality aiQuality = AIQuality.High;
        private ISimpleAIQuality simpleAIQuality;

        private void Awake()
        {
            adversary = GetComponent<AdversaryComponent>();
            if (!adversary)
                throw new UnassignedReferenceException("AdversaryComponent doesn't set.");
            simpleAIQuality = aiQuality.GetSimpleAIQuality();
        }

        private void Update()
        {
            Think(simpleAIQuality.Deep, 0);
        }

        private void OnValidate()
        {
            simpleAIQuality = aiQuality.GetSimpleAIQuality();
        }

        private void Think(int deep, int current)
        {
            if (current >= adversary.GameController.Hits.Length)
                return;

            //Debug.Log(string.Format("{0}: Think deep {1}", gameObject.name, current));

            var hit = adversary.GameController.Hits[current];
            if (hit.collider)
            {
                if (adversary.GameController.IsItSelf(gameObject, hit.collider.gameObject))
                {
                    
                }
                else if (adversary.GameController.IsOppositeWall(gameObject, hit.collider.gameObject))
                {
                    
                }
                else if (current < deep)
                {
                    Think(deep, current + 1);
                }
                else
                {
                    StopMove();
                }
            }
            else
            {
                StopMove();
            }
        }

        private void MoveLeft()
        {
            adversary.SetHorizontalAxis(Mathf.Clamp(adversary.GetHorizontalAxis() - simpleAIQuality.MoveStep, -1f, 1f));
        }

        private void MoveRight()
        {
            adversary.SetHorizontalAxis(Mathf.Clamp(adversary.GetHorizontalAxis() + simpleAIQuality.MoveStep, -1f, 1f));
        }

        private void StopMove()
        {
            adversary.SetHorizontalAxis(0f);
        }
    }
}
