using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MultiplayerARPG
{

    /// <summary>
    /// Place this script as child of BaseCharacterEntity.
    /// </summary>
    public class RGRagdoll : MonoBehaviour
    {

        public bool isRagdoll = false;

        [SerializeField] BaseCharacterEntity baseCharacterEntity;

        public DamageableHitBox_RG[] damageableHitBoxes;

        [InspectorButton(nameof(ToggleRagdoll))]
        [SerializeField] bool toggleRagdoll = false;
        public void ToggleRagdoll()
        {
            isRagdoll = !isRagdoll;
            SetRagdoll(isRagdoll);
        }

        [InspectorButton(nameof(FindColliders))]
        [SerializeField] bool findColliders = false;
        public void FindColliders()
        {
            BaseCharacterEntity bce = GetComponentInParent<BaseCharacterEntity>();

            Rigidbody[] rbs = bce.GetComponentsInChildren<Rigidbody>();

            List<DamageableHitBox_RG> dhbList = new List<DamageableHitBox_RG>();

            foreach (Rigidbody rb in rbs)
            {
                DamageableHitBox_RG dhbNew = rb.gameObject.GetOrAddComponent<DamageableHitBox_RG>();

                dhbList.Add(dhbNew);
                dhbNew.InitDamageableHitBox_RG(this);
            }

            damageableHitBoxes = dhbList.ToArray();
            dhbList.Clear();
        }

        private void Awake()
        {
            baseCharacterEntity = GetComponentInParent<BaseCharacterEntity>();

            if (GameInstance.Singleton.enableRatherGoodRagdoll)
            {
                baseCharacterEntity.GetComponent<Collider>().enabled = false; //always 
                baseCharacterEntity.GetComponent<Animator>().enabled = true; //always 
                baseCharacterEntity.onDead.AddListener(SetRagdollOn);
                baseCharacterEntity.onRespawn.AddListener(SetRagdollOff);
            }

        }
        void Start()
        {
            if (GameInstance.Singleton.enableRatherGoodRagdoll)
            {
                baseCharacterEntity.GetComponent<Collider>().enabled = false;

                FindColliders();
            }
            else
            {
                baseCharacterEntity.GetComponent<Collider>().enabled = true;
            }


        }
        private void OnDestroy()
        {
            baseCharacterEntity.onDead.RemoveListener(SetRagdollOn);
        }

        void Update()
        {

        }
        void SetRagdollOn()
        {
            isRagdoll = true;
            SetRagdoll(isRagdoll);
        }

        void SetRagdollOff()
        {
            isRagdoll = false;
            SetRagdoll(isRagdoll);
        }
        public void SetRagdoll(bool ragdoll)
        {
            //disable main collider
            baseCharacterEntity.GetComponent<Collider>().enabled = false; //always false

            baseCharacterEntity.GetComponent<Animator>().enabled = !ragdoll; //always false

            foreach (DamageableHitBox_RG dhb in damageableHitBoxes)
            {
                Collider col = dhb.CacheCollider;
                if (col == null) col = dhb.GetComponent<Collider>();
                col.attachedRigidbody.isKinematic = !ragdoll;
                col.attachedRigidbody.useGravity = ragdoll;

                if (ragdoll)
                {
                    col.gameObject.layer = GameInstance.Singleton.ragdollLayerMask;
                }
                else
                {
                    col.gameObject.layer = GameInstance.Singleton.playerLayer;
                }
            }
        }

        public void SpawnBodyPartCombatText(Transform followingTransform, int amount, RagdollBodyPart bodyPart)
        {
            if (BaseUISceneGameplay.Singleton.combatTextTransform == null || GameInstance.Singleton.prefabUICombatTextRG == null)
                return;

            UICombatTextRG combatText = Instantiate(GameInstance.Singleton.prefabUICombatTextRG, BaseUISceneGameplay.Singleton.combatTextTransform);
            combatText.transform.localScale = Vector3.one;
            combatText.CacheObjectFollower.TargetObject = followingTransform;
            combatText.setLeadText = bodyPart.ToString();

            combatText.Amount = amount;

            combatText.gameObject.SetActive(true);
        }



    }

    /// <summary>
    /// Label body part fro combat text.
    /// </summary>
    [System.Serializable]
    public enum RagdollBodyPart
    {
        Hips,
        Abdomen,
        Chest,
        Head,
        RightArm,
        LeftArm,
        RightHand,
        LeftHand,
        RightLeg,
        LeftLeg,
        RightFoot,
        LeftFoot,
        Tail,
        Prop,

    }

}