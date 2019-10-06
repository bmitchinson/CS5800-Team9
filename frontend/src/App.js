import React from 'react';

import { createMuiTheme, responsiveFontSizes } from '@material-ui/core/styles';
import { ThemeProvider } from '@material-ui/styles';

import Header from './components/Header/Header';
import Demo from './components/Demo/Demo';

const logo = 'https://res.cloudinary.com/dheqbiqti/image/upload/r_max,fl_progressive,w_100,h_100/v1569869260/classroom/GitHub-Mark.png'

let theme = createMuiTheme({
  palette: {
    primary: {
      main: '#F2E86D'
    },
    secondary: {
      main: '#C2C6A7'
    },
  }
});
theme = responsiveFontSizes(theme);

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Header/>
      <Demo />
    </ThemeProvider>
  );
}

export default App;