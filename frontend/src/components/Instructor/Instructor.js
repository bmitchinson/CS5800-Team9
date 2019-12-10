import React from "react";
import axios from "axios";

import {
  Typography,
  Paper,
  Grid,
  IconButton,
  Menu,
  MenuItem
} from "@material-ui/core";

import MoreVertIcon from "@material-ui/icons/MoreVert";

export default function Instructor(props) {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);

  const { firstName, lastName, registrations, instructorId } = props.instructor;

  const handleClick = event => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const handleDelete = () => {
    setAnchorEl(null);
    axios({
      method: "delete",
      url: "https://localhost:5001/api/instructor",
      params: {
        id: instructorId
      }
    })
      .then(function(response) {
        console.log("res", response);
        props.refreshIndex();
      })
      .catch(function(error) {
        console.log("err", error);
      });
  };

  return (
    <Grid item xs={12}>
      <Paper>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={11}>
            <Typography
              variant={"body1"}
              style={{ textAlign: "left", paddingLeft: "1%" }}
            >
              {lastName}, {firstName}
            </Typography>
            { <Typography
              variant={"body2"}
              style={{ textAlign: "left", paddingLeft: "1%" }}
            >
              Courses: {registrations.length}
            </Typography> }
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
                Delete Instructor
              </MenuItem>
            </Menu>
          </Grid>
        </Grid>
      </Paper>
    </Grid>
  );
}
