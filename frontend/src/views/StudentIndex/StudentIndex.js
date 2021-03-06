import React, { Component } from "react";
import axios from "axios";

import { Grid, Typography } from "@material-ui/core";

import Student from "../../components/Student/Student";

class StudentIndex extends Component {
  constructor(props) {
    super(props);
    this.state = {
      students: null
    };
    this.refreshStudentIndex = this.refreshStudentIndex.bind(this);
    this.addStudent = this.addStudent.bind(this);
  }

  componentDidMount() {
    this.refreshStudentIndex();
  }

  refreshStudentIndex() {
    fetch("https://localhost:5001/api/student")
      .then(res => res.json())
      .then(
        result => {
          this.setState({
            students: result
          });
        },
        error => {}
      );
  }

  addStudent() {
    axios({
      method: "post",
      url: "https://localhost:5001/api/student",
      data: {
        FirstName: "Ben",
        LastName: "Mitchinson",
        BirthDate: "2000-1-1T00:00:00"
      }
    })
      .then(
        function(response) {
          this.refreshStudentIndex();
        }.bind(this)
      )
      .catch(function(error) {});
  }

  render() {
    const { students } = this.state;

    return (
      <Grid container spacing={4}>
        <Grid item xs={12}>
          <Typography variant="h2">All Students</Typography>
        </Grid>
        {/* <Grid item xs={1}>
          <Button
            color="primary"
            size="small"
            variant="contained"
            onClick={this.addStudent}
          >
            New Student
          </Button>
        </Grid> */}
        {students &&
          students.map(student => (
            <Grid key={student.studentId} item xs={12}>
              <Student
                student={student}
                refreshIndex={this.refreshStudentIndex}
              />
            </Grid>
          ))}
      </Grid>
    );
  }
}

export default StudentIndex;
