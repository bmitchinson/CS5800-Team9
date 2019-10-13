import React from 'react'
import { Typography, Paper, Grid } from '@material-ui/core'

export default function Course(props) {
    return (
        <Grid item xs={12}>
            <Paper>
                <Typography>
                    {props.code}
                </Typography>
                <Typography>
                    {props.title}
                </Typography>
                <Typography>
                    {props.id}
                </Typography>
            </Paper>
        </Grid>
    )
}