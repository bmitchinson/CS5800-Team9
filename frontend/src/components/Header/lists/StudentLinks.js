import React from "react";
import { Link } from "react-router-dom";

import { makeStyles } from "@material-ui/core/styles";

import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import SchoolIcon from "@material-ui/icons/School";

import { List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
  link: {
    textDecoration: "none",
    color: "black"
  }
}));

export default function StudentLinks(props) {
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
      <Link to="/students" className={classes.link}>
        <ListItem button key="students" onClick={props.closeDrawer}>
          <ListItemIcon>
            <SchoolIcon />
          </ListItemIcon>
          <ListItemText primary={"All Students"} />
        </ListItem>
      </Link>
    </List>
  );
}
