import React from "react";
import axios from "axios";

import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import Link from "@material-ui/core/Link";
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

  const openSignUp = () => {
    setSignUp(true);
  }; 
  
  const postSignUp = () => {
    axios({
      method: "post",
      url: "http://localhost:5000/api/student",
      data: {
        FirstName: document.getElementById("firstname").value,
        LastName: document.getElementById("lastname").value,
        BirthDate: "2000-1-1T00:00:00",
        Email: document.getElementById("signUpEmail").value,
        Password: document.getElementById("signUpPassword").value
      }
    })
      .then(function() {
        setSignUp(false)
      })
      .catch(function(error) {
        // TODO: "Bad username / password message"
      });
  };
  
  const postLogin = () => {
    
    axios({
      method: "post",
      url: "https://localhost:5001/api/token",
      data: {
        Email: document.getElementById("email").value,
        Password: document.getElementById("password").value
      }
    })
      .then(function(response) {
        props.setUserJWT(response.data.token);
      })
      .catch(function(e) {
        console.log(e);
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
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                  autoFocus
                />
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
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
                    <Link href="#" variant="body2" color="textPrimary" onClick={openSignUp}>
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
              <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  id="firstname"
                  label="First Name"
                  name="first name"
                  autoComplete="first name"
                  autoFocus
                />
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  id="lastname"
                  label="Last Name"
                  name="last name"
                  autoComplete="last name"
                />
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  id="signUpEmail"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                />
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="signUpPassword"
                  autoComplete="current-password"
                />
                <Typography component="h2" variant="subtitle1" >
                  Date of birth
                </Typography>
                <Grid container spacing={1}>
                  <Grid item xs>
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      name="month"
                      label="Month"
                      id="month"
                      autoComplete="month"
                    />
                  </Grid>
                  <Grid item xs>
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      name="day"
                      label="Day"
                      id="day"
                      autoComplete="day"
                    />
                  </Grid>
                  <Grid item xs>
                    <TextField
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      name="year"
                      label="Year"
                      id="year"
                      autoComplete="year"
                    />
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
              </form>
            </>
          )}
        </div>
      </Grid>
    </Grid>
  );
}