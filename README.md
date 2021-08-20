# EasyStradiag Strabismus Diagnosis System
This is a joint project between [Beihang University](https://www.buaa.edu.cn/) and [Peking Union Medical College Hospital](https://www.pumch.cn/index.html), aiming at diagnosing strabismus of different kinds with VR and eye tracking technologies. 

## Project Composition

+ **Core**: The VR scene running in the HTC VIVE headset for the test-taker. Based on Unity.
+ **Shell**: The console running on PC for the tester to control the test and get access to the testing data. Based on Electron.

IPC between the core and the shell is mainly dependant on WebSocket and WebRTC.

