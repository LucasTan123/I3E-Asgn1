# I3E-Asgn1
I3E assignemnt 1 on creating a level

# Controls
. Movement: WASD
. Jump: Space bar
. Interact: E

# Platform
PC

# Premise
You’re the player and you’re out of money. You need to pay your rent. Explore an ancient ruin filled with lava and obstacles. Along the way, find hidden secrets and treasures to earn points and pay off what you owe.

# Game Outline
The player starts at the bottom of the level. Moving blocks appear and disappear on the ground, so you need to move quickly to get to the other side. Next, you’ll jump on obstacles that move up and down vertically. After that, you open a door leading to the main room. In the main room, you must climb to the top while collecting all the coins and gems, avoiding the lava that covers much of the map.

# Bugs and Limitations.
. Player must be precisely aligned with the door to interact; otherwise, the interaction fails or feels unresponsive.

. Respawn system occasionally does not reset the player’s position correctly, causing the player to remain stuck or not return to the intended respawn point.

# Credits and References
Unity Tutorials:

Introduction to Scripting in Unity
https://learn.unity.com/tutorial/introduction-to-c-scripting

Unity Input System (GetKeyDown usage)
https://learn.unity.com/tutorial/input

Instantiating Objects (Spawning Projectiles)
https://learn.unity.com/tutorial/instantiating-prefabs

Working with Transforms and Positioning
https://learn.unity.com/tutorial/transform

Physics and Rigidbody Basics (AddForce usage)
https://learn.unity.com/tutorial/rigidbody

Using Coroutines for Timed Actions
https://learn.unity.com/tutorial/coroutines

Unity UI and TextMeshPro
https://learn.unity.com/tutorial/textmeshpro

Audio in Unity (Playing Sound Effects)
https://learn.unity.com/tutorial/audio-in-unity

Using Tags and Layers
https://learn.unity.com/tutorial/tags-and-layers

Unity Scripting Reference:

MonoBehaviour (base class)
https://docs.unity3d.com/ScriptReference/MonoBehaviour.html

Transform
https://docs.unity3d.com/ScriptReference/Transform.html

Vector3
https://docs.unity3d.com/ScriptReference/Vector3.html

GameObject
https://docs.unity3d.com/ScriptReference/GameObject.html

Time.deltaTime
https://docs.unity3d.com/ScriptReference/Time-deltaTime.html

Moving Platform Script References:

Vector3.MoveTowards
https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html

Vector3.Distance
https://docs.unity3d.com/ScriptReference/Vector3.Distance.html

HealthBar Script References:

UnityEngine.UI.Slider
https://docs.unity3d.com/2019.4/Documentation/ScriptReference/UI.Slider.html

TextMeshProUGUI
https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/api/TMPro.TextMeshProUGUI.html

Mathf.Clamp
https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html

Audio Features (from PlayerBehaviour):

AudioSource
https://docs.unity3d.com/ScriptReference/AudioSource.html

AudioClip
https://docs.unity3d.com/ScriptReference/AudioClip.html

AudioSource.PlayOneShot
https://docs.unity3d.com/ScriptReference/AudioSource.PlayOneShot.html

Physics & Triggers:

Collider
https://docs.unity3d.com/ScriptReference/Collider.html

Destroy(gameObject)
https://docs.unity3d.com/ScriptReference/Object.Destroy.html

OnTriggerEnter / OnTriggerExit
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter.html
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerExit.html

Projectiles & Rigidbody:

Instantiate
https://docs.unity3d.com/ScriptReference/Object.Instantiate.html

Rigidbody.AddForce
https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html

Coroutines (e.g., RespawnRoutine):

IEnumerator & Coroutine
https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html

WaitForSeconds
https://docs.unity3d.com/ScriptReference/WaitForSeconds.html

