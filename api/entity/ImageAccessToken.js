class ImageAccessToken {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.manyToOne("image", {targetEntity: "Image", inversedBy: "accessTokens"});

    mapping.field("guid", {type: "uuid"});
    mapping.field("expiry", {type: "dateTime"});

  }
}

module.exports = ImageAccessToken;
