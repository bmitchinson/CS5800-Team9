import React, { useState } from "react";

import { Modal } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(theme => ({
  paper: {
    position: "absolute",
    width: "30em",
    marginLeft: "-15em",
    height: "40em",
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

export default function ConfirmDelete(props) {
  const classes = useStyles();
  const { deleteCourseID, closeWindow } = props;

  return (
    <Modal open={deleteCourseID !== null} onClose={closeWindow}>
      <div className={classes.paper}>Delete!{deleteCourseID}</div>
    </Modal>
  );
}
