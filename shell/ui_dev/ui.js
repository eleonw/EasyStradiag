function plotFigure(data) {

    var layout = {
        font: {
            family: 'Arial, sans-serif;',
            size: 8,
            color: '#000'
        },
        width: 240,
        height: 240,
        margin: {
            l: 40,
            r: 40,
            t: 40,
            b: 40
        },
        // paper_bgcolor: '#3f3f3f',
        // plot_bgcolor: '#3f3f3f',
        dragmode: false,
        orientation: -90,

        
    };

    var trace = {
        r: data.degree,
        theta: data.theta,
        mode: 'lines',
        name: 'Strabismus Degree',
        line: {color: 'blue'},
        type: 'scatterpolar'
    }

    var data = [trace]

    var config = {
        "displaylogo": false,
        modeBarButtonsToRemove: ['toImage', 'zoom2d']
    }

    Plotly.newPlot('chart', data, layout, config);
}

function uiPlotFigure() {
    let data = {
        theta: [],
        degree: []
    }
    
    for(let i = 0; i < 360; ++i) {
        data.theta.push(i);
        data.degree.push(2 + Math.random())
    }
    
    plotFigure(data);
}

uiPlotFigure();


