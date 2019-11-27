import React from "react";

import { makeStyles } from "@material-ui/core/styles";
import MenuIcon from "@material-ui/icons/Menu";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";

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
          <ListItem button key="roles">
            <ListItemIcon>
              <AccountCircleIcon />
            </ListItemIcon>
            <ListItemText primary={"roles"} />
          </ListItem>
        </List>
      </Drawer>
    </div>
  );
}
