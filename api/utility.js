const uuid = require("uuid");

module.exports = {
  GenerateUUID: function () {
    return uuid.v4();
  },

  ValidateUUID: function(guid) {
    return uuid.validate(guid);
  },

  JsonResponse: function(res, object) {
    object.hasError = false
    res.json(object)
  },

  ErrorResponse: function(res, message) {
    res.json({
      "hasError": true,
      "error": {
        "message": message
      }
    })
  },

  GetFileExtension: function(filename) {
    return filename.slice((filename.lastIndexOf(".") - 1 >>> 0) + 2);
  }
}
