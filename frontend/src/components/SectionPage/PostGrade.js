import React, { useState } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";

import {
  Typography,
  Grid,
  Button,
  Paper,
  FormControlLabel,
  RadioGroup,
  Radio
} from "@material-ui/core";

export default function PostGrade(props) {
  const {
    submissionIdToGrade,
    submissionThumbnail,
    doctype,
    close,
    submissionUrl
  } = props;
  const [choosenGrade, setChoosenGrade] = useState("C");

  const postGrade = async () => {
    await axios({
      method: "POST",
      url: "https://localhost:5001/api/submission/" + submissionIdToGrade,
      headers: getHeaders(),
      data: {
        grade: choosenGrade,
        submissionId: submissionIdToGrade
      }
    })
      .then(() => {
        store.addNotification(
          notificationPrefs("Assignment graded!", "Thanks", "success")
        );
        close();
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

  const grade = [
    "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883408/classroom/grades/a_wcm6x0.png",
    "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/b_gkwr0r.png",
    "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/c_u0rqh2.png",
    "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/d_k0o7uh.png",
    "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575883409/classroom/grades/f_bb5uj1.png"
  ];

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
          <Typography variant="h5">Grading {doctype}</Typography>
        </Grid>
        <Grid item xs={12}>
          <a href={submissionUrl} rel="noopener noreferrer" target="_blank">
            <center>
              <Paper style={{ width: "70%" }} elevation={5}>
                <img
                  style={{ width: "70%" }}
                  src={submissionThumbnail}
                  alt="Document Preview"
                />
              </Paper>
            </center>
          </a>
        </Grid>
        <Grid item xs={12}>
          <div
            style={{
              display: "flex",
              width: "90%",
              paddingLeft: ".8em"
            }}
          >
            <img
              alt="letter grade"
              width="50px"
              src={grade[0]}
              style={{ marginRight: "1.7em" }}
            />
            <img
              alt="letter grade"
              width="50px"
              src={grade[1]}
              style={{ marginRight: "1.7em" }}
            />
            <img
              alt="letter grade"
              width="50px"
              src={grade[2]}
              style={{ marginRight: "1.7em" }}
            />
            <img
              alt="letter grade"
              width="50px"
              src={grade[3]}
              style={{ marginRight: "1.7em" }}
            />
            <img
              alt="letter grade"
              width="50px"
              src={grade[4]}
              style={{ marginRight: "1.7em" }}
            />
          </div>
          <RadioGroup
            name="gradegroup"
            value={choosenGrade}
            onChange={e => {
              setChoosenGrade(e.target.value);
            }}
            row
          >
            <FormControlLabel
              value="A"
              control={<Radio color="primary" />}
              label=""
              labelPlacement="top"
            />
            <FormControlLabel
              value="B"
              control={<Radio color="primary" />}
              label=""
              labelPlacement="top"
            />
            <FormControlLabel
              value="C"
              control={<Radio color="primary" />}
              label=""
              labelPlacement="top"
            />
            <FormControlLabel
              value="D"
              control={<Radio color="primary" />}
              label=""
              labelPlacement="top"
            />
            <FormControlLabel
              value="F"
              control={<Radio color="primary" />}
              label=""
              labelPlacement="top"
            />
          </RadioGroup>
        </Grid>
        <Grid item xs={12}>
          <Button
            variant="contained"
            color="primary"
            style={{ marginRight: "1em" }}
            onClick={postGrade}
          >
            Issue Grade
          </Button>
          <Button
            variant="contained"
            color="secondary"
            style={{ marginLeft: "1em" }}
            onClick={close}
          >
            Cancel
          </Button>
        </Grid>
        <Grid item xs={12}></Grid>
      </Grid>
    </>
  );
}
