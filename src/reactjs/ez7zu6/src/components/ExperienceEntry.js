import React from 'react';

const EzButton = (props) => {
    const ezButtonClass = `btn btn-success glyphicon glyphicon-${props.iconName}`;
    return <button className={ezButtonClass} style={{ color: 'black' }}></button>
}

const RenderForm = (props) => {
    return (
        props.showFullList 
        ? <div>&nbsp;</div>
        :
        <form onSubmit={props.onSubmit}>
            <div>
                <div>
                    <textarea cols='50' rows='3' placeholder='record an experience' style={{ color: 'black' }} />
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
        </form>
    )
}

const RenderList = (props) => {
    let moreLabel = props.showFullList ? 'More' : 'Full List';
    return (
        <div>
            <ul>
                {props.experiences.map((experience, index) => <li key={index}>{experience.notes}</li>)}
            </ul>
            <a role='button' onClick={props.onFullListClick}>{moreLabel}</a>
        </div>
    )
}

export default function ExperienceEntry(props) {
    return (
        <div>
            <div>
                <RenderForm {...props} />
                <RenderList {...props} />
            </div>
        </div>
    );
}
