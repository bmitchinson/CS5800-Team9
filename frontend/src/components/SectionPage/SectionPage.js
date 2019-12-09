import React from "react";

import { Typography } from "@material-ui/core";

export default function SectionPage(props) {
  const { registrationid } = props.match.params;
  return (
    <>
      <Typography variant="h2">Course Section Page</Typography>
      <Typography>Course ID: {registrationid}</Typography>
    </>
  );
}
