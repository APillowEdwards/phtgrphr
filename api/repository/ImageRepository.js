const {EntityRepository} = require("wetland");

class ImageRepository extends EntityRepository {
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
}

module.exports = ImageRepository;
