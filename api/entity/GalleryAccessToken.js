class GalleryAccessToken {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.manyToOne("gallery", {targetEntity: "Gallery", inversedBy: "accessTokens"});

    mapping.field("guid", {type: "uuid"});
    mapping.field("expiry", {type: "dateTime"});
  }
}

module.exports = GalleryAccessToken;
