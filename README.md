# Mobile-Robots-Framework
 
## To-Do
### New
- [x] Basic shape with 2 working wheels:
    - [x] Acceleration/Deceleration
    - [x] Speed control
    - [x] Steering
    - [x] Keyboard shortcuts
- [ ] Sensors:
    - [x] Measuring distance
    - [ ] Using sensors measurments to control velocity
    - [ ] (?) Wheels encoders
- [ ] Obstacles
- [ ] Robot model based on implemented features
- [ ] PID controller:
    - [ ] Automatic acceleration and deceleration before obstacle
- [ ] Second robot model
- [ ] Environments:
    - [ ] Different obstacles
- [ ] UI:
    - [ ] Informations about robot:
        - [ ] Wheel speed
        - [ ] Velocity
        - [ ] Sensors readings
    - [ ] Switching between robots models
    - [ ] Switching between environments
- [ ] Program structure:
  - [ ] Main robot script that controls smaller components:
    - Sensors scripts
    - Motors (wheels) scripts
  - [ ] Main script has acces to smaller components readings
  - [ ] Main script can send signals to smaller components scripts 