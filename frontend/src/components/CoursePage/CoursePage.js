import React from "react";

import { Typography } from "@material-ui/core";

export default function CoursePage(props) {
  const { courseid } = props.match.params;
  return (
    <>
      <Typography variant="h2">Course Page</Typography>
      <Typography>Course ID: {courseid}</Typography>
    </>
  );
}
