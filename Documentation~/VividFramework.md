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

### Import Vivid Spawn Manager

In the Vivid Framework Package loacate the Prefabs folder and drag an drop the VividSpawnManager into the scene hierachy. 
Drag an drop the references to to male and female character prefabs inside the corresponding lists.

## Import and Prepare Custom Charaters
The Vivid Framework can handle all Characters and Avatars that Unity recognises as Humanoid.
In the import settings in the 'Rig' tab select 'Humanoid' and press apply.
- Drag and drop the Character into the scene, 
- On the Animator Component select the VividHumanoidController as the AnimationController.

- Add the VividCharacter script to the character.
    A NavmeshAgent, Rigidbody component aswell as the Vivid_thirdPersonCharacter script and Move Character script will be automatically added. 

- In the NavMeshAgent component adjust the speed (around 0.5f should be good) and the auto breaking value (if you habe many characters in the scene go for a higher value to prevent that characters will never stop). 

- In the VividCharacter script add the character meshes to the list. Accordingly to the gender of the Character. If ther are multiple Meshes within one rigged character drop add all of them. Make sure the list is set to 0 if it is not used (female mesh list on a male character).

- In the Rigidbody Component set to Mass to a value equivalent to the weight of an character (Heavier characters will push lighter chracters to the side).

It is possible to use all Mixamo or Mixamo rigged avatars.

## Create Animation Areas
<img src="/Documentation~/createAnimationArea.png" width="400"  />

To create AnimationAreas simply click on the Vivid in the menu bar, form the dropdown select "Create Animation Area".
An AnimationArea gameobject is then create in the scene.
- Position and Scale it in the scene according to your needs.
- On the VividAnimationArea Script set the AnimationNames list lenght to the number of animations it should be able to play.
- The list accepts the names of the animationclips as strings.
- set the AnimationLayerIndex according to the animationlayer in the animation controller. (Default should be 1).

For debugging reasons it is possible to set the "ShowAreaMesh" bool to true if it is neccessary to keep the areas visible during playtime, otherwise the Mesh Renderer is disabled.

If multiple animation clips are added to the list the script will randomly select which one to play.

## Import Custom Animation Clips
<img src="/Documentation~/AnimatorClips.PNG" width="800"  />

To add additional Animation clips that can be used with the AnimationAreas import the clips to the project.
- In the Import Settings -> Rig tab select Humanoid for the Animation Type.
- Open the Vivid Humanoid Controller in the Animator window.
- Drag and drop the new animationclip to the collideLayer or create a new animation layer. Keep in mind that you need to change the animation layer index in the AnimationArea settings as well.
- Set the AnimationLayer Mask to your need (if it is a Area where characters walk through it is best to set it only to the upper body half)
- Set the transitions to Exit, other animation clips or to itself (to loop) as you like. 



## Create Start and Destination points
### Start Point
To create a starting (spawn) point click on the menu bar on Vivid - Create Starting Point
<img src="/Documentation~/createStartPoint.png" width="400"  />

Position the start point gameobject in the scene according to your needs. Ideally it is placed around 0.5m above the ground.
A start pont can have multiple configurations:

1) One time single spawn 
2) Interval
3) Interval with random time between spawns

For exact implementation examples have a look at the [Vivid-Framework-Implementation Project](https://github.com/moritzhckr/vivid-framework-implementation) -> Spawn Scene.

The  SpawnAmount sets the number of characters that will be created in a single spawn call.
If the FixedDestinaton reference is not set the spawner script will select a random Destination from the destination component list on the VividSpawnManager 

### Destination Point 
To create a destination point click on the menu bar on Vivid -> Create Destination Point.
Position the destination point gameobject in the scene according to your needs. Ideally it is placed around 0.5m above the ground.

<img src="/Documentation~/createDestinationPoint.png" width="400"  />

The destination point script can be set to "Destroy on Arrival".
If this bool is set to true, the characters will get Destroyed if the reach the destination. 
The destroy method is called from within the MoveCharacter script.

## Spawn on Time

To spawn characters at a specific time use the SpawnOnTime scirpt.
- Attach it to a gameobject (VividSpawnManager or StartPoint).
- Reference the VividSpawnManager and the Clock script.
- Create a Resources folder and create a .json file in it.
- Put the name of the json file (with .json extenstion) in the JsonFileName field on the spawnOnTime script.

On Play, the script will read the values of the json file and adds it to a list of SpawnInfo objects.


A SpawnInfo Object has the following parametes:
- StartPosition : The Name of the Gameobject from which the characters should start.
- characterCount: Number of Characters
- DestinationName: The Name of the Gameobject to which the characters walk to.
- TimeHour: The Hour on which the spawn should happen
- TimeMinute: The Minute on which the spawn should happen
- TimeSeconds: The Second on which the spawn should happen

#### Example JsonFile

```
{
    "SpawnList": [
        {
            "StartPoint": "StartPoint_4",
            "characterCount": "10",
            "destinationPoint": "DestinationPoint_4",
            "TimeHour": "12",
            "TimeMinute": "08",
            "TimeSeconds": "00",
            "percentFemales": "100"
        }


    ]
}
```
This will spawn a group of 10 charachters which are 100% Female at StartPoint_4 at 12:00:00 o clock and they will work to DestinationPoint_4_


wf