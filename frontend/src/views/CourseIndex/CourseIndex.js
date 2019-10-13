import React from 'react';
import { Paper, Typography, Grid } from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
    course: {}
}));

export default function CourseIndex(){
    const classes = useStyles();

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <Paper className={classes.course}>course 1</Paper>
            </Grid>
            <Grid item xs={12}>
                <Paper className={classes.course}>course 2</Paper>
            </Grid>
        </Grid>
    )
}