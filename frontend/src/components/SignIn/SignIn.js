import React from "react";
import "date-fns";
import { Switch, Route } from "react-router-dom";
import DateFnsUtils from "@date-io/date-fns";
import axios from "axios";
import { store } from "react-notifications-component";

import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker
} from "@material-ui/pickers";

import {
  Avatar,
  Button,
  TextField,
  Link,
  Checkbox,
  FormControlLabel,
  Paper,
  Grid,
  Typography
} from "@material-ui/core";

import notificationPrefs from "../../helpers/notificationPrefs";

import SchoolIcon from "@material-ui/icons/School";
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
  const { setUserJWT } = props;

  const classes = useStyles();
  const [signUp, setSignUp] = React.useState(false);
  const [forgotPassword, setForgotPassword] = React.useState(false);
  const [birthday, setBirthday] = React.useState(new Date());
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

  const openForgotPassword = () => {
    setForgotPassword(true);
  };

  const closeForgotPassword = () => {
    setForgotPassword(false);
  };

  const postSignUp = () => {
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
        sendConfirmationEmail();
        setSignUp(false);
        store.addNotification(
          notificationPrefs("Account created!", "Login Now", "success")
        );
      })
      .catch(function(e) {
        store.addNotification(
          notificationPrefs(
            "Problem creating account",
            "Please try again",
            "danger"
          )
        );
        console.log("Error:", e);
      });
  };

  const postLogin = () => {
    axios({
      method: "post",
      url: "https://localhost:5001/api/token",
      data: {
        Email: document.getElementById("loginemail").value,
        Password: document.getElementById("loginpassword").value
      }
    })
      .then(function(response) {
        setUserJWT(response.data.token);
      })
      .catch(function(e) {
        console.log(e);
        store.addNotification(
          notificationPrefs(
            "Invalid Login",
            "Please check your credentials or create a new account",
            "danger"
          )
        );
      });
  };

  const sendConfirmationEmail = () => {
    axios({
      method: "post",
      url: "https://localhost:5001/api/email/sendconfirmation",
      data: {
        Email: document.getElementById("signupemail").value,
        Password: document.getElementById("signuppassword").value
      }
    })
      .then(function(response) {
        store.addNotification(
          notificationPrefs(
            "Email confirmation sent!",
            "Please check your email and confirm your email address",
            "success"
          )
        );
      })
      .catch(function(e) {
        console.log("Error:", e);
        store.addNotification(
          notificationPrefs("Error sending email", "Please try again", "danger")
        );
      });
  };

  const sendForgotPasswordEmail = () => {
    axios({
      method: "post",
      url: "https://localhost:5001/api/email/forgotpassword",
      data: {
        Email: document.getElementById("resetpasswordemail").value,
        ResetPassword: document.getElementById("resetpasswordpassword").value
      }
    })
      .then(function(response) {
        setForgotPassword(false);
        store.addNotification(
          notificationPrefs(
            "Email sent!",
            "Please check your email to reset your password",
            "success"
          )
        );
      })
      .catch(function(e) {
        console.log("Error:", e);
        store.addNotification(
          notificationPrefs("Error sending email", "Please try again", "danger")
        );
      });
  };

  return (
    <Grid container component="main" className={classes.root}>
      <Grid item xs={false} sm={4} md={7} className={classes.image} />
      <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <SchoolIcon />
          </Avatar>
          <Switch>
            <Route path="/confirm">
              <Typography component="h1" variant="h5">
                Email Confirmed
              </Typography>
              <Grid container>
                <Grid item>
                  <Link href="/#" variant="body2" color="textPrimary">
                    {"Click here to Sign in"}
                  </Link>
                </Grid>
              </Grid>
            </Route>
            <Route path="/resetpassword">
              <Typography component="h1" variant="h5">
                Password Reset
              </Typography>
              <Grid container>
                <Grid item>
                  <Link href="/#" variant="body2" color="textPrimary">
                    {"Click here to Sign in"}
                  </Link>
                </Grid>
              </Grid>
            </Route>
            <Route path="/cancelreset">
              <Typography component="h1" variant="h5">
                Password Reset Cancelled
              </Typography>
              <Grid container>
                <Grid item>
                  <Link href="/#" variant="body2" color="textPrimary">
                    {"Click here to Sign in"}
                  </Link>
                </Grid>
              </Grid>
            </Route>
            <Route path="/error">
              <Typography component="h1" variant="h5">
                Error
              </Typography>
              <Grid container>
                <Grid item>
                  <Link href="/#" variant="body2" color="textPrimary">
                    {"An error occurred. Click here to Sign in"}
                  </Link>
                </Grid>
              </Grid>
            </Route>
            <Route exact path="/">
              {!signUp && !forgotPassword && (
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
                      <Grid item xs={12} sm={6}>
                        <Link
                          href="#"
                          variant="body2"
                          color="textPrimary"
                          onClick={openSignUp}
                        >
                          {"Don't have an account? Sign Up"}
                        </Link>
                      </Grid>
                      <Grid item xs={12} sm={6}>
                        <Link
                          href="#"
                          variant="body2"
                          color="textPrimary"
                          alignitems="flex-end"
                          onClick={openForgotPassword}
                        >
                          {"Forgot your password?"}
                        </Link>
                      </Grid>
                    </Grid>
                  </form>
                </>
              )}
              {signUp && !forgotPassword && (
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
              {forgotPassword && (
                <>
                  <Typography component="h1" variant="h5">
                    Forgot Password
                  </Typography>
                  <form className={classes.form} noValidate>
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      id="resetpasswordemail"
                      label="Email Address"
                      name="resetpasswordemail"
                      autoComplete="email"
                      autoFocus
                    />
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      id="resetpasswordpassword"
                      label="New Password"
                      name="resetpasswordpassword"
                      type="password"
                      autoComplete="resetpassword"
                    />

                    <Button
                      fullWidth
                      variant="contained"
                      color="primary"
                      className={classes.submit}
                      onClick={sendForgotPasswordEmail}
                    >
                      Send Email
                    </Button>
                    <Grid container>
                      <Grid item>
                        <Link
                          href="#"
                          variant="body2"
                          color="textPrimary"
                          onClick={closeForgotPassword}
                        >
                          {"Back to sign up"}
                        </Link>
                      </Grid>
                    </Grid>
                  </form>
                </>
              )}
            </Route>
          </Switch>
        </div>
      </Grid>
    </Grid>
  );
}
