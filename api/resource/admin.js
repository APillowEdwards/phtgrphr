const express = require('express');
const utility = require("../utility");

const router = express.Router();

const UserAccessToken = require('../entity/UserAccessToken');

router.use("/gallery", require("./admin/gallery"));

router.post("/auth", (req, res, next) => {
  let manager = req.wetland.getManager();

  let username = req.body.username;
  let password = req.body.password;

  // TODO: make sure this uses hashing
  manager.getRepository("User").findOne({username: username})
    .then(function(user) {
      if (!user || user.password != password) {
        utility.ErrorResponse(res, "Username or password are incorrect");
        return;
      }

      let userAccessToken = new UserAccessToken;
      userAccessToken.token = utility.GenerateUUID();
      userAccessToken.user_id = user.id;
      userAccessToken.expiry = new Date(Date.now() + (60*60*1000)).toISOString().slice(0, 19).replace('T', ' ');

      manager.persist(userAccessToken).flush()
        .then(function () {
          utility.JsonResponse(res, {
            "token": userAccessToken.token
          });
        })
        .catch(error => res.status(500).json({error}));
    })
    .catch(error => res.status(500).json({error}));
});

module.exports = router
