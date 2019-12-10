import React, { Component } from "react";
import axios from "axios";

import { Grid, Typography } from "@material-ui/core";

import Instructor from "../../components/Instructor/Instructor";

class InstructorIndex extends Component {
  constructor(props) {
    super(props);
    this.state = {
      instructors: null
    };
    this.refreshInstructorIndex = this.refreshInstructorIndex.bind(this);
    this.addInstructor = this.addInstructor.bind(this);
  }

  componentDidMount() {
    this.refreshInstructorIndex();
  }

  refreshInstructorIndex() {
    fetch("https://localhost:5001/api/instructor")
      .then(res => res.json())
      .then(
        result => {
          this.setState({
            instructors: result
          });
        },
        error => {}
      );
  }

  addInstructor() {
    axios({
      method: "post",
      url: "https://localhost:5001/api/instructor",
      data: {
        FirstName: "Ben",
        LastName: "Mitchinson",
        BirthDate: "2000-1-1T00:00:00"
      }
    })
      .then(
        function(response) {
          this.refreshInstructorIndex();
        }.bind(this)
      )
      .catch(function(error) {});
  }

  render() {
    const { instructors } = this.state;

    return (
      <Grid container spacing={4}>
        <Grid item xs={12}>
          <Typography variant="h2">All Instructors</Typography>
        </Grid>
        {/* <Grid item xs={1}>
          <Button
            color="primary"
            size="small"
            variant="contained"
            onClick={this.addInstructor}
          >
            New Instructor
          </Button>
        </Grid> */}
        {instructors &&
          instructors.map(instructor => (
            <Grid key={instructor.instructorId} item xs={12}>
              <Instructor
                instructor={instructor}
                refreshIndex={this.refreshInstructorIndex}
              />
            </Grid>
          ))}
      </Grid>
    );
  }
}

export default InstructorIndex;

