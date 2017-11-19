import React, { Component } from 'react';

class ExperienceContainer extends Component {
    render() {
        return (
            <div>
                <div>
                    <textArea cols='50' rows='5' />
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>Thumbsup</span>
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'><button>camera</button></span>
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>Los Angeles, CA US</span>
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>Saturday, November 18, 2017</span>
                </div>
                <div className='autoInfoContainer'>
                    <span className='autoInfo'>65&deg;F - clear</span>
                </div>
                <div className='autoInfoContainer'>
                    <button>SAVE</button>
                </div>
            </div>
        );
    }
}

export default ExperienceContainer;
