using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Enemy;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.Presentation;
using LBoLEntitySideloader.CustomHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace NoRobberyMod
{
    internal static class Handlers
    {
        public static void RegisterHandlers()
        {
            CHandlerManager.RegisterBattleEventHandler(battle => battle.CardUsed, OnCardUsed, null, GameEventPriority.ConfigDefault);
        }
        private static void OnCardUsed(CardUsingEventArgs args)
        {
            Debug.Log("hi1");
            if (args.Card is Bribery briberyCard)
            {
                BattleController battle = briberyCard.Battle;   
                List<EnemyUnit> list = battle.EnemyGroup.Where((EnemyUnit u) => u is Long && u.IsAlive).ToList<EnemyUnit>();
                if (list.Count > 1)
                {
                    Debug.LogWarning("Multiple Long exists");
                }
                else if (list.Count == 0)
                {
                    Debug.LogWarning("Bribery is used while no Long");
                }
                else
                {
                    Debug.Log("hi2");
                    EnemyUnit longUnit = list.First<EnemyUnit>();
                    battle.React(new ApplyStatusEffectAction<BribeStatus>(longUnit, briberyCard.MoneyCost), longUnit, ActionCause.CardUse);
                }
            }
        }
    }
}
