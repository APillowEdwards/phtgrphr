class UserAccessToken {
  static setMapping(mapping) {
    mapping.forProperty("id").primary().increments();

    mapping.manyToOne("user", {targetEntity: "User", inversedBy: "userAccessTokens"});

    mapping.field("token", {type: "uuid"});
    mapping.field("expiry", {type: "dateTime"});
  }
}

module.exports = UserAccessToken;
