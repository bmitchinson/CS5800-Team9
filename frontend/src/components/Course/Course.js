import React from "react";

import {
  Typography,
  Paper,
  Grid,
  IconButton,
  Menu,
  MenuItem,
  makeStyles
} from "@material-ui/core";

import MoreVertIcon from "@material-ui/icons/MoreVert";

const useStyles = makeStyles(theme => ({
  mainview: {
    margin: "2%"
  }
}));

export default function Course(props) {
  const classes = useStyles();

  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);

  const handleClick = event => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const handleDelete = () => {
    setAnchorEl(null);
  };

  return (
    <Grid item xs={12}>
      <Paper>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={11}>
            <Typography style={{ textAlign: "left", paddingLeft: "1%" }}>
              {props.code}: {props.title}
            </Typography>
          </Grid>
          <Grid item xs={1}>
            <IconButton
              aria-label="more"
              aria-controls="long-menu"
              aria-haspopup="true"
              onClick={handleClick}
            >
              <MoreVertIcon />
            </IconButton>
            <Menu
              id="editmenu"
              anchorEl={anchorEl}
              keepMounted
              open={open}
              onClose={handleClose}
              PaperProps={{
                style: {
                  marginTop: "55px"
                }
              }}
            >
              <MenuItem key={"delete"} onClick={handleDelete}>
                Delete Course
              </MenuItem>
            </Menu>
          </Grid>
        </Grid>
      </Paper>
    </Grid>
  );
}
