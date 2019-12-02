import React, { useState } from "react";

import { Typography } from "@material-ui/core";
import MaterialTable from "material-table";
import ConfirmDelete from "../../components/ConfirmDelete";
import { isAdmin } from "../../helpers/jwtHelpers";

export default function CourseIndex(props) {
  const { registrations, linkToRegistrationID, courseName, refresh } = props;
  const [deleteRegistrationID, setDeleteRegistrationID] = useState(null);
  const [deleteName, setDeleteName] = useState("");

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
          actions={
            isAdmin()
              ? [
                  {
                    icon: "delete",
                    tooltip: "delete",
                    onClick: (e, rowData) => {
                      setDeleteRegistrationID(rowData.registrationId);
                      setDeleteName(rowData.section);
                    }
                  }
                ]
              : []
          }
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
