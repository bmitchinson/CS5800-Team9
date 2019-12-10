import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";
import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";

import { isInstructor, isStudent } from "../../helpers/jwtHelpers";
import { Grid, Typography, Paper, Button, Badge } from "@material-ui/core";
import { getTitle, getThumbnailURL } from "../../helpers/cloudinaryHelpers";
import { CloudinaryButton } from "../Upload/CloudinaryButton";

import NotificationsActiveIcon from "@material-ui/icons/NotificationsActive";

export default function Document(props) {
  // const [submitModal, setSubmitModal] = useState(false);
  const [refresh, setRefresh] = useState(false);
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
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [refresh]);

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

  const postGrade = async () => {
    const submissionIdToGrade = ungradedSubmissions()[0].submissionId;
    await axios({
      method: "POST",
      url: "https://localhost:5001/api/submission/" + submissionIdToGrade,
      headers: getHeaders(),
      data: {
        grade: "A",
        submissionId: submissionIdToGrade
      }
    })
      .then(() => {
        store.addNotification(
          notificationPrefs("Assignment graded!", "Thanks", "success")
        );
        setRefresh(!refresh);
      })
      .catch(e => {
        console.log(e);
        store.addNotification(
          notificationPrefs(
            "Problem submitting grade",
            "Please try again",
            "danger"
          )
        );
      });
  };

  console.log("ungraded", ungradedSubmissions());
  return (
    <Grid item xs={3}>
      <Paper
        style={{
          display: "flex",
          position: "relative",
          flexDirection: "column",
          width: "100%",
          height: "15em",
          zIndex: 90
        }}
        elevation={4}
      >
        {docIsNotNote() && isStudent() && (
          <img
            alt="letter grade"
            width="50px"
            src={getGradeImg()}
            style={{ zIndex: 95, top: -20, left: -15, position: "absolute" }}
          />
        )}
        {docIsNotNote() && isInstructor() && ungradedSubmissions().length > 0 && (
          // <span
          //   role="img"
          //   aria-label="bell emoji"
          //   style={{
          //     zIndex: 95,
          //     top: -28,
          //     left: -19,
          //     position: "absolute",
          //     fontSize: "250%"
          //   }}
          // >
          //   🔔
          // </span>
          <div style={{ zoom: "140%" }}>
            <Badge
              color="primary"
              badgeContent={ungradedSubmissions().length}
              style={{
                zIndex: 95,
                top: -10,
                left: -10,
                position: "absolute"
              }}
            >
              <NotificationsActiveIcon />
            </Badge>
          </div>
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
          <a
            href={document.resourceLink}
            rel="noopener noreferrer"
            target="_blank"
          >
            <img
              alt="document preview"
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
                  <CloudinaryButton
                    buttonText={"Turn In"}
                    refresh={() => setRefresh(!refresh)}
                    documentId={document.documentId}
                  />
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
                  <Button
                    color="primary"
                    variant="contained"
                    onClick={postGrade}
                  >
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
