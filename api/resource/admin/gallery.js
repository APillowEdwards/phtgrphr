const express = require("express");
const multer = require("multer");
const utility = require("../../utility");

const router = express.Router();

const upload = multer({
  storage: multer.diskStorage({
    destination: function (req, file, cb) {
      cb(null, '../image-store/')
    },
    filename: function (req, file, cb) {
      var extension = utility.GetFileExtension(file.originalname).toLowerCase();

      cb(null, utility.GenerateUUID() + "." + extension)
    }
  }),
  fileFilter: function (req, file, cb) {
    var extension = utility.GetFileExtension(file.originalname).toLowerCase();
    var acceptable = ["png", "jpg"];

    cb(null, acceptable.includes(extension))
  }
});

const Gallery = require("../../entity/Gallery");
const Image = require("../../entity/Image");

router.get("/list", (req, res, next) => {
  let manager = req.wetland.getManager();

  let token = req.query.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  manager.getRepository("Gallery").findByUserAccessToken(token)
    .then(function(galleries) {
        if (!galleries) {
          utility.ErrorResponse(res, "Token does not exist or has expired");
          return;
        }

        utility.JsonResponse(res, {
          "galleries": galleries
        });
    });
});

router.get("/", (req, res, next) => {
  let manager = req.wetland.getManager();

  let id = req.query.id;
  let token = req.query.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  manager.getRepository("Gallery").findByUserAccessTokenAndId(token, id)
    .then(function(gallery) {
        if (!gallery) {
          utility.ErrorResponse(res, "Invalid token or id");
          return;
        }

        utility.JsonResponse(res, gallery[0]);
    });
});

router.post("/", (req, res, next) => {
  let manager = req.wetland.getManager();

  let id = req.body.id;
  let name = req.body.name;
  let password = req.body.password;
  let token = req.body.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  if (id == 0) {
    let gallery = new Gallery;
    gallery.name = name;
    gallery.password = password;
    gallery.guid = utility.GenerateUUID();
    gallery.user_id = 1; // TODO: refactor this to get the user first, for better error messages too

    manager.persist(gallery).flush()
      .then(function () {
        utility.JsonResponse(res, gallery);
      })
      .catch(error => res.status(500).json({error}));
  }
  else {
    manager.getRepository("Gallery").findByUserAccessTokenAndId(token, id)
      .then(function(gallery) {
          if (!gallery) {
            utility.ErrorResponse(res, "Invalid token or id");
            return;
          }

          gallery[0].name = name;
          gallery[0].password = password;

          manager.flush().then(function () {
            utility.JsonResponse(res, gallery[0]);
          });
      });
  }
});

router.delete("/", (req, res, next) => {
  let manager = req.wetland.getManager();

  let id = req.query.id;
  let token = req.query.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  manager.getRepository("Gallery").findByUserAccessTokenAndId(token, id)
    .then(function(gallery) {
      if (!gallery) {
        utility.ErrorResponse(res, "Invalid token or id");
        return;
      }

      manager.getRepository("Gallery").findOne(id)
        .then(function(g) {
          g.isDeleted = true;

          manager.flush()
            .then(() => utility.JsonResponse(res, {}));
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));
});

router.get("/images", (req, res, next) => {
  let manager = req.wetland.getManager();

  let id = req.query.id;
  let token = req.query.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  manager.getRepository("Gallery").findByUserAccessTokenAndId(token, id)
    .then(function(gallery) {
      if (!gallery) {
        utility.ErrorResponse(res, "Invalid token or id");
        return;
      }

      manager.getRepository("Image").findByGalleryId(id)
        .then(function(images) {
          utility.JsonResponse(res, images);
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));

});

router.post("/images", upload.array("images", 20), (req, res, next) => {
  let manager = req.wetland.getManager();

  let id = req.query.id;
  let token = req.query.token;

  if(!utility.ValidateUUID(token)) {
    utility.ErrorResponse(res, "Invalid token format");
    return;
  }

  manager.getRepository("Gallery").findByUserAccessTokenAndId(token, id)
    .then(function(gallery) {
      if (!gallery) {
        utility.ErrorResponse(res, "Invalid token or id");
        return;
      }

      manager.getRepository("Image").getQueryBuilder("i")
        .select({"max": "i.sort"})
        .innerJoin("i.gallery", "g")
        .where({"g.id": id})
        .getQuery()
        .getSingleScalarResult()
        .then(function(maxSort) {

          for (var i = 0; i < req.files.length; i++) {
            let file = req.files[i];

            let image = new Image;
            image.fileName = file.filename;
            image.sort = maxSort + i + 1;
            image.gallery_id = id;

            manager.persist(image);
          }

          manager.flush()
            .then(function () {
              console.log("Persisted")
              utility.JsonResponse(res, {});
            })
            .catch(error => res.status(500).json({error}));

        })
        .catch(error => res.status(500).json({error}));

    })
    .catch(error =>console.log(error));

})

router.get("/image", (req, res, next) => {
  let manager = req.wetland.getManager();

  var token = req.query.token;
  var id = req.query.id;

  manager.getRepository("Image").findByUserAccessTokenAndId(token, id)
    .then(function(image) {
      if (!image) {
        utility.ErrorResponse(res, "Invalid token or id");
        return;
      }

      res.sendFile("C:/Users/looph/Documents/GitHub/phtgrphr/image-store/" + image[0].fileName);
    })
    .catch(error => res.status(500).json({error}));
});

router.post("/images/sort", (req, res, next) => {
  let manager = req.wetland.getManager();

  var token = req.body.token;
  var queryImages = req.body.images; // expecting {id, sort}
  var galleryId = req.body.galleryId;

  manager.getRepository("Image").findByUserAccessTokenAndGalleryId(token, galleryId)
    .then(function(dbImages) {
      if (!dbImages) {
        utility.ErrorResponse(res, "Invalid token or id");
        return;
      }

      for (var i = 0; i < dbImages.length; i++) {
        let dbImage = dbImages[i];
        // use the index of the corresponding image in the parameter array
        let newSort = queryImages.findIndex(image => image.id == dbImage.id);

        dbImage.sort = newSort;
      }

      manager.flush().then(function () {
        utility.JsonResponse(res, {});
      });
    })
    .catch(error => res.status(500).json({error}));
});

router.delete("/image", (req, res, next) => {
  let manager = req.wetland.getManager();

  var token = req.query.token;
  var id = req.query.id;

  manager.getRepository("Image").findByUserAccessTokenAndId(token, id)
    .then(function(image) {
      if (!image) {
        utility.ErrorResponse(res, "Invalid token or id");
        return;
      }

      manager.getRepository("Image").findOne(id)
        .then(function(result) {
          manager.remove(result).flush()
            .then(() => utility.JsonResponse(res, {}));
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));
});


module.exports = router;
