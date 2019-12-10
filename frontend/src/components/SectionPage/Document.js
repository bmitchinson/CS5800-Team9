import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";
import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";

import PostGrade from "./PostGrade";

import { makeStyles } from "@material-ui/core/styles";

import { isInstructor, isStudent } from "../../helpers/jwtHelpers";
import {
  Grid,
  Typography,
  Paper,
  Button,
  Badge,
  Modal
} from "@material-ui/core";
import { getTitle, getThumbnailURL } from "../../helpers/cloudinaryHelpers";
import { CloudinaryButton } from "../Upload/CloudinaryButton";

import NotificationsActiveIcon from "@material-ui/icons/NotificationsActive";

const useStyles = makeStyles(theme => ({
  paper: {
    position: "absolute",
    width: "30em",
    marginLeft: "-15em",
    height: "38em",
    marginTop: "-20em",
    left: "50%",
    top: "50%",
    padding: "1.2em",
    paddingTop: "3em",
    backgroundColor: theme.palette.background.paper,
    border: "1px solid #000",
    boxShadow: theme.shadows[5]
  }
}));

export default function Document(props) {
  const classes = useStyles();
  const [showPostGrade, setShowPostGrade] = useState(false);
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

  return (
    <>
      {showPostGrade && (
        <Modal
          open={showPostGrade}
          onClose={() => {
            setShowPostGrade(false);
          }}
        >
          <div className={classes.paper}>
            <PostGrade
              close={() => {
                setShowPostGrade(false);
                setRefresh(!refresh);
              }}
              submissionIdToGrade={
                ungradedSubmissions().length
                  ? ungradedSubmissions()[0].submissionId
                  : null
              }
              sumbissionUrl={ungradedSubmissions()[0].resourceLink}
              submissionThumbnail={getThumbnailURL(
                ungradedSubmissions()[0].resourceLink
              )}
              doctype={document.docType}
            />
          </div>
        </Modal>
      )}
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
          {docIsNotNote() && isStudent() && getGradeImg() && (
            <img
              alt="letter grade"
              width="50px"
              src={getGradeImg()}
              style={{ zIndex: 95, top: -20, left: -15, position: "absolute" }}
            />
          )}
          {docIsNotNote() &&
            isInstructor() &&
            ungradedSubmissions().length > 0 && (
              <div style={{ zoom: "140%" }}>
                <Badge
                  color="primary"
                  badgeContent={ungradedSubmissions().length}
                  style={{
                    zIndex: 95,
                    top: -9,
                    left: -7,
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
                      onClick={() => setShowPostGrade(true)}
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
    </>
  );
}
