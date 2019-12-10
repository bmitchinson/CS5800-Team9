export default function notificationPrefs(
  title,
  message,
  type,
  response = null
) {
  if (
    response &&
    response.data &&
    response.data.Errors &&
    response.data.Errors.length
  ) {
    message = "Server msg: " + response.data.Errors[0];
  }
  return {
    title,
    message,
    type,
    insert: "bottom",
    container: "bottom-right",
    animationIn: ["animated", "fadeIn"],
    animationOut: ["animated", "fadeOut"],
    dismiss: {
      duration: 4000
    }
  };
}
