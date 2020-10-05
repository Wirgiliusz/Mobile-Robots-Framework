# Mobile-Robots-Framework
 
## To-Do
### New
- [x] 1. Basic shape with 2 working wheels:
    - [x] a. Acceleration/Deceleration
    - [x] b. Speed control
    - [x] c. Steering
    - [x] d. Keyboard shortcuts
- [ ] 2. Sensors:
    - [x] a. Measuring distance
    - [ ] b. Using sensors measurments to control velocity
    - [ ] c. (?) Wheels encoders
- [x] 3. Obstacles
- [ ] 4. Robot model based on implemented features
- [ ] 5. PID controller:
    - [ ] a. Automatic acceleration and deceleration before obstacle
- [ ] 6. Second robot model
- [ ] 7. Environments:
    - [ ] a. Different obstacles
- [ ] 8. UI:
    - [x] a. Informations about robot:
        - [x] Wheel speed
        - [x] Velocity
        - [x] Sensors readings
        - [x] Travel path (trail)
    - [ ] b. Switching between robots models
    - [ ] c. Switching between environments
    - [ ] d. Camera settings:
      - [x] Follow the robot
      - [x] Overhead
      - [x] Free view
- [ ] 9. Program structure:
  - [x] a. Main robot script that controls smaller components:
    - Sensors scripts
    - Motors (wheels) scripts
  - [x] b. Main script has acces to smaller components readings
  - [x] c. Main script can send signals to smaller components scripts 
  - [ ] d. Move UI/Camera control elements from RobotController.cs to different script (ProgramController.cs?)

### Improvements
- [ ] 1. UI:
  - [ ] a. Better free view camera
    - [ ] rotation
  - [x] b. Better overhead camera
    - [x] zoom level change