import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

import { makeStyles } from "@material-ui/core/styles";

import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import SchoolIcon from "@material-ui/icons/School";

import getHeaders from "../../../helpers/getHeaders";

import {
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Divider
} from "@material-ui/core";

const useStyles = makeStyles(theme => ({
  link: {
    textDecoration: "none",
    color: "black"
  }
}));

export default function NonAdminLinks(props) {
  const classes = useStyles();
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    const fetchEnrollments = async () => {
      const courses = await axios({
        method: "get",
        url: "https://localhost:5001/api/studentenrollment",
        headers: getHeaders()
      })
        .then(function(response) {
          let enrollments = response.data[0];
          let courses = [];
          let ids = [];
          console.log('res:', response);
          enrollments.forEach(enrolment => {
            if (!ids.includes(enrolment.registration.course.courseId)) {
              ids.push(enrolment.registration.course.courseId);
              courses.push([
                enrolment.registration.section,
                enrolment.registration.course.courseName,
                enrolment.registration.course.courseId
              ]);
            }
          });
          return courses;
        })
        .catch(function(e) {
          console.log("Error Getting Enrollments");
          return [];
        });
      setCourses(courses);
    };
    fetchEnrollments();
  }, []);

  // ["code", "name", "id"]
  // let courses = [["ECE:4000", "Circuits", 3], ["CS:3524", "Fundamentals", 2]];

  return (
    <List>
      <Link to="/myaccount" className={classes.link}>
        <ListItem button onClick={props.closeDrawer}>
          <ListItemIcon>
            <AccountCircleIcon />
          </ListItemIcon>
          <ListItemText primary={"My Account"} />
        </ListItem>
      </Link>
      <Link to="/courses" className={classes.link}>
        <ListItem button key="courses" onClick={props.closeDrawer}>
          <ListItemIcon>
            <SchoolIcon />
          </ListItemIcon>
          <ListItemText primary={"Course Index"} />
        </ListItem>
      </Link>
      <Divider />
      <ListItem>
        <ListItemText primary={"Enrolled Courses:"} />
      </ListItem>
      {courses.map(c => (
        <Link to={`/course/${c[2]}`} className={classes.link} key={c[2]}>
          <ListItem button key={c[0]} onClick={props.closeDrawer}>
            <ListItemIcon>
              <SchoolIcon />
            </ListItemIcon>
            <ListItemText
              primary={`${c[0]} - ${c[1]}`.substring(0, 15) + "..."}
            />
          </ListItem>
        </Link>
      ))}
    </List>
  );
}
