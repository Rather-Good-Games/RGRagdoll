using UnityEngine;
using LiteNetLibManager;
using LiteNetLib;

//Credit: Mod of Denarii Games CombatText
//Replaces previous Ragdoll client side only text display

namespace MultiplayerARPG
{
    public partial class DamageableEntity
    {

        public void CallAllAppendCombatTextStringRG(string bodyPart)
        {
            RPC(AllAppendCombatTextStringRG, 0, DeliveryMethod.Unreliable, bodyPart);
        }

        /// <summary>
        /// This will be called on clients to display generic combat texts
        /// </summary>
        /// <param name="text"></param>
        [AllRpc]
        protected void AllAppendCombatTextStringRG(string text)
        {
            if (!IsClient || CurrentGameInstance.prefabUICombatTextRG == null) return;

            UICombatTextRG combatText = Instantiate(CurrentGameInstance.prefabUICombatTextRG, BaseUISceneGameplay.Singleton.combatTextTransform);
            combatText.transform.localScale = Vector3.one;
            combatText.CacheObjectFollower.TargetObject = this.CombatTextTransform;
            combatText.Text = text;
            combatText.gameObject.SetActive(true);
        }

    }
}