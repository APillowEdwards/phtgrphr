const {EntityRepository} = require('wetland');

class GalleryRepository extends EntityRepository {
  findByGuid(guid) {
    return this.findOne({guid: guid});
  }

  findByGalleryAccessToken(token) {
    return this.getQueryBuilder("g")
      .select("g")
      .where({
        and: [
          {"gat.token": token}
        ]
      })
      .innerJoin("g.galleryAccessTokens", "gat")
      .getQuery()
      .getResult();
  }
}

module.exports = GalleryRepository;
