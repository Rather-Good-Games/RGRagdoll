using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MultiplayerARPG.RGRagdoll;

namespace MultiplayerARPG
{

    [DisallowMultipleComponent]
    public class DamageableHitBox_RG : DamageableHitBox
    {

        public RagdollBodyPart ragdollBodyPart = RagdollBodyPart.Hips;

        RGRagdoll rgRagdoll;
        public void InitDamageableHitBox_RG(RGRagdoll rgRagdoll)
        {
            this.rgRagdoll = rgRagdoll;
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            if (Application.isPlaying && GameInstance.Singleton.debugRagdollCollidersInEditor)
                base.OnDrawGizmos();
        }
#endif

        public override void ReceiveDamage(Vector3 fromPosition, EntityInfo instigator, Dictionary<DamageElement, MinMaxFloat> damageAmounts, CharacterItem weapon, BaseSkill skill, short skillLevel, int randomSeed)
        {
            base.ReceiveDamage(fromPosition, instigator, damageAmounts, weapon, skill, skillLevel, randomSeed);

            if (GameInstance.Singleton.enableRatherGoodRagdoll)
                rgRagdoll?.SpawnBodyPartCombatText(transform, 0, ragdollBodyPart); //Only works client currently
        }

        public override void Setup(int index)
        {
            base.Setup(index);

            gameObject.layer = GameInstance.Singleton.ragdollLayerMask; //setup will override this so need to set it back.
        }


    }


}
