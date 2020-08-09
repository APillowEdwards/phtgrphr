class Gallery {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.oneToMany("accessTokens", {targetEntity: "GalleryAccessToken", mappedBy: "gallery"});
    mapping.manyToOne("user", {targetEntity: "User", mappedBy: "galleries"});

    mapping.field("name", {type: "string"});
    mapping.field("password", {type: "string"});
  }
}

module.exports = Gallery;
