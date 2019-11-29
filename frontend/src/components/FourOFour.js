import React from "react";

import { Typography, Grid } from "@material-ui/core";
import { Link } from "react-router-dom";

export default function FourOFour() {
  return (
    <Grid
      container
      spacing={3}
      direction="column"
      alignItems="center"
      justify="center"
    >
      <Grid item xs={6}>
        <Typography variant="h2">
          404{" "}
          <span role="img" aria-label="sad">
            😢
          </span>
        </Typography>
      </Grid>
      <Grid item xs={6}>
        <Typography>
          Maybe this page exists but you don't have permission
        </Typography>
      </Grid>
      <Grid item xs={6}>
        <img
          style={{ height: "20em" }}
          alt="raman"
          src="http://homepage.cs.uiowa.edu/~aravamud/images/AravamudhanRaman.jpg"
        />
      </Grid>
      <Grid item xs={12}>
        <Link to="/">Return Home</Link>
      </Grid>
    </Grid>
  );
}
