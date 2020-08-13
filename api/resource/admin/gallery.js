const express = require("express");
const utility = require("../../utility");

const router  = express.Router();

const Gallery = require("../../entity/Gallery");

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

          console.log(gallery)

          return manager.flush().then(function () {
            utility.JsonResponse(res, gallery[0]);
          });
      });
  }
});

router.delete("/", (req, res, next) => {
  let manager = req.wetland.getManager();

  let id = req.body.id;
  let token = req.body.token;

  console.log(id)

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

        return manager.remove(gallery).flush().then(function () {
          utility.JsonResponse(res, {});
        });
    });
});

module.exports = router;
