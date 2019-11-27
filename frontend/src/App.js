import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { createMuiTheme, responsiveFontSizes } from "@material-ui/core/styles";
import { ThemeProvider } from "@material-ui/styles";

import CssBaseline from "@material-ui/core/CssBaseline";

import MainView from "./views/MainView";
import StudentIndex from "./views/StudentIndex/StudentIndex";

import Header from "./components/Header/Header";
import SignIn from "./components/SignIn/SignIn";

let theme = createMuiTheme({
  palette: {
    primary: {
      main: "#FFCD00"
    },
    background: {
      default: "#FFFDEB"
    }
  }
});
theme = responsiveFontSizes(theme);

function getRoleFromJWT(token) {
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
      return JSON.parse(jsonPayload).roles[0];
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
  return [value, setValue, getRoleFromJWT(value)];
};

function App() {
  const [userJWT, setUserJWT, role] = useStateWithLocalStorage("userJWT");
  const clearJWT = () => {
    setUserJWT(null);
  };

  return (
    <Router>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {(userJWT === "null" || !userJWT) && <SignIn setUserJWT={setUserJWT} />}
        {userJWT !== "null" && userJWT && (
          <>
            <Header clearJWT={clearJWT} role={role} />
            <MainView>
              <Switch>
                <Route path="/students">
                  <StudentIndex />
                </Route>
                <Route path="/roles">
                  <h2>Signed in with role: {role}</h2>
                </Route>
                <Route>
                  <h2>Home Page</h2>
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
