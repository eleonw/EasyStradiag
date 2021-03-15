`use strict`;

const PORT_NO = 8081;


var video = document.querySelector('#vr-video');
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
    dataChannel.onMessage = onMessage;
    dataChannel.send("Data channel established.");
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

function onMessage(msg) {
    console.log("receive message through WebRTC from Unity: ", msg);
}





