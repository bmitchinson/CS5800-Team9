export function getRole() {
  const jwt = localStorage.getItem("userJWT");
  return isLoggedIn() ? getRoleAndEmailFromJWT(jwt)[0] : null;
}

export function getEmail() {
  const jwt = localStorage.getItem("userJWT");
  return isLoggedIn() ? getRoleAndEmailFromJWT(jwt)[1] : null;
}

export function isAdmin() {
  return getRole() === "Admin";
}

export function isInstructor() {
  return getRole() === "Instructor";
}

export function isStudent() {
  return getRole() === "Student";
}

export function isLoggedIn() {
  const jwt = localStorage.getItem("userJWT");
  return !(jwt === "null" || jwt === null || jwt === "");
}

export function getRoleAndEmailFromJWT(token) {
  if (!token) {
    return "";
  } else {
    var base64Url = token.split(".")[1];
    if (base64Url) {
      var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      var jsonPayload = decodeURIComponent(
        atob(base64)
          .split("")
          .map(function(c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join("")
      );
      return [JSON.parse(jsonPayload).roles[0], JSON.parse(jsonPayload).email];
    } else {
      return "";
    }
  }
}
