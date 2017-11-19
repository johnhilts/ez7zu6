import React from 'react';
import ReactDOM from 'react-dom';
import ExperienceContainer from './containers/ExperienceContainer';
// import './index.css';
// import App from './App';
// import registerServiceWorker from './registerServiceWorker';

let experiences = document.getElementById('experiences');
if (experiences){
    ReactDOM.render(<ExperienceContainer />, experiences);
}
// ReactDOM.render(<App />, document.getElementById('root'));
// registerServiceWorker();

