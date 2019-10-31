import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { createMuiTheme, responsiveFontSizes } from "@material-ui/core/styles";
import { ThemeProvider } from "@material-ui/styles";

import CssBaseline from "@material-ui/core/CssBaseline";

import MainView from "./views/MainView";
import StudentIndex from "./views/StudentIndex/StudentIndex";

import Header from "./components/Header/Header";
import SignIn from "./components/SignIn/SignIn";

// include this line if you'd like to clear JWT data
// remove if you'd like sign in to persist
// localStorage.removeItem("userJWT");

let theme = createMuiTheme({
  palette: {
    primary: {
      main: "#FFF796"
    },
    background: {
      default: "#FFFDEB"
    }
  }
});
theme = responsiveFontSizes(theme);

function getRolesFromJwt(token) {
  if (!token) {
    return [];
  }

  var base64Url = token.split(".")[1];
  var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
  var jsonPayload = decodeURIComponent(
    atob(base64)
      .split("")
      .map(function(c) {
        return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join("")
  );

  return JSON.parse(jsonPayload).roles;
}

const useStateWithLocalStorage = localStorageKey => {
  const [value, setValue] = React.useState(
    localStorage.getItem(localStorageKey) || ""
  );
  React.useEffect(() => {
    localStorage.setItem(localStorageKey, value);
  }, [value]);
  return [value, setValue, getRolesFromJwt(value)];
};

function App() {
  const [userJWT, setUserJWT, roles] = useStateWithLocalStorage("userJWT");
  const clearJWT = () => {
    setUserJWT(null);
  };

  return (
    <Router>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {!userJWT && <SignIn setUserJWT={setUserJWT} />}
        {userJWT && (
          <>
            <Header clearJWT={clearJWT} />
            <MainView>
              <Switch>
                <Route path="/students">
                  <StudentIndex />
                </Route>
                <Route>
                  <h2>Signed in with role(s): [{roles}]</h2>
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
