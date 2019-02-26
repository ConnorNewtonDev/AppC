The GameWorld project is a very simple game framework that contains a single Actor in a World. The game loop updates at 30 fps and renders the Actor as a simple triangle.



Please update the code to:

[FINISHED]
- Make the Actor move autonomously.

[FINISHED]
- Add multiple Actors into the World, each with a different start position, rotation etc.

[FINISHED]
- Make one Actor player controlled, the others should move autonomously.






What SEGA HARDlight is looking for is your approach to updating the code - it is the quality of the updates that are important. 

You should consider that your updates would be code-reviewed by peers prior to being committed to the repository.

Consider how polymorphism could be used to enhance the architecture.
If you finish the work above, and you have time, consider further enhancements, for example:

[Partial Implementation]
- Accelerate and decelerate the Actor's movement and rotation.

- Allow for Actor to change control method dynamically, from player controller to autonomous.

- Have multiple player controlled Actors - i.e. player 1 + 2.

- Define the size of the World and make the Actors remain within a set boundary.

- Some Actors could chase or avoid other Actors 
- e.g. Actors could be "prey" that stay a set distance away from the player, who is a "hunter".
