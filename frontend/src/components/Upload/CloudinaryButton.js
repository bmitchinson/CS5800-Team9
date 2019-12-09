import React, { useState } from "react";

import { Button, Modal } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

import SubmitDocument from "./SubmitDocument";

const useStyles = makeStyles(theme => ({
  paper: {
    position: "absolute",
    width: "30em",
    marginLeft: "-15em",
    height: "35em",
    marginTop: "-20em",
    left: "50%",
    top: "50%",
    padding: "1.2em",
    paddingTop: "3em",
    backgroundColor: theme.palette.background.paper,
    border: "1px solid #000",
    boxShadow: theme.shadows[5]
  }
}));

export function CloudinaryButton(props) {
  const classes = useStyles();
  const [classifyModalState, setClassifyModalState] = useState(false);
  const [fileURL, setFileURL] = useState(
    "https://res.cloudinary.com/dkfj0xfet/image/upload/v1574969958/classroom/test_qpwosk.pdf"
  );

  const { buttonText } = props;

  let widget = window.cloudinary.createUploadWidget(
    {
      cloudName: "dkfj0xfet",
      uploadPreset: "classroom",
      sources: ["local", "url"],
      styles: widgetStyles
    },
    (e, result) => {
      if (e) {
        console.log("Error uploading your document");
      } else if (result.event === "success") {
        setFileURL(result.info.secure_url);
        setClassifyModalState(true);
      }
    }
  );

  const showWidget = () => {
    widget.open();
  };

  return (
    <>
      <Button variant="contained" color="primary" onClick={showWidget}>
        {buttonText}
      </Button>
      <Modal
        open={classifyModalState}
        onClose={() => {
          setClassifyModalState(false);
        }}
      >
        <div className={classes.paper}>
          <SubmitDocument
            fileURL={fileURL}
            close={() => {
              setClassifyModalState(false);
            }}
          />
        </div>
      </Modal>
    </>
  );
}

const widgetStyles = {
  palette: {
    window: "#FFF",
    windowBorder: "#000",
    tabIcon: "#F7D33A",
    menuIcons: "#000",
    textDark: "#000000",
    textLight: "#FFFFFF",
    link: "#F7D33A",
    action: "#F7D33A",
    inactiveTabIcon: "#0E2F5A",
    error: "#F44235",
    inProgress: "#FFD41F",
    complete: "#FFD41F",
    sourceBg: "#E4EBF1"
  }
};
