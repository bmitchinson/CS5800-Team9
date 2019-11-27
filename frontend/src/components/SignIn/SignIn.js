import "date-fns";
import React from "react";
import axios from "axios";
import DateFnsUtils from "@date-io/date-fns";
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker
} from "@material-ui/pickers";

import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import Link from "@material-ui/core/Link";
import Checkbox from "@material-ui/core/Checkbox";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Paper from "@material-ui/core/Paper";
import Grid from "@material-ui/core/Grid";
import SchoolIcon from "@material-ui/icons/School";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(theme => ({
  root: {
    height: "100vh"
  },
  image: {
    backgroundImage:
      "url(https://res.cloudinary.com/dheqbiqti/image/upload/q_auto/v1572551864/classroom/cap.png)",
    backgroundRepeat: "no-repeat",
    backgroundSize: "cover",
    backgroundPosition: "center"
  },
  paper: {
    margin: theme.spacing(8, 4),
    display: "flex",
    flexDirection: "column",
    alignItems: "center"
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.primary.main,
    color: "#000000"
  },
  form: {
    width: "100%", // Fix IE 11 issue.
    marginTop: theme.spacing(1)
  },
  submit: {
    margin: theme.spacing(3, 0, 2)
  }
}));

export default function SignIn(props) {
  const classes = useStyles();
  const [signUp, setSignUp] = React.useState(false);
  const [birthday, setBirthday] = React.useState(new Date());
  const [message, setMessage] = React.useState(null);
  const [studentCheck, setStudentCheck] = React.useState(true);
  const [instructorCheck, setInstructorCheck] = React.useState(false);

  const checkStudent = () => {
    setStudentCheck(true);
    setInstructorCheck(false);
  };

  const checkInstructor = () => {
    setStudentCheck(false);
    setInstructorCheck(true);
  };

  const handleDateChange = date => {
    setBirthday(date);
  };

  const openSignUp = () => {
    setSignUp(true);
  };

  const closeSignUp = () => {
    setSignUp(false);
  };

  const postSignUp = () => {
    setMessage("");
    let bd = birthday.toISOString().split("T")[0] + "T00:00:00";
    let accountType = studentCheck ? "student" : "instructor";
    axios({
      method: "post",
      url: "https://localhost:5001/api/" + accountType,
      data: {
        FirstName: document.getElementById("signupfirstname").value,
        LastName: document.getElementById("signuplastname").value,
        BirthDate: bd,
        Email: document.getElementById("signupemail").value,
        Password: document.getElementById("signuppassword").value
      }
    })
      .then(function() {
        setMessage("Account Created");
        setSignUp(false);
      })
      .catch(function(e) {
        setMessage("Error Creating Account");
        console.log("Error:", e);
      });
  };

  const postLogin = () => {
    setMessage("");
    axios({
      method: "post",
      url: "https://localhost:5001/api/token",
      data: {
        Email: document.getElementById("loginemail").value,
        Password: document.getElementById("loginpassword").value
      }
    })
      .then(function(response) {
        props.setUserJWT(response.data.token);
      })
      .catch(function(e) {
        setMessage("Error Logging In");
        console.log("Error:", e);
      });
  };

  return (
    <Grid container component="main" className={classes.root}>
      <Grid item xs={false} sm={4} md={7} className={classes.image} />
      <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          {message && (
            <Typography component="h2" variant="h5">
              {message}
            </Typography>
          )}
          <Avatar className={classes.avatar}>
            <SchoolIcon />
          </Avatar>
          {!signUp && (
            <>
              <Typography component="h1" variant="h5">
                Sign in
              </Typography>
              <form className={classes.form} noValidate>
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  id="loginemail"
                  label="Email Address"
                  name="loginemail"
                  autoComplete="email"
                  autoFocus
                />
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  name="loginpassword"
                  label="Password"
                  type="password"
                  id="loginpassword"
                  autoComplete="current-password"
                />
                <Button
                  fullWidth
                  variant="contained"
                  color="primary"
                  className={classes.submit}
                  onClick={postLogin}
                >
                  Sign In
                </Button>
                <Grid container>
                  <Grid item xs>
                    <Link href="#" variant="body2" color="textPrimary">
                      Forgot password?
                    </Link>
                  </Grid>
                  <Grid item>
                    <Link
                      href="#"
                      variant="body2"
                      color="textPrimary"
                      onClick={openSignUp}
                    >
                      {"Don't have an account? Sign Up"}
                    </Link>
                  </Grid>
                </Grid>
              </form>
            </>
          )}
          {signUp && (
            <>
              <Typography component="h1" variant="h5">
                Sign Up
              </Typography>
              <form className={classes.form} noValidate>
                <Grid container spacing={1}>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      id="signupfirstname"
                      label="First Name"
                      name="signupfirstname"
                      autoComplete="first name"
                      autoFocus
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      id="signuplastname"
                      label="Last Name"
                      name="signuplastname"
                      autoComplete="last name"
                    />
                  </Grid>
                </Grid>
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  id="signupemail"
                  label="Email Address"
                  name="signupemail"
                  autoComplete="email"
                />
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  name="signuppassword"
                  label="Password"
                  type="password"
                  id="signuppassword"
                  autoComplete="current-password"
                />
                <Grid container justify="space-around">
                  <Grid item xm={6} sm={4} style={{ paddingTop: "1.8em" }}>
                    <FormControlLabel
                      control={
                        <Checkbox
                          checked={studentCheck}
                          onChange={checkStudent}
                          value="studentCheck"
                          color="primary"
                        />
                      }
                      label="Student"
                    />
                  </Grid>
                  <Grid item xm={6} sm={4} style={{ paddingTop: "1.8em" }}>
                    <FormControlLabel
                      control={
                        <Checkbox
                          checked={instructorCheck}
                          onChange={checkInstructor}
                          value="instructorCheck"
                          color="primary"
                        />
                      }
                      label="Instructor"
                    />
                  </Grid>
                  <Grid item xs={12} sm={4}>
                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                      <KeyboardDatePicker
                        disableToolbar
                        variant="inline"
                        format="MM/dd/yyyy"
                        margin="normal"
                        id="birthday"
                        label="Date of birth"
                        value={birthday}
                        onChange={handleDateChange}
                        KeyboardButtonProps={{
                          "aria-label": "change date"
                        }}
                      />
                    </MuiPickersUtilsProvider>
                  </Grid>
                </Grid>
                <Button
                  fullWidth
                  variant="contained"
                  color="primary"
                  className={classes.submit}
                  onClick={postSignUp}
                >
                  Sign Up
                </Button>
                <Grid container>
                  <Grid item>
                    <Link
                      href="#"
                      variant="body2"
                      color="textPrimary"
                      onClick={closeSignUp}
                    >
                      {"Already have an account? Log In"}
                    </Link>
                  </Grid>
                </Grid>
              </form>
            </>
          )}
        </div>
      </Grid>
    </Grid>
  );
}
