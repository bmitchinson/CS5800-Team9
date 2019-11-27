import React from "react";
import { Link } from "react-router-dom";

import { makeStyles } from "@material-ui/core/styles";

import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import RecordVoiceOverIcon from "@material-ui/icons/RecordVoiceOver";

import { List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
  link: {
    textDecoration: "none",
    color: "black"
  }
}));

export default function InstructorLinks(props) {
  const classes = useStyles();

  return (
    <List>
      <Link to="/roles" className={classes.link}>
        <ListItem button key="roles" onClick={props.closeDrawer}>
          <ListItemIcon>
            <AccountCircleIcon />
          </ListItemIcon>
          <ListItemText primary={"Current Roles"} />
        </ListItem>
      </Link>
      <Link to="/instructors" className={classes.link}>
        <ListItem button key="instructors" onClick={props.closeDrawer}>
          <ListItemIcon>
            <RecordVoiceOverIcon />
          </ListItemIcon>
          <ListItemText primary={"All Instructors"} />
        </ListItem>
      </Link>
    </List>
  );
}
