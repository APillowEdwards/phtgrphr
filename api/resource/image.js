const express = require("express");
const fs = require("fs");
const path = require("path");
const utility = require("../utility");

const router = express.Router();

// =========================================================================
// START /image
// =========================================================================

router.get("/", (req, res, next) => {
  let manager = req.wetland.getManager();

  var token = req.query.token;
  var id = req.query.id;

  manager.getRepository("GalleryAccessToken").findByToken(token)
    .then(function(gat) {
      if(!gat) {
        utility.ErrorResponse(res, "Gallery access token does not exist or has expired");
        return;
      }

      manager.getRepository("Image").findFileNameByIdAndGalleryId(id, gat.gallery.id)
        .then(function(image) {
          if(!image) {
            utility.ErrorResponse(res, "Invalid image id");
            return;
          }

          res.sendFile("C:/Users/looph/Documents/phtgrphr-images/" + image[0].fileName);
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));
});

// =========================================================================
// END /image
// =========================================================================

module.exports = router;
