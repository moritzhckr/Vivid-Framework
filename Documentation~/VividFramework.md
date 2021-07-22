# Vivid Framework 
Framework for simulating character animation in 3d digital buildings

# Installation
In the Unity Package Manager window click + Add from GitUrl and paste in the link to the Vivid Github repository.
`https://github.com/moritzhckr/Vivid-Framework.git`

# Setup
## Scene Preperation
<img src="/Documentation~/NavStatic.png" width="200" />
<img src="/Documentation~/NavMeshSettings.png" width="200"  />

Before importing any Vivid Components into your scene set all walkable areas,  obstacles and Walls 'Navigation Static'. 
In the Navigation Inspector Window (Window -> AI -> Navigation) In the Bake tab set your Agents settings and click on Bake.


<img src="/Documentation~/UnityNavmesh.png" width="400"  />

Unity calculates an Navigation Mesh on which a character can navigate.



## Import custom charaters
The Vivid Framework can handle all Characters and Avatars that Unity recognises as Humanoid.
In the import settings in the 'Rig' tab select 'Humanoid' and press apply.
- Drag and drop the Character into the scene, 
- On the Animator Component select the VividHumanoidController as the AnimationController.

- Add the VividCharacter script to the character.
    A NavmeshAgent, Rigidbody component aswell as the Vivid_thirdPersonCharacter script and Move Character script will be automatically added. 

- In the NavMeshAgent component adjust the speed (around 0.5f should be good) and the auto breaking value (if you habe many characters in the scene go for a higher value to prevent that characters will never stop). 

- In the VividCharacter script add the character meshes to the list. Accordingly to the gender of the Character. If ther are multiple Meshes within one rigged character drop add all of them. Make sure the list is set to 0 if it is not used (female mesh list on a male character).

- In the Rigidbody Component set to Mass to a value equivalent to the weight of an character (Heavier characters will push lighter chracters to the side).





### Mixamo 


## Import custom animationclips

## Create Start and Destination points
### 



## Create AnimationAreas

