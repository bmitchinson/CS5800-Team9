import React from "react";
import ReactNotification from "react-notifications-component";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { Typography, Grid, CssBaseline } from "@material-ui/core";
import { createMuiTheme, responsiveFontSizes } from "@material-ui/core/styles";
import { ThemeProvider } from "@material-ui/styles";
import "react-notifications-component/dist/theme.css";

import MainView from "./views/MainView";
import StudentIndex from "./views/StudentIndex/StudentIndex";
import InstructorIndex from "./views/InstructorIndex/InstructorIndex";
import CourseIndex from "./views/CourseIndex/CourseIndex";

import Header from "./components/Header/Header";
import SignIn from "./components/SignIn/SignIn";
import SectionPage from "./components/SectionPage/SectionPage";
import MyAccount from "./components/MyAccount/MyAccount";
import FourOFour from "./components/FourOFour";

import { isAdmin, isLoggedIn } from "./helpers/jwtHelpers";

let theme = createMuiTheme({
  palette: {
    primary: {
      main: "#FFCD00"
    },
    secondary: {
      main: "#FFFFFF"
    },
    background: {
      default: "#FFFDEB"
    }
  }
});
theme = responsiveFontSizes(theme);

const useStateWithLocalStorage = localStorageKey => {
  const [value, setValue] = React.useState(
    localStorage.getItem(localStorageKey) || ""
  );
  React.useEffect(() => {
    localStorage.setItem(localStorageKey, value);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [value]);
  return [value, setValue];
};

function App() {
  const [userJWT, setUserJWT] = useStateWithLocalStorage("userJWT");
  console.log("token:", userJWT);
  const clearJWT = () => {
    setUserJWT(null);
    window.location.replace("http://localhost:3000/");
  };

  return (
    <Router>
      <ReactNotification />
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {!isLoggedIn(userJWT) && (
          <SignIn
            setUserJWT={jwt => {
              setUserJWT(jwt);
            }}
          />
        )}
        {isLoggedIn(userJWT) && (
          <>
            <Header clearJWT={clearJWT} jwt={userJWT} />
            <MainView>
              <Switch>
                <Route path="/myaccount">
                  <MyAccount />
                </Route>
                {isAdmin() && (
                  <Route path="/students">
                    <StudentIndex />
                  </Route>
                )}
                {isAdmin() && (
                  <Route path="/instructors">
                    <InstructorIndex />
                  </Route>
                )}
                <Route
                  path="/courses"
                  render={props => <CourseIndex {...props} />}
                />
                <Route
                  path="/section/:registrationid"
                  render={props => <SectionPage {...props} />}
                />
                <Route exact path="/">
                  <Grid
                    container
                    spacing={3}
                    direction="column"
                    alignItems="center"
                    justify="center"
                  >
                    <Typography variant="h2" style={{ marginTop: "2em" }}>
                      Welcome to Classroom™
                    </Typography>
                  </Grid>
                </Route>
                <Route>
                  <FourOFour />
                </Route>
              </Switch>
            </MainView>
          </>
        )}
      </ThemeProvider>
    </Router>
  );
}

export default App;
