export default function notificationPrefs(title, message, type) {
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
