const ImageRepository = require('../repository/ImageRepository');

class Image {
  static setMapping(mapping) {
    mapping.entity({repository: ImageRepository});

    mapping.forProperty("id").primary().increments();

    mapping.oneToMany("imageAccessTokens", {targetEntity: "ImageAccessToken", mappedBy: "image"});
    mapping.manyToOne("gallery", {targetEntity: "Gallery", inversedBy: "images"});

    mapping.field("fileName", {type: "string"});
    mapping.field("order", {type: "integer"});
  }
}

module.exports = Image;
