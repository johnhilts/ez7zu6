import React, { Component } from 'react';
import ExperienceEntry from '../components/ExperienceEntry';

class ExperienceContainer extends Component {
  constructor(props) {
    super(props);

		this.handleSaveInfo = this.handleSaveInfo.bind(this);

		// this.state = { 'users': {}, }
    }

    handleSaveInfo(event) {
        event.preventDefault();

        let notes = event.target[0].value
        let timestamp = (new Date()).getTime();

        alert(`You entered: ${notes} - wow!`)

        // this.state.dates.push(date);
        // this.setState({ dates: this.state.dates });
        // this.props.onSaveDateInfo(this.state.dates);

        // event.target.reset();
    }

    render() {
        return <ExperienceEntry onSubmit={this.handleSaveInfo} />
    }
}

export default ExperienceContainer;
