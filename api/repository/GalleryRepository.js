const {EntityRepository} = require("wetland");

class GalleryRepository extends EntityRepository {
  findByGuid(guid) {
    return this.findOne({guid: guid});
  }

  findByGalleryAccessToken(token) {
    return this.getQueryBuilder("g")
      .select("g")
      .innerJoin("g.galleryAccessTokens", "gat")
      .where({"gat.token": token, "g.IsDeleted": false})
      .getQuery()
      .getResult();
  }

  findByUserAccessToken(token) {
    return this.getQueryBuilder("g")
      .select("g")
      .innerJoin("g.user", "u")
      .innerJoin("u.userAccessTokens", "uat")
      .where({"uat.token": token, "g.IsDeleted": false})
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
          {"uat.token": token, "g.id": id, "g.IsDeleted": false}
        ]
      })
      .getQuery()
      .getResult();
  }

  getImageCount(id) {
    return this.getQueryBuilder("g")
      .select({count: "i.id"})
      .innerJoin("g.images", "i")
      .where({"g.id": id})
      .getQuery()
      .getSingleScalarResult();
  }
}

module.exports = GalleryRepository;
