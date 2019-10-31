import React from "react";

import { makeStyles } from "@material-ui/core/styles";
import MenuIcon from "@material-ui/icons/Menu";

import {
  AppBar,
  Toolbar,
  Typography,
  IconButton,
  Button
} from "@material-ui/core";

const useStyles = makeStyles(theme => ({
  grow: {
    flexGrow: 1
  },
  root: {
    flexGrow: 1
  },
  menuButton: {
    marginRight: theme.spacing(2)
  }
}));

export default function Header(props) {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            edge="start"
            className={classes.menuButton}
            color="inherit"
            aria-label="menu"
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6">Classroom</Typography>
          <div className={classes.grow} />
          <Button variant="contained" onClick={props.clearJWT}>
            Sign Out
          </Button>
        </Toolbar>
      </AppBar>
    </div>
  );
}
