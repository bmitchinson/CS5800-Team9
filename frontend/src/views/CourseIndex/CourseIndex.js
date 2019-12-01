import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import { isAdmin, isInstructor, isStudent } from "../../helpers/jwtHelpers";

import { Typography } from "@material-ui/core";
import MaterialTable from "material-table";
import getHeaders from "../../helpers/getHeaders";

export default function CourseIndex() {
  const [allCourses, setAllCourses] = useState([]);

  useEffect(() => {
    const fetchCourses = async () => {
      const courses = await axios({
        method: "get",
        url: "https://localhost:5001/api/course",
        headers: getHeaders()
      })
        .then(response => {
          return response.data;
        })
        .catch(e => {
          store.addNotificaiton(
            notificationPrefs(
              "Error fetching all courses",
              "Please try again",
              "danger"
            )
          );
          return [];
        });
      setAllCourses(editDataABit(courses));
    };
    fetchCourses();
  }, []);

  return (
    <>
      <Typography variant="h2">
        <span role="img" aria-label="sad">
          🎓
        </span>
        Browse Courses
      </Typography>
      <div style={{ maxWidth: "100%", marginTop: "2em" }}>
        <MaterialTable
          columns={getColumns()}
          data={allCourses}
          title="Courses"
        />
      </div>
    </>
  );
}

const getColumns = () => {
  return [
    { title: "Code", field: "section" },
    { title: "Course Name", field: "courseName" },
    { title: "Credit Hours", field: "creditHours" },
    { title: "Instructors", field: "instructors" },
    { title: "Pre-Requisites", field: "prereqtext" },
    { title: "Active Sections", field: "sections" }
  ];
};

const editDataABit = data => {
  // console.log("before edit", data);
  // + string of instructor names
  // + # of sections to each
  // + prereq names
  // to each course object
  data.forEach(course => {
    let instructors = [];
    let prereqnames = [];
    let totalEnrolled = 0;
    course.registrations.forEach(section => {
      instructors.push(
        section.instructor.firstName[0] + ". " + section.instructor.lastName
      );
      section.prerequisites.forEach(prereqobj => {
        if (!prereqnames.includes(prereqobj.course.courseName)) {
          prereqnames.push(prereqobj.course.courseName);
        }
      });
    });
    instructors = instructors.reduce((combo, name) => {
      return combo + " and " + name;
    });
    if (prereqnames.length) {
      prereqnames = prereqnames.reduce((combo, name) => {
        return combo + " and " + name;
      });
    }
    course.instructors = instructors;
    course.sections = course.registrations.length;
    course.prereqtext = !prereqnames.length ? "No Pre Reqs" : prereqnames;
  });
  // console.log("after edit", data);
  return data;
};
