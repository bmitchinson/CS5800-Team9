import React from 'react'

import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
    mainview: {
        margin: '2%'
    }
}));

export default function MainView(props){
    const classes = useStyles();    

    return (
        <div className={classes.mainview}>
            {props.children}
        </div>
    )
}