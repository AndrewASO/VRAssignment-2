VR Assignment 2

Features 

Gameplay
- Level 1: The player is gathering 5 different ingredients to assemble a sandwich.
- Level 2: The player has 3 base ingredients in fronot of them and have to combine them or cook them to form a finished product then submit 5 randomized ones.
- Level 3: The player is on a platform looking to gather 5 different ingredients.

Exit
- There should be an object that appears allowing the player to close the game after 3 minutes have elapsed when clicking on it.

Environment
- Level 1: The player is in a room resembling a kitchen
- Level 2 & 3: The players are on platforms where the skybox was changed using a skybox from an asset package imported from the Unity Asset store.

Assets
- There were assets imported for the ingredients, skybox, and furniture for level 1 & 2. 



Level 2 Thoughts

- DropFunction() <br>
  There was a drop function initially implemented that didn't work after x amount of time for unknown reasons. When initially importing different levels - specifically the taco level somehow broke the script for it
and caused it to no longer work. It would drop objects when not looking at an object it would snap to was the intended purpose. The combination stations / cook stations would be where the base ingredients can snap to for combination to make
finalized products or to be cooked. It seemed like when importing another level, that somehow it always drops the item when looking at objets its meant to snap the ingredients to. So, I was able to narrow it down to 1 line that would
cause the ingredients to be dropped and commented it out so the main gameplay can still function. Will need to do further investigation to see why that function is triggering even when looking at an object its intended to snap the ingredients
to. May be beause of raycasts so need to remove those or find a work around. I believe I did attempt to find a work around or make sure that raycasts were properly hititng their target by changing how it aims out from the camera. 

- Cooking / Combination <br>
  There's an initially overlooked function where the shrimp cooks into tempura but its still combinable during the cooking process. Skipped over because low on time, but a solution could be implementing a status
where if an object can be combined and preventing the shrimp to be combined when cooking. Another one would be preventing any object from being placed onto the combination station. Ultimately, what I was aiming for
and in my opinion the best solution is having 2 different stations: cook station, and combination station. I imported a stove asset intended for the shrimp only being able to cook on it. Didn't have time to fix it, so
whatever is a combination station has the ability to cook. Could have a bool to differentiate if something can cook or not. Or maybe another entirely different script just for cooking to attach to the stove.

- Level transition <br>
 Wish to make something the player could've clicked on or a few seconds delay to enable a smoother level transition rather than having it jump straight to the next level once all the dishes have been served.

- Game closing <br>
  Besides an exit object being spawned, there should've been a way to leave. Maybe through a HUD or something consistent throughout the level that'll left the capability to leave. Although, for all intents and purposes, the 
implementation of allowing the player to leave through an object after 3 minutes works.
