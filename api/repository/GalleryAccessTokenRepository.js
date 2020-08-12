const {EntityRepository} = require('wetland');

class GalleryAccessTokenRepository extends EntityRepository {
  findByToken(token) {
    return this.findOne({token: token}, {populate: ["gallery"]});
  }
}

module.exports = GalleryAccessTokenRepository;
