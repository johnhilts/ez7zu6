import React, { Component } from 'react';
import axios from 'axios';
import ExperienceEntry from '../components/ExperienceEntry';

class ExperienceContainer extends Component {
  constructor(props) {
      super(props);

      this.handleGetExperienceSuccess = this.handleGetExperienceSuccess.bind(this);
      this.handleSaveInfo = this.handleSaveInfo.bind(this);
      this.handleFullListClick = this.handleFullListClick.bind(this);

      this.experienceUrl = '/api/experience'

      this.state = { 'experiences': [], showFullList: false, totalRowCount: 0, }
    }

    handleGetExperienceSuccess(response) {
        this.setState({ experiences: response.data.experiences, totalRowCount: response.data.totalRowCount, })
    }

	// NOTE: componentDidMount is used to initialize a component with server-side info
	// fore more info, see react docs: https://facebook.github.io/react/docs/component-specs.html
    componentDidMount() {
        // TODO add some kind of helper class for the API calls ... maybe apiHelper with appropriate functions ... and we can pass in our then's, etc
        axios.get(this.experienceUrl).then(this.handleGetExperienceSuccess)
	}

    handleSaveInfo(event) {
        event.preventDefault();

        let notes = event.target[0].value

        axios.post('/api/experience', {
            Notes: notes
        })
            .then((response) => {
                let listUrl = response.headers.location
                axios.get(listUrl).then(this.handleGetExperienceSuccess)
            })

        // this.state.dates.push(date);
        // this.setState({ dates: this.state.dates });
        // this.props.onSaveDateInfo(this.state.dates);

        // event.target.reset();
    }

    handleFullListClick(event) {
        event.preventDefault();

        this.setState({ showFullList: true });
    }

    render() {
        // let experiences = [{Notes: 'one'}, {Notes: 'two'}, {Notes: 'three'}]
        return (
            <ExperienceEntry 
                onSubmit={this.handleSaveInfo} 
                onFullListClick={this.handleFullListClick} 
                experiences={this.state.experiences} 
                showFullList={this.state.showFullList}
            />
        )
    }
}

export default ExperienceContainer;
