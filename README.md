# Game Design based on Camera Projection Clipping, Shader Programming, Real-time Path Planning, and Triangulation Application

## 1. Introduction
This graduation project combines elements of parkour and role-playing games (RPG), primarily involving 3D game design, computer graphics and animation, virtual reality, and environment. The application of these technologies aims to translate creativity into tangible gaming experiences.

The main focus of the project is to enhance player immersion, thus establishing a **virtual reality (VR)** environment where players can directly interact with elements in the environment, providing sensory simulation for auditory, tactile, and visual experiences. 

Additionally, through reinforcement learning, **enemy AI** is trained to autonomously respond to player attacks, developing their own combat strategies, creating a more realistic and engaging battle experience. 

**The incorporation of ChatGPT technology into non-player characters (NPCs)** allows players to freely interact and converse with characters in the game world, providing an in-depth understanding of the constructed virtual world and creating a customizable gaming narrative.

The distinctive features of this project lie in utilizing various technologies to present detailed game elements for a realistic effect, simulating a sense of the real world. Techniques such as 

**(1) Oblique view frustum depth projection and clipping camera** are employed for portal functionality and the **marching cubes algorithm** for flexible spatial exploration. 

**(2) Shader programming** is utilized to create visual distortions and disruptions during time-switching pauses.

**(3) Real-time path planning** enables players to manipulate gravity for object attraction and release. 

**(4) Triangulation application** allows for physical objects to be sliced open by the player's sword, enhancing the realism of injury effects on enemies. Through these technologies, the developed game in this project offers visually enticing effects to captivate player enjoyment.

## 2. Project Framework
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Framework.jpg)

## 3. Technique
### 3-1. Oblique View Frustum Depth Projection and Clipping achieve the function of portal

#### The effect of the Portal :
<figure class= "half">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Portal1.jpg" width="305"/>
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Portal2.jpg" width="325"/>
</figure>

<figure class= "center">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Portal3.jpg"/>
</figure>

#### The process of calculating the thickness of the portal involves the following steps and the reason for recalculating the skew matrix is as follows : 
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/PortalVisual.png)

**Achieving the visual effect of teleportation involves the following:**

Each teleportation portal is equipped with its own camera, and the portal's visuals are rendered by the corresponding camera of the other portal. The rotation angles and position vectors of the cameras are synchronized with those of the player's camera.

Handling the rotation angles of the two cameras is accomplished through spatial transformations based on rotation matrices and quaternions. Additionally, it is crucial to calculate the thickness of the portal to prevent clipping of the camera view. 

Subsequently, the Oblique Matrix is recalculated to modify the near plane of the camera projection, ensuring it aligns with the surface of the portal to avoid rendering objects behind the portal. This enables players to switch between spaces through the teleportation portal.

Finally, the rendering process involves transforming points (vertices) from camera space to clip space using the Projection Matrix, allowing for the visualization of the scene.

#### The processing flow for the player's physical effects during the teleportation process is as follows :
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/PortalPhysics.png)

**To achieve the physical effects of teleportation as illustrated below:**

(1) First, obtain the rotation vector matrix of the player relative to the world (player to world) and dot product it with the player's position.

(2) Multiply the result by the rotation matrix of the world relative to the first portal (world to local).

(3) Further multiply by the rotation matrix of the second portal relative to the world (local to world).

(4) Finally, obtain the player's post-teleportation coordinates. Simultaneously, using these three rotation matrices, derive the quaternion representing the player's post-teleportation rotation.

Additionally, the transformation of the player's teleportation velocity vector significantly influences the teleportation effect. 

Initially, the absence of consideration for the physics velocity vector led to noticeable camera screen jitter during teleportation. 

Oringinally, I assumed it was a flaw in the mathematical code describing the visual effects of the portal. However, after a thorough investigation, it was discovered that the issue stemmed from the omission of the teleportation's physics velocity vector. 

Calculate the player's velocity (player to world), transform it relative to the first portal (world to local), and then convert this velocity vector to the rotation vector of the second portal (local to world).

#### Application in the game
**(1) In-Portal & Out-Portal**
<figure class= "half">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/AutoPortal.png" width="180"/>
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/AutoPortal2.png" width="380"/>
</figure>

Player can strategically navigate through the game by observing the map and enemy positions, leveraging the terrain features. They have a limited number of opportunities to set the locations of 'in-portals' and 'out-portals' to successfully complete the level.

**(2) Fixed Poral**
Players can utilize portals to access different areas, either to accomplish specific missions, acquire items, or progress through other regions to successfully complete objectives.

![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/FixedPortal.jpg)

### 3-2. Marching Cube

#### The effect of Marching Cube :
<figure class= "half">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/MarchingCube1.jpg" width="325"/>
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/MarchingCube2.jpg" width="300"/>
</figure>

#### Isosurface Search Algorithm Implementation Process :
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/MarchingCube.jpg)

**Developer Perspective:** 
First, understanding the relationships between various vertices and edges allows for the derivation of 15 cutting planes. Considering the adjustable weights for each point, there are countless possibilities for these cutting planes.

Due to the excessive number of cubes forming the mesh of an object, for real-time interaction with players and responsiveness to changes in vertex weights during ray casting, parallel computing techniques are required. 

Accelerating computations using Unity Compute Shader involves allocating compute buffers and having each thread calculate the changes in the Isoplane level by manipulating cube shapes.

**Player Perspective:**
Players can use ray casting to increase or decrease the weights of points on cubes, thereby altering the terrain environment. Introducing player skills for evading enemies and attacking adds an interactive element, achieving an engaging player-environment interaction effect.

#### Application in the game
When in the forest area, player faces the threat of bird enemies dropping rocks from the sky, causing damage. To mitigate this, players can alter the terrain by constructing structures resembling roofs to take cover from falling rocks. This strategic approach allows player to complete tasks within the specified time frame in the forest region.

### 3-3. ChatGPT NPC
#### Player's Perspective and ChatGPT NPC Dialogue Presentation :
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/ChatGPT.jpg)

In order to guide players and enhance game interactivity with Non-Player Characters (NPCs), we have integrated the ChatGPT API to facilitate more immersive dialogue interactions between players and NPCs. 

The process involves using the game script as a prompt for ChatGPT. Additionally, we have integrated Unity with Amazon's AWS Polly technology to convert the player's spoken content into text, serving as input for ChatGPT. After textual processing by GPT, NPC responses are generated. Subsequently, AWS Polly is employed to convert the text back into speech, delivering the response to the player for ongoing dialogue. This integration aims to provide a dynamic and non-scripted feedback mechanism for player actions in the game.

#### Integrating ChatGPT and AWS Polly in Player-NPC Dialogue Flowchart :
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/ChatGPT_flowchart.jpg)

### 3-4. Virtual Reality
#### Virtual Reality (VR) Game Visuals Presentation : 
![image](https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/VR.jpg)

Virtual Reality requires sensing the player's position and detecting changes in the player's perspective through head-mounted devices. 

In our VR implementation, player can interact directly with objects, such as picking them up or cutting them using hand gestures.

The most significant challenge encountered during the implementation process was the screen rendering issues when transitioning from the computer version to the VR version. The original computer version utilized the Universal Render Pipeline technology to enhance visual rendering effects. However, this technology faced various obstacles when applied to VR devices. 

For instance, rendering artifacts in teleportation portals required the development of a new portal shader to address visual jitter issues. As VR involves two cameras, representing the player's eyes, the approach to rendering had to adapt to the focus and overlap of images from both cameras. 

Although the issue was not fully resolved, utilizing the stencil buffer allowed the creation of scene duplicates at the positions of the two portals, ensuring their simultaneous display in front of the player's cameras. 

When objects from the alternate scene entered the player's camera view through the teleportation portals, rendering operations were executed. The portals employed a masking shader to determine which scene's objects should be rendered, effectively addressing the visual challenges in the VR version.

### Map in Portal Level 

==Level 1 Map==
<figure class= "half">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Level1.png" width="330"/>
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Level1Hint.png" width="240"/>
</figure>

==Forest Map==
<figure class= "half">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Forest.png" width="300"/>
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/ForestHint.png" width="260"/>
</figure>

==Level 2 Map==
<figure class= "half">
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/level2.png" width="300"/>
<img src="https://github.com/weihsinyeh/Unity_VR_Project_Latest/blob/main/images/Level2Hint.png" width="260"/> 
</figure>

## 🎉🎉🎉🎉🎉
Won the 1st place in National Cheng Kung University Computer Science Information Engineer 2023 Graduation Project Exhibition 

## Reference
1. Oblique View Frustum Depth Projection and Clipping (https://citeseerx.ist.psu.edu/viewdoc/download;jsessionid=255FB6BD9252A50A1A7166319C8A5CE0?doi=10.1.1.367.1894&rep=rep1&type=pdf)
2. https://polycoding.net/marching-cubes 
3. https://docs.unity3d.com/Manual/Shaders.html 
4. Floriani L D , Puppo E . An on-line algorithm for constrained Delaunay triangulation[J]. Cvgip Graphical Models & Image Processing, 1992, 54(4):290-300
5. Sven Koenig, Maxim Likhachev. (2002). D* Lite. In Proceedings of the 18th AAAI Conference on Artificial Intelligence(pp. 476-483). AAAI Press. https://aaai.org/papers/00476-d-lite/
