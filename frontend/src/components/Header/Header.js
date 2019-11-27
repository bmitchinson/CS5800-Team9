import React from "react";
import { Link } from "react-router-dom";

import { makeStyles } from "@material-ui/core/styles";

import MenuIcon from "@material-ui/icons/Menu";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import SchoolIcon from "@material-ui/icons/School";

import {
  AppBar,
  Toolbar,
  Typography,
  IconButton,
  Button,
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText
} from "@material-ui/core";

const drawerWidth = 240;

const useStyles = makeStyles(theme => ({
  grow: {
    flexGrow: 1
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1
  },
  drawer: {
    width: drawerWidth,
    position: "fixed",
    flexShrink: 0
  },
  drawerPaper: {
    width: drawerWidth
  },
  root: {
    flexGrow: 1
  },
  menuButton: {
    marginRight: theme.spacing(2)
  },
  link: {
    textDecoration: "none",
    color: "black"
  },
  toolbar: theme.mixins.toolbar
}));

export default function Header(props) {
  const classes = useStyles();
  const [drawerState, setDrawerState] = React.useState(false);

  const closeDrawer = () => {
    drawerState && setDrawerState(false);
  };

  const toggleDrawer = () => {
    setDrawerState(!drawerState);
  };

  return (
    <div className={classes.root}>
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar>
          <IconButton
            edge="start"
            className={classes.menuButton}
            color="inherit"
            aria-label="menu"
            onClick={toggleDrawer}
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
      <Drawer
        className={classes.drawer}
        open={drawerState}
        onClose={closeDrawer}
        classes={{
          paper: classes.drawerPaper
        }}
        style={{ zIndex: 100 }}
      >
        <div className={classes.toolbar} />
        <List>
          <Link to="/roles" className={classes.link}>
            <ListItem button key="roles" onClick={closeDrawer}>
              <ListItemIcon>
                <AccountCircleIcon />
              </ListItemIcon>
              <ListItemText primary={"Current Roles"} />
            </ListItem>
          </Link>
          <Link to="/students" className={classes.link}>
            <ListItem button key="students" onClick={closeDrawer}>
              <ListItemIcon>
                <SchoolIcon />
              </ListItemIcon>
              <ListItemText primary={"All Students"} />
            </ListItem>
          </Link>
        </List>
      </Drawer>
    </div>
  );
}
