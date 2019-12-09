import React, { useState } from "react";

import { getThumbnailURL } from "../../helpers/cloudinaryHelpers";

import {
  Typography,
  Grid,
  Button,
  TextField,
  Select,
  InputLabel,
  MenuItem
} from "@material-ui/core";

export default function SubmitDocument(props) {
  const [docType, setDocType] = useState("Notes");

  let headerText = props.doctype
    ? `Submit ${props.doctype}`
    : "Submit Course Material";
  let uploadButtonText = props.doctype ? `Upload ${props.doctype}` : "Submit";

  console.log(getThumbnailURL(props.fileURL));

  return (
    <>
      <Grid
        container
        spacing={3}
        direction="column"
        alignItems="center"
        justify="center"
      >
        <Typography variant="h5">{headerText}</Typography>
      </Grid>
      <form noValidate autoComplete="off">
        <Grid container spacing={3} style={{ paddingTop: "1em" }}>
          <Grid item xs={7}>
            <TextField
              required
              id="document-title"
              label="Document Title"
              margin="normal"
              variant="outlined"
            />
          </Grid>
          <Grid item xs={5} style={{ marginTop: "1.3em" }}>
            <InputLabel id="doc-type-label">Document Type</InputLabel>
            <Select
              id="document-type"
              value={docType}
              onChange={e => {
                setDocType(e.target.value);
              }}
            >
              <MenuItem value={"Notes"}>Notes</MenuItem>
              <MenuItem value={"Quiz"}>Quiz</MenuItem>
              <MenuItem value={"Exam"}>Exam</MenuItem>
            </Select>
          </Grid>
        </Grid>
      </form>
      <Grid
        container
        spacing={3}
        direction="column"
        alignItems="center"
        justify="center"
      >
        <Grid item xs={12}>
          <center>
            <img
              style={{ width: "70%" }}
              src={getThumbnailURL(props.fileURL)}
              alt="Document Preview"
            />
          </center>
        </Grid>
        <Grid item xs={12}>
          <Button color="primary" variant="contained">
            {uploadButtonText}
          </Button>
        </Grid>
      </Grid>
    </>
  );
}
