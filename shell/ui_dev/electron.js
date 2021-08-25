const { remote } = require('electron');

const { dialog, BrowserWindow } = remote;

let infoWin = new BrowserWindow({
    width: 600,
    height: 450,
    center: true,
    resizable: false,
    minimizable: false,
    maximizable: false,
    fullscreen: false,

    // frame: false,

    parent: remote.getCurrentWindow(),

    title: '填写基本信息',
    icon: 'logos/logo.ico',

    autoHideMenuBar: true,

    show: false,
});

function openInfoWin() {
    console.log(1);
    infoWin.show();
    infoWin.loadFile('ui_dev/info.html');
}

openInfoWin();