import React, { useState, useEffect } from "react";
import { isAdmin, isInstructor } from "../../helpers/jwtHelpers";
import { Grid, Typography, Paper } from "@material-ui/core";
import zIndex from "@material-ui/core/styles/zIndex";

export default function Document(props) {
  const { document } = props;

  return (
    <Grid item xs={3}>
      <Paper
        style={{
          display: "flex",
          flexDirection: "column",
          width: "100%",
          height: "15em",
          zIndex: 111
        }}
        elevation={4}
      >
        <div style={{ flexGrow: 1 }}></div>
        <Paper
          style={{
            height: "4em"
          }}
          elevation={2}
        >
          <Grid
            container
            spacing={3}
            direction="column"
            alignItems="center"
            justify="center"
          >
            <Typography style={{ marginTop: "1em" }}>
              {getTitle(document)}
            </Typography>
          </Grid>
        </Paper>
      </Paper>
    </Grid>
  );
}

const getTitle = doc => {
  return doc.resourceLink.split("/")[11].slice(0, -11);
};
