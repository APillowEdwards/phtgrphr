const express = require("express");
const utility = require("../utility");

const router = express.Router();

const GalleryAccessToken = require("../entity/GalleryAccessToken");

// =========================================================================
// START Shared Functions
// =========================================================================



// =========================================================================
// END Shared Functions
// =========================================================================

// =========================================================================
// START /gallery
// =========================================================================

router.get("/", (req, res, next) => {
  let manager = req.wetland.getManager();

  var token = req.query.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  manager.getRepository("Gallery").findByGalleryAccessToken(token)
    .then(function(gallery) {
      if (!gallery) {
        utility.ErrorResponse(res, "Token does not exist or has expired");
        return;
      }

      gallery = gallery[0]

      utility.JsonResponse(res, {
        "name": gallery.name
      });
    })
    .catch(error => res.status(500).json({error}));
});

// =========================================================================
// END /gallery
// =========================================================================



// =========================================================================
// START /gallery/exists
// =========================================================================

router.get("/exists", (req, res, next) => {
  let manager = req.wetland.getManager();

  let guid = req.query.guid;

  if(!utility.ValidateUUID(guid)) {
    utility.ErrorResponse(res, "Invalid gallery GUID");
    return;
  }

  manager.getRepository("Gallery").findByGuid(guid)
    .then(function(result) {
      var guid_exists = result ? true : false;

      utility.JsonResponse(res, {
        "exists": guid_exists
      });
    })
    .catch(error => res.status(500).json({error}));
});

// =========================================================================
// END /gallery/exists
// =========================================================================



// =========================================================================
// START /gallery/auth
// =========================================================================

router.post("/auth", (req, res, next) => {
  let manager = req.wetland.getManager();

  let guid = req.body.guid;
  let password = req.body.password;

  if(!utility.ValidateUUID(guid)) {
    utility.ErrorResponse(res, "Invalid gallery GUID");
    return;
  }

  // TODO: make sure this uses hashing
  manager.getRepository("Gallery").findByGuid(guid)
    .then(function(gallery) {
      if (!gallery) {
        utility.ErrorResponse(res, "Gallery does not exist");
        return;
      }

      if (gallery.password != password) {
        utility.ErrorResponse(res, "Invalid password");
        return;
      }

      let galleryAccessToken = new GalleryAccessToken;
      galleryAccessToken.token = utility.GenerateUUID();
      galleryAccessToken.gallery_id = gallery.id;
      galleryAccessToken.expiry = new Date(Date.now() + (60*60*1000)).toISOString().slice(0, 19).replace('T', ' ');

      manager.persist(galleryAccessToken).flush()
        .then(function () {
          utility.JsonResponse(res, {
            "token": galleryAccessToken.token
          });
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));
});

// =========================================================================
// END /gallery/auth
// =========================================================================



// =========================================================================
// START /gallery/images
// =========================================================================

router.get("/images", (req, res, next) => {
  let manager = req.wetland.getManager()

  var page = parseInt(req.query.page);
  var pageSize = parseInt(req.query.pageSize);
  var token = req.query.token;

  if(pageSize < 1) {
    utility.ErrorResponse(res, "Page size must be greater than 0");
    return;
  }

  if(page < 1) {
    utility.ErrorResponse(res, "Page number must be greater than 0");
    return;
  }

  if (!token || !utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Gallery access token undefined or invalid");
    return;
  }

  manager.getRepository("GalleryAccessToken").findByToken(token)
    .then(function(gat) {
      if(!gat) {
        utility.ErrorResponse(res, "Token does not exist or has expired");
        return;
      }

      var firstSort = (page - 1) * pageSize;
      var lastSort = firstSort + (pageSize - 1);


      manager.getRepository("Image").findIdsByGalleryIdAndSort(gat.gallery.id, firstSort, lastSort)
        .then(function(images) {
          if(!images) {
            utility.ErrorResponse(res, "No images found on this page");
            return;
          }

          utility.JsonResponse(res, {
            images: images
          })
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));

});

// =========================================================================
// END /gallery/image-tokens
// =========================================================================

module.exports = router;
