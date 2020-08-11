const GalleryRepository = require('../repository/GalleryRepository');

class Gallery {
  static setMapping(mapping) {
    mapping.entity({repository: GalleryRepository});

    mapping.forProperty("id").primary().increments();

    mapping.oneToMany("galleryAccessTokens", {targetEntity: "GalleryAccessToken", mappedBy: "gallery"});
    mapping.oneToMany("images", {targetEntity: "Image", mappedBy: "gallery"});
    mapping.manyToOne("user", {targetEntity: "User", inversedBy: "galleries"});

    mapping.field("name", {type: "string"});
    mapping.field("password", {type: "string"});
    mapping.field("guid", {type: "uuid"});
  }
}

module.exports = Gallery;
