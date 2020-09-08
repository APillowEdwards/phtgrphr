const ImageRepository = require("../repository/ImageRepository");

class Image {
  static setMapping(mapping) {
    mapping.entity({repository: ImageRepository});

    mapping.forProperty("id").primary().increments();

    mapping.manyToOne("gallery", {targetEntity: "Gallery", inversedBy: "images"});

    mapping.field("fileName", {type: "string"});
    mapping.field("sort", {type: "integer"});
  }
}

module.exports = Image;
