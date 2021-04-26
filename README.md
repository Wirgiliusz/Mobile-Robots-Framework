# Mobile Robots Framework
Engineering project created as a part of Engineering thesis titled **"Design of a framework for modelling mobile robots behaviour using a Unity framework"**.
 
---

# Project description
Main goal of the project was to show that Unity framework, which main purpose is providing a environment for creating video games, can be used to create a simulation sofware for mobile robots. Contrary to appearances the fields of game development and robotics are very close to each other, which made this project possible.

---

# Results
<img src="docs/imgs/preview.png" width="1000">

### Implemented features:
- Basic robots components modeled in accordance to real parts:
  - motors,
  - wheels,
  - sensors,
- Scene with 3 different controllable cameras, every with 2 modes - free movement and following the robot model:
  - view from the top,
  - view from the back of the model,
  - view at an angle,
- User interface with informations about all the robots in the simulation:
  - robot names,
  - velocities,
  - motors power,
  - sensors readings,
  - simulation time,
- User interface with buttons for simulation control:
  - start and restart button,
  - list with saved scenes,
  - toggle buttons for visual effects - robot trajectories and sensors rays
- Possibility to implement unique robot drivers - two drivers based on PD controller implemented as an example:
  - rotating by given angle,
  - driving untill given distance from obstacle,

### Sample robots models
Both robots models were created using implemented components and functions of the simulator
<img src="docs/imgs/model-basic.png" width="300"> <img src="docs/imgs/model-micromouse.png" width="300">

---
 
<details>
  <summary>Finished tasks</summary>
 
## To-Do
### New
- [x] 1. Basic shape with 2 working wheels:
    - [x] a. Acceleration/Deceleration
    - [x] b. Speed control
    - [x] c. Steering
    - [x] d. Keyboard shortcuts
- [x] 2. Sensors:
    - [x] a. Measuring distance
    - [x] b. Using sensors measurments to control velocity
    - [x] c. Wheels encoders
- [x] 3. Obstacles
- [x] 4. Robot model based on implemented features
- [x] 5. PID controller:
    - [x] a. Automatic acceleration and deceleration before obstacle
- [x] 6. Second robot model (Micromouse)
  - [x] a. Add model
  - [x] b. Write driver
- [ ] 7. Environments:
    - [ ] a. Different obstacles
- [x] 8. UI:
    - [x] a. Informations about robot:
        - [x] Wheel speed
        - [x] Velocity
        - [x] Sensors readings
        - [x] Travel path (trail)
    - [x] b. Switching between robots models
    - [x] c. Switching between environments
    - [x] d. Camera settings:
      - [x] Follow the robot
      - [x] Overhead
      - [x] Free view
      - [x] Possibility to toggle robot following by clicking
    - [x] e. Toggles:
      - [x] Robots paths
      - [x] Sensors rays
- [x] 9. Program structure:
  - [x] a. Main robot script that controls smaller components:
    - Sensors scripts
    - Motors (wheels) scripts
  - [x] b. Main script has acces to smaller components readings
  - [x] c. Main script can send signals to smaller components scripts 
  - [x] d. Move UI/Camera control elements from RobotController.cs to different script (ProgramController.cs?)

### Bugfixes
- [x] 1. Micromouse robot model:
  - [x] - Fix model shaking
    - Cause is probably wheels / weight
  - [x] Overhead camera weird models

### Improvements
- [ ] 1. UI:
  - [ ] a. Better free view camera
    - [ ] rotation
  - [x] b. Better overhead camera
    - [x] zoom level change
  - [x] c. Dynamic UI for multiple robots
  - [x] d. Buttons for starting and restarting simulation

- [x] 2. PID controller:
  - [x] a. Decide what to do with negative 'u':
    - using 'arrived' bool makes robot stop before destination
    - not using 'arrived' makes it do weird moves close to destination

</details>
