import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";
import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";

import { isAdmin, isInstructor, isStudent } from "../../helpers/jwtHelpers";
import { Grid, Typography, Paper, Button } from "@material-ui/core";
import { getTitle, getThumbnailURL } from "../../helpers/cloudinaryHelpers";
import { CloudinaryButton } from "../Upload/CloudinaryButton";

export default function Document(props) {
  const [submitModal, setSubmitModal] = useState(false);
  const [submissions, setSubmissions] = useState([]);
  const { document } = props;
  const grade = {
    A:
      "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883408/classroom/grades/a_wcm6x0.png",
    B:
      "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/b_gkwr0r.png",
    C:
      "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/c_u0rqh2.png",
    D:
      "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/d_k0o7uh.png",
    F:
      "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/f_bb5uj1.png"
  };

  useEffect(() => {
    const fetchSubmissions = async () => {
      const submissions = await axios({
        method: "get",
        url: "https://localhost:5001/api/submission/" + document.documentId,
        headers: getHeaders()
      })
        .then(res => {
          return res.data;
        })
        .catch(e => {
          store.addNotification(
            notificationPrefs(
              "Error fetching submission info",
              "Please try again",
              "danger"
            )
          );
          return [];
        });
      setSubmissions(submissions);
    };
    if (document.docType !== "Notes") {
      fetchSubmissions();
    }
  }, []);

  const getActionAppropriatePadding = () => {
    return document.docType !== "Notes" ? "9em" : "4em";
  };

  const docIsNotNote = () => {
    return document.docType !== "Notes";
  };

  const getGradeImg = () => {
    if (submissions[0]) {
      return grade[submissions[0].grade];
    }
    return "";
  };

  const ungradedSubmissions = () => {
    return submissions.filter(submission => !submission.grade);
  };

  return (
    <Grid item xs={3}>
      <Paper
        style={{
          display: "flex",
          position: "relative",
          flexDirection: "column",
          width: "100%",
          height: "15em",
          zIndex: 111
        }}
        elevation={4}
      >
        {docIsNotNote() && isStudent() && (
          <img
            width="50px"
            src={getGradeImg()}
            style={{ zIndex: 112, top: -20, left: -15, position: "absolute" }}
          />
        )}
        {docIsNotNote() && isInstructor() && ungradedSubmissions().length > 0 && (
          <p
            style={{
              zIndex: 112,
              top: -50,
              left: -15,
              position: "absolute",
              fontSize: "250%"
            }}
          >
            🔔
          </p>
        )}
        <Paper
          style={{
            flexGrow: 1,
            marginTop: "8%",
            marginLeft: "20%",
            marginRight: "20%",
            maxHeight: "80%",
            overflow: "hidden"
          }}
          elevation={4}
        >
          <a href={document.resourceLink} target="_blank">
            <img
              src={getThumbnailURL(document.resourceLink)}
              style={{ width: "100%" }}
            />
          </a>
        </Paper>
        <Paper
          style={{
            height: getActionAppropriatePadding()
          }}
          elevation={2}
        >
          <Grid
            container
            spacing={0}
            direction="column"
            alignItems="center"
            justify="center"
          >
            <Grid item>
              <Typography style={{ marginTop: ".5em" }}>
                {getTitle(document)}
              </Typography>
            </Grid>
            {document.docType !== "Notes" && isStudent() && (
              <Grid item>
                {!submissions.length && (
                  <CloudinaryButton buttonText={"Turn In"} />
                )}
                {submissions.length !== 0 && (
                  <Button color="primary" variant="contained" disabled>
                    Submitted
                  </Button>
                )}
              </Grid>
            )}
            {document.docType !== "Notes" && isInstructor() && (
              <Grid item>
                {!ungradedSubmissions().length && (
                  <Button variant="contained" disabled>
                    All Graded
                  </Button>
                )}
                {ungradedSubmissions().length !== 0 && (
                  <Button color="primary" variant="contained">
                    Grade
                  </Button>
                )}
              </Grid>
            )}
          </Grid>
        </Paper>
      </Paper>
    </Grid>
  );
}
