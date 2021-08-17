# strabismus-diagnosis
This is a joint project between [Beihang University](https://www.buaa.edu.cn/) and [Peking Union Medical College Hospital](https://www.pumch.cn/index.html), aiming at diagnosing strabismus of different kind with the help of VR and eye tracking technologies. 

The project is exclusive for Windows

## Project Composition

+ **Core**: The VR scene running in the HTC VIVE headset for the test-taker. Based on Unity.
+ **Shell**: The GUI running on PC for the tester to control the test and get access to the testing data. Based on Electron.

IPC between the core and the shell is mainly dependant on WebSocket and WebRTC.

