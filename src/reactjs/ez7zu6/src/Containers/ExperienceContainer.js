import React, { Component } from 'react';
import axios from 'axios';
import ExperienceEntry from '../components/ExperienceEntry';

class ExperienceContainer extends Component {
  constructor(props) {
      super(props);

      this.handleSaveInfo = this.handleSaveInfo.bind(this);

      const config = { apiDomain: 'http://localhost:17726' }
      this.experienceUrl = `${config.apiDomain}/api/experience`

      this.state = { 'experiences': [], }
    }


	// NOTE: componentDidMount is used to initialize a component with server-side info
	// fore more info, see react docs: https://facebook.github.io/react/docs/component-specs.html
    componentDidMount() {
        // TODO add some kind of helper class for the API calls ... maybe apiHelper with appropriate functions ... and we can pass in our then's, etc
        axios.get(this.experienceUrl)
            .then((response) => this.setState({ experiences: response.data }))
	}

    handleSaveInfo(event) {
        event.preventDefault();

        let notes = event.target[0].value
        let timestamp = (new Date()).getTime();

        let config = {apiDomain: 'http://localhost:17726'};
        axios.post(`${config.apiDomain}/api/experience`, {
            Notes: notes
        })
            .then((response) => {
                let listUrl = response.headers.location
                axios.get(listUrl)
                    .then((response) => this.setState({experiences : response.data}))
            })

        // this.state.dates.push(date);
        // this.setState({ dates: this.state.dates });
        // this.props.onSaveDateInfo(this.state.dates);

        // event.target.reset();
    }

    render() {
        // let experiences = [{Notes: 'one'}, {Notes: 'two'}, {Notes: 'three'}]
        return <ExperienceEntry onSubmit={this.handleSaveInfo} experiences={this.state.experiences} />
    }
}

export default ExperienceContainer;
