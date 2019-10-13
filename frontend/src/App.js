import React from 'react';

import { createMuiTheme, responsiveFontSizes } from '@material-ui/core/styles';
import { ThemeProvider } from '@material-ui/styles';

import CssBaseline from '@material-ui/core/CssBaseline';

import MainView from './views/MainView';
import CourseIndex from './views/CourseIndex/CourseIndex';

import Header from './components/Header/Header';

let theme = createMuiTheme({
  palette: {
    primary: {
      main: '#FFF796'
    },
    secondary: {
      main: '#C2C6A7'
    },
    background: {
      default: '#FFFDEB'
    },
  }
});
theme = responsiveFontSizes(theme);



function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Header/>
      <MainView>
        <CourseIndex />
      </MainView>
    </ThemeProvider>
  );
}

export default App;
