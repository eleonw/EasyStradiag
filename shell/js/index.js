`use strict`;

require('webrtc-adapter');

const PORT_NO = 8081;


var video = document.querySelector('#unity-video');
var remoteStream = new MediaStream();
video.srcObject = remoteStream;
var dataChannel;
var pc;

pc = new RTCPeerConnection();
pc.ontrack = e => {
    console.log('OnTrack event: ', e)
    remoteStream.addTrack(e.track);
}
pc.ondatachannel = e => {
    console.log("Ondatachannel event: ", e);
    console.log(dataChannel);
    // dataChannel = e.channel;
    dataChannel.onmessage = onMessage;
    dataChannel.send("Data channel established.");
    console.log(dataChannel);
}
pc.oniceconnectionstatechange = e => {
    let state = e.target.connectionState;
    console.log("IceConnection state changed: ", state);
    if (state === 'connected') {
        ws.close();
        console.log('-'*30);
        console.log("Signaling complete, close webSocket");
        console.log('-'*30);
    }
}

var dataElements = {
    left: {
        origin: document.querySelector("#left-origin"),
        direction: document.querySelector('#left-direction')
    },
    right: {
        origin: document.querySelector('#right-origin'),
        direction: document.querySelector('#right-direction')
    },
    targetPosition: document.querySelector('#target-position'),
    expectedDirection: document.querySelector('#expected-direction'),
    strabismusDegree: document.querySelector('#strabismus-degree'),
}

function changeText(element, text) {
    element.innerText = text;
}

function onMessage(msg) {

    console.log("receive message through WebRTC from Unity: ", msg);
    let data = JSON.parse(msg.data);
    console.log()

    let eles = dataElements;
    changeText(eles.left.origin, data.leftOrigin);
    changeText(eles.left.direction, data.leftDirection);
    changeText(eles.right.origin, data.rightOrigin);
    changeText(eles.right.direction, data.rightDirection);
    changeText(eles.targetPosition, data.targetPosition);
    changeText(eles.strabismusDegree, data.strabismusDegree);
    

}





