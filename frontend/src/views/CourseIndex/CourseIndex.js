import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";
import { isAdmin } from "../../helpers/jwtHelpers";

import ConfirmDelete from "../../components/ConfirmDelete";
import RegistionIndex from "./RegistrationIndex";

import { Typography } from "@material-ui/core";
import MaterialTable from "material-table";

export default function CourseIndex(props) {
  const [allCourses, setAllCourses] = useState([]);
  const [refreshToggle, setRefreshToggle] = useState(false);
  const [deleteCourseID, setDeleteCourseID] = useState(null);
  const [deleteName, setDeleteName] = useState("");

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
          store.addNotification(
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
  }, [refreshToggle]);

  return (
    <>
      <ConfirmDelete
        deleteCourseID={deleteCourseID}
        closeWindow={() => setDeleteCourseID(null)}
        deleteName={deleteName}
        refresh={() => setRefreshToggle(!refreshToggle)}
      />
      <Typography variant="h2">
        <span role="img" aria-label="school logo">
          🎓
        </span>
        Browse Courses
      </Typography>
      <div style={{ maxWidth: "100%", marginTop: "2em" }}>
        <MaterialTable
          columns={getColumns()}
          data={allCourses}
          title="Courses"
          options={{
            emptyRowsWhenPaging: false,
            searchFieldStyle: {
              marginRight: "1em"
            }
          }}
          detailPanel={rowData => {
            return (
              <RegistionIndex
                courseName={rowData.courseName}
                registrations={rowData.registrations}
                refresh={() => setRefreshToggle(!refreshToggle)}
                linkToRegistrationID={id => {
                  props.history.push(`/section/${id}`);
                }}
              />
            );
          }}
          actions={
            isAdmin()
              ? [
                  {
                    icon: "delete",
                    tooltip: "delete",
                    onClick: (e, rowData) => {
                      setDeleteCourseID(rowData.courseId);
                      setDeleteName(rowData.section);
                    }
                  }
                ]
              : []
          }
          onRowClick={(e, rowData, toggelPanel) => toggelPanel()}
        />
      </div>
    </>
  );
}

const getColumns = () => {
  return [
    { title: "Code", field: "courseCode" },
    { title: "Course Name", field: "courseName" },
    { title: "Credit Hours", field: "creditHours" },
    { title: "Instructors", field: "instructors" },
    { title: "Pre-Requisites", field: "prereqtext" },
    { title: "Active Sections", field: "sections" }
  ];
};

const editDataABit = data => {
  data.forEach(course => {
    let instructors = [];
    let prereqnames = [];
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
    if (instructors.length) {
      instructors = instructors.reduce((combo, name) => {
        return combo + " and " + name;
      });
    }
    if (prereqnames.length) {
      prereqnames = prereqnames.reduce((combo, name) => {
        return combo + " and " + name;
      });
    }
    course.courseCode = course.registrations.length
      ? course.registrations[0].section.split(":")[0] +
        ":" +
        course.registrations[0].section.split(":")[1]
      : `Course ID: ${course.courseId}`;
    course.instructors = instructors;
    course.sections = course.registrations.length;
    course.prereqtext = !prereqnames.length ? "No Pre Reqs" : prereqnames;
  });
  return data;
};
