const GalleryAccessTokenRepository = require("../repository/GalleryAccessTokenRepository");

class GalleryAccessToken {
  static setMapping(mapping) {
    mapping.entity({repository: GalleryAccessTokenRepository});

    mapping.forProperty("id").primary().increments();

    mapping.manyToOne("gallery", {targetEntity: "Gallery", inversedBy: "galleryAccessTokens"});

    mapping.field("token", {type: "uuid"});
    mapping.field("expiry", {type: "dateTime"});
  }
}

module.exports = GalleryAccessToken;
