import React, { useState, useEffect } from "react";
import axios from "axios";
import { isAdmin, isInstructor } from "../../helpers/jwtHelpers";
import { makeStyles } from "@material-ui/core/styles";
import { Typography, Grid, Paper } from "@material-ui/core";

const useStyles = makeStyles({
  root: {
    flexGrow: 1,
    marginTop: "1em",
    paddingLeft: "1em"
  }
});

export default function DocumentGroup(props) {
  const classes = useStyles();

  const { tabValue, index } = props;

  const activeTab = tabValue === index;

  var doctype = "";

  switch (tabValue) {
    case 0:
      doctype = "Notes:";
      break;
    case 1:
      doctype = "Assignments:";
      break;
    case 2:
      doctype = "Exams:";
      break;
    case 3:
      doctype = "Quizes:";
      break;
  }

  return (
    <>
      {activeTab && (
        <Paper className={classes.root} elevation={0}>
          <Grid container spacing={4}>
            <Grid item xs={12}>
              <Typography variant="h4">{doctype}</Typography>
            </Grid>
          </Grid>
        </Paper>
      )}
      {!activeTab && <></>}
    </>
  );
}
