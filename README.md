# RGRagdoll


**Author:** RatherGood1

**Version**: 0.3

This is a working demo. Not tested in MMO mode. Check back often for further change/improvements.

**Updated:** 20 Feb 22

V0.3 
* fixes layers player/monster. 
* Fixes raycast atatcks (melee/guns etc...) resetting entity transform when attacking ragdoll.  
* Adds player ragdoll example. (Use Male_CC_RGRG) in place of Male_CC

V0.2 few fixes. Changes to RGRagdoll script

**Compatibility:** Tested on Suriyun MMORPG Kit Version 1.73c and Unity 2021.1.23f1

**Core MMORPG Kit modifications:** None

**Description:** Monster entities ragdoll on death, or others probably.

**Demo Video:**

[![RGRagdoll](media/RGRagdollPic.png)](https://youtu.be/4H9hedYt1x8)

**Other Dependencies:**

None. Uses standard unity ragdoll creator.

**QUICK START:**

See example "BigOrcWarrior RGRD"

Use standard Unity ragdoll creator "GameObject > 3D Object > Ragdoll"

Add child gameObject and RGRagdoll script.

![RGRagdoll](media/RagdollSetup.png)

Click "Find Colliders" button that will add DamageableHitBox_RG.cs script to each joing rigidBody.  Set the body part enum manually if you want combat text to display the body part text hit in combat.

See the GameInstance for other settings.  Create and assign a seperate ragdoll layer for the ragdoll body parts seperate from the characterLayer.

![RGRagdoll](media/GameInstanceRGRagdoll.png)





**Done.**


[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=L7RYB7NRR78L6)