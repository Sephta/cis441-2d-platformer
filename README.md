# Experimental 2.5D Action Platformer

Source code for my final project in CIS 441 (Computer Graphics) @ University of Oregon

This experimental platformer, as I will be refering to it, was the core concept I had for my final project. My goal was to build a simple platformer with tight movement mechanics and simple but fun combat.

The final feature set in the vertical slice I built is as follows:

* Full 2D character movement in a "3D" environment
    * The player can double jump, dash up to two times in the air
    * The player can also perform a multihit combo on enemies (fairly extensible system, but I currently have only implented a three hit combo)
* Enemy State Machine (I would specifically call it behavior driven AI)
    * Each enemy slime in the main level is controlled by a state manager that calls the initial state and then on each frame updates the current state behavior
    * Each behvaior will determine which behavior it transitions to and under what conditions
* To emulate a "full" gamplay loop there is a win condition and a loose condition
    * Win condition: To win, simply reach the end of the "The Forest" level where you will see a summary of the collectables you obtained as well as how many enemies you have slain
    * Loose Condition: there are two scenarios, the player runs out of health or the player falls in the pit early on in the level. In either case the player will see the same summary of colletables and achievements. The player is then prompted to either quit to the main menu, or try again with all progress maintained but they start at the beginning of the level.
* Certain measures were taken to optimize the WebGL build of the game. Currently the game still does not run well on MacOS systems, but in general performance should be servicable
    * Utilized unity's built in system for Occlusion Culling of objects outside of the main camera's view frustrum
    * Utilized a free Asset Store package to combine meshes on an object for better optimization.
    * Took time and care to plan out which assets in the scene should remain static vs dynamic
