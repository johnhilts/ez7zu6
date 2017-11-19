import React from 'react';
import ReactDOM from 'react-dom';
import ExperienceContainer from './Containers/ExperienceContainer';
// import './index.css';
// import App from './App';
// import registerServiceWorker from './registerServiceWorker';

let addExperience = document.getElementById('addExperience');
if (addExperience){
    ReactDOM.render(<ExperienceContainer />, addExperience);
}
// ReactDOM.render(<App />, document.getElementById('root'));
// registerServiceWorker();

