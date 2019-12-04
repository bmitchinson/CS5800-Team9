import React, { useState } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import { Typography } from "@material-ui/core";
import MaterialTable from "material-table";
import ConfirmDelete from "../../components/ConfirmDelete";
import { isAdmin, isStudent, getId } from "../../helpers/jwtHelpers";
import getHeaders from "../../helpers/getHeaders";
import notificationPrefs from "../../helpers/notificationPrefs";

export default function RegistrationIndex(props) {
  const { registrations, linkToRegistrationID, courseName, refresh } = props;
  const [deleteRegistrationID, setDeleteRegistrationID] = useState(null);
  const [deleteName, setDeleteName] = useState("");

  const getActions = () => {
    const actions = [];
    isAdmin() &&
      actions.push({
        icon: "delete",
        tooltip: "Delete Course",
        onClick: (e, rowData) => {
          setDeleteRegistrationID(rowData.registrationId);
          setDeleteName(rowData.section);
        }
      });
    isStudent() &&
      actions.push({
        icon: "add",
        tooltip: "Enroll",
        onClick: (e, rowData) => {
          console.log("id:", getId());
          console.log("regId:", rowData.registrationId);
          axios({
            method: "POST",
            url: "https://localhost:5001/api/studentenrollment",
            headers: getHeaders(),
            data: {
              StudentId: getId(),
              RegistrationId: rowData.registrationId
            }
          })
            .then(res => {
              store.addNotification(
                notificationPrefs(
                  `Successfuly enrolled in ${rowData.section}`,
                  `You are now a student in ${rowData.section}. Go get to work!`,
                  "success"
                )
              );
              refresh();
            })
            .catch(e => {
              store.addNotification(
                notificationPrefs(
                  `Error enrolling for ${rowData.section}`,
                  "Please try again",
                  "danger",
                  e.response
                )
              );
            });
        }
      });
    return actions;
  };

  return (
    <>
      <ConfirmDelete
        deleteRegistrationID={deleteRegistrationID}
        closeWindow={() => setDeleteRegistrationID(null)}
        deleteName={deleteName}
        refresh={refresh}
      />
      <Typography style={{ margin: ".8em", marginLeft: "2em" }} variant="h6">
        {`Sections of ${courseName}`}
      </Typography>
      <div
        style={{
          margin: ".8em",
          marginLeft: "3em",
          marginRight: "3em",
          marginBottom: "3em"
        }}
      >
        <MaterialTable
          columns={getColumns()}
          data={editDataABit(registrations)}
          title={`Course Sections for ${courseName}`}
          options={{
            search: false,
            toolbar: false,
            showTitle: true,
            paging: false
          }}
          actions={getActions()}
          onRowClick={(e, rowData, toggleRow) => {
            linkToRegistrationID(rowData.registrationId);
          }}
        />
      </div>
    </>
  );
}

const getColumns = () => {
  return [
    { title: "Instructor", field: "instructorname" },
    { title: "Course Code", field: "section" },
    { title: "Enrollment Limit", field: "enrollmentLimit" }
  ];
};

const editDataABit = registrations => {
  registrations.forEach(reg => {
    reg.instructorname =
      reg.instructor.firstName + ", " + reg.instructor.lastName;
  });
  return registrations;
};
