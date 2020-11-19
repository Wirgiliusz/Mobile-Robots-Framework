# Mobile-Robots-Framework
 
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

- [ ] 2. PID controller:
  - [ ] a. Add I
  - [x] b. Decide what to do with negative 'u':
    - using 'arrived' bool makes robot stop before destination
    - not using 'arrived' makes it do weird moves close to destination 