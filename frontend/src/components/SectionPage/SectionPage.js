import React, { useState, useEffect } from "react";
import axios from "axios";
import { store } from "react-notifications-component";

import notificationPrefs from "../../helpers/notificationPrefs";
import getHeaders from "../../helpers/getHeaders";
import { isAdmin } from "../../helpers/jwtHelpers";

import ConfirmDelete from "../../components/ConfirmDelete";

import { Typography, CircularProgress, Grid } from "@material-ui/core";

export default function SectionPage(props) {
  const [loading, setLoading] = useState(true);
  const { registrationid } = props.match.params;

  return (
    <>
      {loading && (
        <Grid
          container
          spacing={3}
          direction="column"
          alignItems="center"
          justify="center"
        >
          <Grid item style={{ marginTop: "12em" }}>
            <em style={{ fontSize: "2em" }}>Loading...</em>
          </Grid>
          <Grid item>
            <CircularProgress style={{ zoom: 2.5, marginTop: "1em" }} />
          </Grid>
        </Grid>
      )}
      {!loading && (
        <>
          <Typography variant="h2">📓 Course</Typography>
          <Typography>Course ID: {registrationid}</Typography>
        </>
      )}
    </>
  );
}
