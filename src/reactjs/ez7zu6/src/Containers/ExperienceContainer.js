import React, { Component } from 'react';

const EzButton = (props) => {
    const ezButtonClass = `btn btn-success glyphicon glyphicon-${props.iconName}`;
    return <button className={ezButtonClass} style={{color: 'black'}}></button>
}

class ExperienceContainer extends Component {
    render() {
        return (
            <div>
                <div>
                    <textArea cols='50' rows='3' placeholder='record an experience' />
                </div>
                <div className='autoInfoContainer'>
                    <EzButton iconName='thumbs-up' />
                </div>
                <div className='autoInfoContainer'>
                    <EzButton iconName='camera' />
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>Los Angeles, CA US</span>
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>Saturday, November 18, 2017 4PM</span>
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>65&deg;F - clear</span>
                </div>
                <div className='autoInfoContainer'>
                    <button className='btn btn-danger'>SAVE</button>
                </div>
            </div>
        );
    }
}

export default ExperienceContainer;
