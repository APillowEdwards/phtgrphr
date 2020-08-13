const {EntityRepository} = require("wetland");

class GalleryRepository extends EntityRepository {
  findByGuid(guid) {
    return this.findOne({guid: guid});
  }

  findByGalleryAccessToken(token) {
    return this.getQueryBuilder("g")
      .select("g")
      .innerJoin("g.galleryAccessTokens", "gat")
      .where({"gat.token": token})
      .getQuery()
      .getResult();
  }

  findByUserAccessToken(token) {
    return this.getQueryBuilder("g")
      .select("g")
      .innerJoin("g.user", "u")
      .innerJoin("u.userAccessTokens", "uat")
      .where({"uat.token": token})
      .getQuery()
      .getResult();
  }

  findByUserAccessTokenAndId(token, id) {
    return this.getQueryBuilder("g")
      .select("g")
      .innerJoin("g.user", "u")
      .innerJoin("u.userAccessTokens", "uat")
      .where({
        and: [
          {"uat.token": token},
          {"g.id": id}
        ]
      })
      .getQuery()
      .getResult();
  }
}

module.exports = GalleryRepository;
