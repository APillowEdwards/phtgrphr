const express = require('express');

const router = express.Router();

router.use("/gallery", require("./admin/gallery"));

module.exports = router
