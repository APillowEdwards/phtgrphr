class ImageAccessToken {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.manyToOne("image", {targetEntity: "Image", inversedBy: "imageAccessTokens"});
    mapping.manyToOne("galleryAccessToken", {targetEntity: "GalleryAccessToken", inversedBy: "imageAccessTokens"});

    mapping.field("token", {type: "uuid"});
    mapping.field("expiry", {type: "dateTime"});
  }
}

module.exports = ImageAccessToken;
