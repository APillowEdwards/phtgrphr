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

  findIdsByGalleryIdAndSort(galleryId, firstSort, lastSort) {
    return this.getQueryBuilder("i")
      .select("i.id")
      .where({
        and: [
          {"i.sort": {between: [firstSort, lastSort]}},
          {"g.id": galleryId}
        ]
      })
      .innerJoin("i.gallery", "g")
      .orderBy("sort")
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
}

module.exports = ImageRepository;
