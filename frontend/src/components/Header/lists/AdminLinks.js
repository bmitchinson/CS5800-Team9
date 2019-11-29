import React from "react";
import { Link } from "react-router-dom";

import { makeStyles } from "@material-ui/core/styles";

import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import SchoolIcon from "@material-ui/icons/School";
import RecordVoiceOverIcon from "@material-ui/icons/RecordVoiceOver";
import PersonIcon from "@material-ui/icons/Person";

import {
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Divider
} from "@material-ui/core";

const useStyles = makeStyles(theme => ({
  link: {
    textDecoration: "none",
    color: "black"
  }
}));

export default function AdminLinks(props) {
  const classes = useStyles();

  return (
    <List>
      <Link to="/myaccount" className={classes.link}>
        <ListItem button onClick={props.closeDrawer}>
          <ListItemIcon>
            <AccountCircleIcon />
          </ListItemIcon>
          <ListItemText primary={"My Account"} />
        </ListItem>
      </Link>
      <Divider />
      <Link to="/students" className={classes.link}>
        <ListItem button key="students" onClick={props.closeDrawer}>
          <ListItemIcon>
            <PersonIcon />
          </ListItemIcon>
          <ListItemText primary={"All Students"} />
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
      <Link to="/courses" className={classes.link}>
        <ListItem button key="courses" onClick={props.closeDrawer}>
          <ListItemIcon>
            <SchoolIcon />
          </ListItemIcon>
          <ListItemText primary={"All Courses"} />
        </ListItem>
      </Link>
      <Divider />
    </List>
  );
}
