import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";
import { isAdmin } from "../../helpers/jwtHelpers";

import ConfirmDelete from "../../components/ConfirmDelete";

import { Typography, CircularProgress, Grid } from "@material-ui/core";
import { bool } from "prop-types";

export default function SectionPage(props) {
  const [regInfo, setRegInfo] = useState([]);
  const [documents, setDocuments] = useState([]);
  const [deleteName, setDeleteName] = useState("");

  const { registrationid } = props.match.params;

  useEffect(() => {
    const fetchRegistration = async () => {
      const registrationInfo = await axios({
        method: "get",
        url: "https://localhost:5001/api/registration/" + registrationid,
        headers: getHeaders()
      })
        .then(response => {
          return response.data;
        })
        .catch(e => {
          store.addNotification(
            notificationPrefs(
              "Error fetching section info",
              "Please try again",
              "danger"
            )
          );
          return [];
        });
      setRegInfo(registrationInfo);
    };

    const fetchDocuments = async () => {
      const documents = await axios({
        method: "get",
        url: "https://localhost:5001/api/document/" + registrationid,
        headers: getHeaders()
      })
        .then(response => {
          return response.data;
        })
        .catch(e => {
          store.addNotification(
            notificationPrefs(
              "Error fetching documents",
              "Please try again",
              "danger"
            )
          );
          return [];
        });
      setDocuments(documents);
    };
    fetchDocuments();
    fetchRegistration();
  }, []);

  console.log("regInfo:", regInfo.length);
  console.log("documents:", documents.length);

  const havedata = regInfo.length && documents.length;

  return (
    <>
      {!havedata && (
        <Grid
          container
          spacing={3}
          direction="column"
          alignItems="center"
          justify="center"
        >
          <Grid item style={{ marginTop: "12em" }}>
            <em style={{ fontSize: "2em" }}>Loading...</em>
          </Grid>
          <Grid item>
            <CircularProgress style={{ zoom: 2.5, marginTop: "1em" }} />
          </Grid>
        </Grid>
      )}
      {havedata && (
        <>
          <Typography variant="h2">📓 Course</Typography>
          <Typography>Course ID: {registrationid}</Typography>
          <p>got data :)</p>
        </>
      )}
    </>
  );
}
