import React, { useState, useEffect } from "react";
import { isAdmin, isInstructor, isStudent } from "../../helpers/jwtHelpers";
import { Grid, Typography, Paper, Button } from "@material-ui/core";
import { getTitle, getThumbnailURL } from "../../helpers/cloudinaryHelpers";
import { CloudinaryButton } from "../Upload/CloudinaryButton";

export default function Document(props) {
  const [submitModal, setSubmitModal] = useState(false);
  const { document } = props;

  const getActionAppropriatePadding = () => {
    return document.docType !== "Notes" ? "9em" : "4em";
  };

  return (
    <Grid item xs={3}>
      <Paper
        style={{
          display: "flex",
          flexDirection: "column",
          width: "100%",
          height: "15em",
          zIndex: 111,
          overflow: "hidden"
        }}
        elevation={4}
      >
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
                <CloudinaryButton buttonText={"Turn In"} />
              </Grid>
            )}
          </Grid>
        </Paper>
      </Paper>
    </Grid>
  );
}
