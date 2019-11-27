import React from "react";
import { Link } from "react-router-dom";

import { makeStyles } from "@material-ui/core/styles";

import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import SchoolIcon from "@material-ui/icons/School";

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

export default function InstructorLinks(props) {
  const classes = useStyles();
  // ["code", "name", "id"]
  let courses = [["ECE:4000", "Circuits", 3], ["CS:3524", "Fundamentals", 2]];

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
      <ListItem>
        <ListItemText primary={"My Courses:"} />
      </ListItem>
      {courses.map(c => (
        <Link to={`/course/${c[2]}`} className={classes.link}>
          <ListItem button key={c[0]} onClick={props.closeDrawer}>
            <ListItemIcon>
              <SchoolIcon />
            </ListItemIcon>
            <ListItemText
              primary={`${c[0]} - ${c[1]}`.substring(0, 15) + "..."}
            />
          </ListItem>
        </Link>
      ))}
    </List>
  );
}
