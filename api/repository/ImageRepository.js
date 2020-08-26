const {EntityRepository} = require("wetland");

class ImageRepository extends EntityRepository {

  findByGalleryId(galleryId) {
    return this.getQueryBuilder("i")
      .select("i")
      .where({"g.id": galleryId})
      .innerJoin("i.gallery", "g")
      .orderBy("sort")
      .getQuery()
      .getResult();
  }

  findIdsByGalleryIdAndOffsetAndLimit(galleryId, offset, limit) {
    return this.getQueryBuilder("i")
      .select("i.id")
      .where({"g.id": galleryId})
      .innerJoin("i.gallery", "g")
      .orderBy("sort")
      .offset(offset)
      .limit(limit)
      .getQuery()
      .getResult();
  }

  findFileNameByIdAndGalleryId(id, galleryId) {
    return this.getQueryBuilder("i")
      .select("i.fileName")
      .where({
        and: [
          {"i.id": id},
          {"g.id": galleryId}
        ]
      })
      .innerJoin("i.gallery", "g")
      .getQuery()
      .getResult();
  }

  findByUserAccessTokenAndId(token, id) {
    return this.getQueryBuilder("i")
      .select("i")
      .innerJoin("i.gallery", "g")
      .innerJoin("g.user", "u")
      .innerJoin("u.userAccessTokens", "uat")
      .where({
        and: [
          {"uat.token": token},
          {"i.id": id}
        ]
      })
      .getQuery()
      .getResult();
  }

  findByUserAccessTokenAndGalleryId(token, galleryId) {
    return this.getQueryBuilder("i")
      .select("i")
      .innerJoin("i.gallery", "g")
      .innerJoin("g.user", "u")
      .innerJoin("u.userAccessTokens", "uat")
      .where({
        and: [
          {"uat.token": token},
          {"g.id": galleryId}
        ]
      })
      .getQuery()
      .getResult();
  }
}

module.exports = ImageRepository;
