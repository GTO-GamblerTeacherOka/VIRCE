# V.I.R.C.E
This repository is for the V.I.R.C.E project.
V.I.R.C.E is a service for user to communicate and play with other users in a virtual world.

# Tech Stack
- System Client : Unity
- System Server : .NET 7 application
- Communication : UDP socket
- Communication Protocol : Custom application layer protocol

## Dependency Packages
- VRoid SDK
- VRoid SDK for Multiplayer
- Extenject
- UniRx
- SciFi UI
- SciFi Platform City

## Environment
- Unity 2021.3.24f1

# How to start development
1. Install Unity Editor.
2. Clone this repository.
3. Open the project with Unity Editor.
4. Import the packages listed in the Dependency Packages section.
5. Open the scene `Assets/Scenes/launch.scene`.
6. Press the play button.

# About the project
## Project Structure
- `Assets/Scenes/launch.scene` : The scene to launch the game.
- `Assets/Scenes/main.scene` : The scene to play the game.

### Launch Scene
This scene is for launching the game.
When the game is launched, this scene is loaded.
This scene is used for login with Pixiv VRoid Hub account and loading the main scene.

### Main Scene
This scene is lobby scene.

If you want to design the lobby space, you must open this scene.

# License
This project is not licensed because VRoid SDK is not published under an open source license.
So this project will not be published under an open source license.
