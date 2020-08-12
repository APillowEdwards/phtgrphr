const express = require("express");
const body_parser = require("body-parser");
const express_wetland = require("express-wetland");
const _wetland = require("wetland").Wetland;

const app = express();
const wetland = new _wetland(require("./wetland"));

app.use(body_parser.json());
app.use(express_wetland(wetland));

// cors - FOR DEVELOPMENT ONLY
const cors = require("cors");
app.use(cors());

wetland.getMigrator().devMigrations().then(function() {
  app.listen(3000, function() {
    console.log("phtgrphr api running on port 3000");
  });
});

// Resources
app.use("/admin", require("./resource/admin"));
app.use("/gallery", require("./resource/gallery"));
app.use("/image", require("./resource/image"));
