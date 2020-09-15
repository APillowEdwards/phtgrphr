var path = require("path");
var defaultStore = require("./defaultStore");


module.exports = {
  entityPath: path.resolve(process.cwd(), "entity"),
  stores: {
    defaultStore: defaultStore
  }
}
