import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { createMuiTheme, responsiveFontSizes } from "@material-ui/core/styles";
import { ThemeProvider } from "@material-ui/styles";

import CssBaseline from "@material-ui/core/CssBaseline";

import MainView from "./views/MainView";
import StudentIndex from "./views/StudentIndex/StudentIndex";
import InstructorIndex from "./views/InstructorIndex/InstructorIndex";
import CourseIndex from "./views/CourseIndex/CourseIndex";

import Header from "./components/Header/Header";
import SignIn from "./components/SignIn/SignIn";
import CoursePage from "./components/CoursePage/CoursePage";
import MyAccount from "./components/MyAccount/MyAccount";
import CloudinaryButton from "./components/Upload/CloudinaryButton";
import FourOFour from "./components/FourOFour";

import { Typography } from "@material-ui/core";

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

function getRoleAndEmailFromJWT(token) {
  if (!token) {
    return "";
  } else {
    var base64Url = token.split(".")[1];
    if (base64Url) {
      var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      var jsonPayload = decodeURIComponent(
        atob(base64)
          .split("")
          .map(function(c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join("")
      );
      return [JSON.parse(jsonPayload).roles[0], JSON.parse(jsonPayload).email];
    } else {
      return "";
    }
  }
}

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
  const [role, email] = getRoleAndEmailFromJWT(userJWT);
  // console.log("token:", userJWT);
  const clearJWT = () => {
    setUserJWT(null);
    window.location.replace("http://localhost:3000/");
  };

  return (
    <Router>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {(userJWT === "null" || !userJWT) && <SignIn setUserJWT={setUserJWT} />}
        {userJWT !== "null" && userJWT && (
          <>
            <Header clearJWT={clearJWT} role={role} email={email} />
            <MainView>
              <Switch>
                <Route path="/upload">
                  <CloudinaryButton />
                </Route>
                <Route path="/myaccount">
                  <MyAccount />
                </Route>
                {role === "Admin" && (
                  <Route path="/students">
                    <StudentIndex />
                  </Route>
                )}
                {role === "Admin" && (
                  <Route path="/instructors">
                    <InstructorIndex />
                  </Route>
                )}
                <Route path="/courses">
                  <CourseIndex isAdmin={role === "Admin"} />
                </Route>
                <Route
                  path="/course/:courseid"
                  render={props => (
                    <CoursePage {...props} isAdmin={role === "Admin"} />
                  )}
                />
                <Route exact path="/">
                  <Typography variant="h2">Welcome to Classroom™</Typography>
                  <p>
                    What should go here? Maybe we just take them to their first
                    course
                  </p>
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
