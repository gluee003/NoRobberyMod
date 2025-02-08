using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static NoRobberyMod.BepinexPlugin;
using LBoL.Core.Units;
using LBoL.Core;
using LBoL.EntityLib.Cards.Enemy;

namespace NoRobberyMod
{
    public sealed class BribeEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BribeStatus);
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(LBoL.Core.Locale.En, "StatusEffectsEn.yaml");
            return loc;
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("BribeSE.png", BepinexPlugin.embeddedSource);
        }

        public override StatusEffectConfig MakeConfig()
        {
            {
                var statusEffectConfig = new StatusEffectConfig(
                                Id: "",
                                Index: 0,
                                Order: 10,
                                Type: StatusEffectType.Special,
                                IsVerbose: false,
                                IsStackable: true,
                                StackActionTriggerLevel: null,
                                HasLevel: true,
                                LevelStackType: StackType.Add,
                                HasDuration: false,
                                DurationStackType: StackType.Add,
                                DurationDecreaseTiming: DurationDecreaseTiming.Custom,
                                HasCount: false,
                                CountStackType: StackType.Keep,
                                LimitStackType: StackType.Keep,
                                ShowPlusByLimit: false,
                                Keywords: Keyword.None,
                                RelativeEffects: new List<string>() { },
                                VFX: "Default",
                                VFXloop: "Default",
                                SFX: "Default"
                    );
                return statusEffectConfig;
            }
        }
    }
    [EntityLogic(typeof(BribeEffect))]
    public sealed class BribeStatus : StatusEffect
    {
        public int Bribe
        {
            get
            {
                return Library.CreateCard<Bribery>().MoneyCost;
            }
        }
        protected override void OnAdded(Unit unit)
        {
            EnemyUnit enemyUnit = unit as EnemyUnit;
            base.HandleOwnerEvent<DieEventArgs>(enemyUnit.EnemyPointGenerating, new GameEventHandler<DieEventArgs>(this.OnDie));
        }
        private void OnDie(DieEventArgs args)
        {
            args.Money += base.Level;
        }
    }
}
