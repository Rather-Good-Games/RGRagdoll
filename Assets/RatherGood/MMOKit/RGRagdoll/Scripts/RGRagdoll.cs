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

        public bool debugTestSeleteMe = false;

        public bool isRagdoll = false;

        BaseCharacterEntity baseCharacterEntity;

        Collider baseCharCollider;

        Animator baseCharAnimator;

        public DamageableHitBox_RG[] damageableHitBoxes;

        [InspectorButton(nameof(ToggleRagdoll))]
        [SerializeField] bool toggleRagdoll = false;
        public void ToggleRagdoll()
        {
            SetRagdoll(!isRagdoll);
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

        }

        [InspectorButton(nameof(RemoveColliders))]
        [SerializeField] bool removeColliders = false;
        public void RemoveColliders()
        {
            foreach (var dhb in damageableHitBoxes)
            {
                dhb.gameObject.RemoveComponents<DamageableHitBox_RG>();
            }

            damageableHitBoxes = null;
        }

        private void Awake()
        {
            baseCharacterEntity = GetComponentInParent<BaseCharacterEntity>();

            baseCharCollider = baseCharacterEntity.GetComponent<Collider>();

            baseCharAnimator = baseCharacterEntity.GetComponent<Animator>();

            if (GameInstance.Singleton.enableRatherGoodRagdoll)
            {
                //baseCharCollider.enabled = false; //always 
                baseCharAnimator.enabled = true; //always 

            }

        }
        void Start()
        {
            if (GameInstance.Singleton.enableRatherGoodRagdoll)
            {
                //baseCharCollider.enabled = false;

                FindColliders();
            }
            else
            {
                //baseCharCollider.enabled = true;
            }

        }

        private void OnEnable()
        {
            baseCharacterEntity.onDead.AddListener(SetRagdollOn);
            baseCharacterEntity.onRespawn.AddListener(SetRagdollOff);
        }

        private void OnDisable()
        {
            baseCharacterEntity.onDead.AddListener(SetRagdollOn);
            baseCharacterEntity.onRespawn.AddListener(SetRagdollOff);
        }
        void Update()
        {

        }
        void SetRagdollOn()
        {
            SetRagdoll(true);
        }

        void SetRagdollOff()
        {
            SetRagdoll(false);
        }

        public void SetRagdoll(bool ragdoll)
        {
            isRagdoll = ragdoll;

            //disable main collider
            //baseCharCollider.enabled = false; //always false

            baseCharAnimator.enabled = !ragdoll; //always false

            foreach (DamageableHitBox_RG dhb in damageableHitBoxes)
            {
                Collider col = dhb.CacheCollider;
                if (col == null) col = dhb.GetComponent<Collider>();
                col.attachedRigidbody.isKinematic = !ragdoll;
                col.attachedRigidbody.useGravity = ragdoll;

                col.gameObject.layer = (ragdoll) ? GameInstance.Singleton.ragdollLayerMask : baseCharacterEntity.gameObject.layer;

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
    /// Label body part for combat text.
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