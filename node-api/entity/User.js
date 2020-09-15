// TODO: make sure this uses hashing
class User {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.oneToMany("userAccessTokens", {targetEntity: "UserAccessToken", mappedBy: "user"});
    mapping.oneToMany("galleries", {targetEntity: "Gallery", mappedBy: "user"});

    mapping.field("name", {type: "string"});
    mapping.field("username", {type: "string"});
    mapping.field("password", {type: "string"});
  }
}

module.exports = User;
