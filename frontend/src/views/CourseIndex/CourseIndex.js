import React from 'react';
import { Grid } from '@material-ui/core';

import Course from '../../components/Course/Course';

// TODO: This will be populated from a central state solution upon login 
// TODO: For crud demo: this will be populated by fetching all from the
//                      CourseIndex component itself.

const allCourses = [
    {
        code: "ECE:2201",
        title: "Physics I",
        id: 1023
    },
    {
        code: "MUS:1003",
        title: "Into to Piano",
        id: 1024
    },
    {
        code: "MATH:3044",
        title: "Matrix Algebra",
        id: 1025
    }
]

export default function CourseIndex(){
    return (
        <Grid container spacing={2}>
            {allCourses.map(course => 
                <Grid item xs={12}>
                    <Course code={course.code} title={course.title} id={course.id}/>
                </Grid>
            )}
        </Grid>
    )
}