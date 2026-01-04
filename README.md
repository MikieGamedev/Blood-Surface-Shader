# Blood-Surface-Shader
This is the repository that holds the information and scripts for blood sprays, stains and textures

To use this, place a BloodCollisionEmitter.cs script on a particle system that you want to stain on the environment.
Make sure that there is an object with the name 'Splatter Part' (This name is required for the system to work).
This particle system, Splatter Part, should have a high max particle limit but without any emmission.
Set Splatter Part's Render Mode (Particle System > Renderer > Render Mode) to Mesh, then set the 'Meshes' array to only contain one Quad mesh.
Set Splatter Part's Render Alignment (Particle System > Renderer > Render Alignment) to World.
