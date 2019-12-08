import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";
import { isAdmin, isInstructor } from "../../helpers/jwtHelpers";

import DocumentGroup from "./DocumentGroup";
import CloudinaryButton from "../Upload/CloudinaryButton";

import { makeStyles } from "@material-ui/core/styles";
import {
  Typography,
  CircularProgress,
  Grid,
  Tabs,
  Tab,
  Paper
} from "@material-ui/core";

const useStyles = makeStyles({
  root: {
    flexGrow: 1
  }
});

export default function SectionPage(props) {
  const classes = useStyles();
  const [tabValue, setTabValue] = React.useState(0);
  const [regInfo, setRegInfo] = useState(null);
  const [documents, setDocuments] = useState(null);

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
  }, [registrationid]);

  const havedata = regInfo && documents;

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
          <div style={{ width: "100%", display: "flex" }}>
            <Typography
              variant="h2"
              style={{ marginBottom: ".5em", width: "80%" }}
            >
              <span role="img" aria-label="notebook emoji">
                📓
              </span>
              Materials for {regInfo[0].section}
            </Typography>
            <div style={{ flexGrow: 1 }} />
            {isAdmin() && (
              <div style={{ maxHeight: "1em", marginTop: ".4em" }}>
                <CloudinaryButton style={{ maxWidth: ".6em" }} />
              </div>
            )}
          </div>
          <Paper className={classes.root}>
            <Tabs
              value={tabValue}
              onChange={(e, val) => {
                setTabValue(val);
              }}
              indicatorColor="primary"
              textColor="primary"
              centered
            >
              <Tab label="Notes" />
              <Tab label="Assignments" />
              <Tab label="Exam" />
              <Tab label="Quiz" />
            </Tabs>
            <DocumentGroup tabValue={tabValue} index={0} />
            <DocumentGroup tabValue={tabValue} index={1} />
            <DocumentGroup tabValue={tabValue} index={2} />
            <DocumentGroup tabValue={tabValue} index={3} />
          </Paper>
        </>
      )}
    </>
  );
}
