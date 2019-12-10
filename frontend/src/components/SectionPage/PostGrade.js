import React from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";
import { getThumbnailURL } from "../../helpers/cloudinaryHelpers";

import { Typography, Grid, Button } from "@material-ui/core";

export default function PostGrade(props) {
  const { fileURL, close, documentId, refresh } = props;

  let headerText = "Confirm Submission:";
  let uploadButtonText = "Submit";

  // const postGrade = async () => {
  //   const submissionIdToGrade = ungradedSubmissions()[0].submissionId;
  //   await axios({
  //     method: "POST",
  //     url: "https://localhost:5001/api/submission/" + submissionIdToGrade,
  //     headers: getHeaders(),
  //     data: {
  //       grade: "A",
  //       submissionId: submissionIdToGrade
  //     }
  //   })
  //     .then(() => {
  //       store.addNotification(
  //         notificationPrefs("Assignment graded!", "Thanks", "success")
  //       );
  //       setRefresh(!refresh);
  //     })
  //     .catch(e => {
  //       console.log(e);
  //       store.addNotification(
  //         notificationPrefs(
  //           "Problem submitting grade",
  //           "Please try again",
  //           "danger"
  //         )
  //       );
  //     });
  // };

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
          <Typography variant="h5">hey :)</Typography>
        </Grid>
      </Grid>
    </>
  );
}
