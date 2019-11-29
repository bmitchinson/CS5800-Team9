export default function getHeaders() {
  return {
    "Content-Type": "application/json",
    Authorization: "bearer " + localStorage.getItem("userJWT")
  };
}
