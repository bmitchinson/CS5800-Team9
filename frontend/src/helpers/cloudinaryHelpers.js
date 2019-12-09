export function getThumbnailURL(baseurl) {
  if (!baseurl.includes(".pdf")) {
    return "https://res.cloudinary.com/dkfj0xfet/image/upload/v1574971810/classroom/seed/docicon_ejjur9.png";
  }
  return baseurl
    .replace("/upload/", "/upload/w_340,h_440/")
    .replace(".pdf", ".png");
}

export function getTitle(doc) {
  return doc.resourceLink.split("/")[11].slice(0, -11);
}
