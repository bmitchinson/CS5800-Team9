import React, { useState, useEffect } from "react";
import { isAdmin, isInstructor } from "../../helpers/jwtHelpers";
import { Grid, Typography } from "@material-ui/core";

export default function Document(props) {
  return (
    <Grid item xs={3}>
      <Typography>Doc</Typography>
    </Grid>
  );
}
