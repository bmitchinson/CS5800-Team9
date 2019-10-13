import React from 'react';
import { Grid, Typography, Button } from '@material-ui/core';

import Course from '../../components/Course/Course';

// TODO: This will be populated from a central state solution upon login 
// TODO: For crud demo: this will be populated by fetching all from the
//                      CourseIndex component itself.

const allCourses = [
    {
        code: "ECE:2201",
        title: "Physics I",
    },
    {
        code: "MUS:1003",
        title: "Into to Piano",
    },
    {
        code: "MATH:3044",
        title: "Matrix Algebra",
    }
]



export default function CourseIndex(){

    const addCourse = () => {
        console.log(`make post request with random course info, then refresh`)
    };

    return (
        <Grid container spacing={4} alignItems='center'>
            <Grid item xs={4}>
                <Typography variant='h2'>
                    Courses
                </Typography>
            </Grid>
            <Grid item xs={7} />
            <Grid item xs={1}>
                <Button 
                    color='primary' 
                    variant='contained'
                    onClick={addCourse}
                    >
                        Add Course
                    </Button>
            </Grid>
            {allCourses.map(course => 
                <Grid item xs={12}>
                    <Course code={course.code} title={course.title} id={course.id}/>
                </Grid>
            )}
        </Grid>
    )
}