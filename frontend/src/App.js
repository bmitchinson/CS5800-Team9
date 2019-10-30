import React from "react";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

import { createMuiTheme, responsiveFontSizes } from "@material-ui/core/styles";
import { ThemeProvider } from "@material-ui/styles";

import CssBaseline from "@material-ui/core/CssBaseline";

import MainView from "./views/MainView";
import StudentIndex from "./views/StudentIndex/StudentIndex";

import Header from "./components/Header/Header";

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

const useStateWithLocalStorage = localStorageKey => {
  const [value, setValue] = React.useState(
    localStorage.getItem(localStorageKey) || ""
  );
  React.useEffect(() => {
    localStorage.setItem(localStorageKey, value);
  }, [value]);
  return [value, setValue];
};

// include this line if you'd like to clear JWT data
// localStorage.removeItem("userJWT");

const setDummyJWT = (userJWT, setUserJWT) => {
  if (!userJWT) {
    setUserJWT("active-jwt-here");
  }
};

function App() {
  const [userJWT, setUserJWT] = useStateWithLocalStorage("userJWT");

  // setDummyJWT(userJWT, setUserJWT);

  return (
    <Router>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Header />
        <MainView>
          {!userJWT && (
            <Switch>
              <h2>gotta sign in</h2>
            </Switch>
          )}
          {userJWT && (
            <Switch>
              <Route path="/students">
                <StudentIndex />
              </Route>
              <Route>
                <h2>Home</h2>
              </Route>
            </Switch>
          )}
        </MainView>
      </ThemeProvider>
    </Router>
  );
}

export default App;
