import React, { useState } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";
import { getThumbnailURL } from "../../helpers/cloudinaryHelpers";

import { Typography, Grid, Button } from "@material-ui/core";

export default function SubmitDocument(props) {
  const { fileURL, close, documentId, refresh } = props;

  let headerText = "Confirm Submission:";
  let uploadButtonText = "Submit";

  const postSubmit = async () => {
    await axios({
      method: "POST",
      url: "https://localhost:5001/api/submission",
      headers: getHeaders(),
      data: {
        DocumentId: documentId,
        ResourceLink: fileURL
      }
    })
      .then(() => {
        store.addNotification(
          notificationPrefs("Assignment submitted!", "Good Work", "success")
        );
        refresh();
      })
      .catch(e => {
        console.log(e);
        store.addNotification(
          notificationPrefs("Problem submitting", "Please try again", "danger")
        );
      });
    close();
  };

  return (
    <>
      <Grid
        container
        spacing={3}
        direction="column"
        alignItems="center"
        justify="center"
      >
        <Grid item xs={12}>
          <Typography variant="h5">{headerText}</Typography>
        </Grid>
        <Grid item xs={12}>
          <center>
            <img
              style={{ width: "70%" }}
              src={getThumbnailURL(fileURL)}
              alt="Document Preview"
            />
          </center>
        </Grid>
        <Grid item xs={12}>
          <Button color="primary" variant="contained" onClick={postSubmit}>
            {uploadButtonText}
          </Button>
        </Grid>
      </Grid>
    </>
  );
}
