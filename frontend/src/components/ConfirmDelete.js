import React from "react";
import axios from "axios";

import { store } from "react-notifications-component";
import notificationPrefs from "../helpers/notificationPrefs";
import getHeaders from "../helpers/getHeaders";

import { Modal, Typography, Grid, Button } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(theme => ({
  paper: {
    position: "absolute",
    width: "30em",
    marginLeft: "-15em",
    height: "12em",
    marginTop: "-6em",
    left: "50%",
    top: "50%",
    padding: "1.2em",
    paddingTop: "3em",
    backgroundColor: theme.palette.background.paper,
    border: "1px solid #000",
    boxShadow: theme.shadows[5]
  }
}));

export default function ConfirmDelete(props) {
  const classes = useStyles();
  const {
    deleteCourseID,
    // deleteStudentID,
    // deleteInstructorID,
    deleteRegistrationID,
    deleteName,
    refresh,
    closeWindow
  } = props;

  const handleDelete = () => {
    if (deleteCourseID) {
      deleteCourse();
    }
    if (deleteRegistrationID) {
      deleteRegistration();
    }
  };

  const deleteCourse = () => {
    closeWindow();
    axios({
      method: "delete",
      url: `https://localhost:5001/api/course/${deleteCourseID}`,
      headers: getHeaders()
    })
      .then(function(response) {
        refresh();
        store.addNotification(
          notificationPrefs(
            `Deleted ${deleteName}`,
            `${deleteName} has been sucessfully removed`,
            "success"
          )
        );
      })
      .catch(function(error) {
        console.log(error);
        store.addNotification(
          notificationPrefs(
            `Error deleting ${deleteName}`,
            "Please try again",
            "danger"
          )
        );
      });
  };

  const deleteRegistration = () => {
    closeWindow();
    axios({
      method: "delete",
      url: `https://localhost:5001/api/registration/${deleteRegistrationID}`,
      headers: getHeaders()
    })
      .then(function(response) {
        refresh();
        store.addNotification(
          notificationPrefs(
            `Deleted ${deleteName}`,
            `${deleteName} has been sucessfully removed`,
            "success"
          )
        );
      })
      .catch(function(error) {
        console.log(error);
        store.addNotification(
          notificationPrefs(
            `Error deleting ${deleteName}`,
            "Please try again",
            "danger"
          )
        );
      });
  };

  return (
    <Modal
      open={!!(deleteCourseID || deleteRegistrationID)}
      onClose={closeWindow}
    >
      <div className={classes.paper}>
        <Grid
          container
          spacing={3}
          direction="column"
          alignItems="center"
          justify="center"
        >
          <Grid item xs={12}>
            <Typography>
              Are you sure you'd like to delete: {deleteName}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Button style={{ margin: "1em" }} onClick={handleDelete}>
              Yes I'm Sure
            </Button>
            <Button
              style={{ margin: "1em" }}
              color="primary"
              variant="contained"
              onClick={closeWindow}
            >
              Nevermind
            </Button>
          </Grid>
        </Grid>
      </div>
    </Modal>
  );
}
