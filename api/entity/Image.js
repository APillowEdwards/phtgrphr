class Image {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.oneToMany("accessTokens", {targetEntity: "ImageAccessToken", mappedBy: "image"});

    mapping.field("fileName", {type: "string"});
  }
}

module.exports = Image;
