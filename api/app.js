var express = require("express");
var body_parser = require("body-parser");
var uuid = require('uuid');
var cors = require('cors');
var fs = require('fs');
var path = require('path');

var app = express();
app.use(body_parser.json());
app.use(cors());

app.listen(3000, () => {
  console.log("Server running on port 3000");
});

// =========================================================================
// START utility functions
// =========================================================================

function GenerateUUID() {
  return uuid.v4();
}

function JsonResponse(res, object) {
  object.hasError = false
  res.json(object)
}

function ErrorResponse(res, message) {
  res.json({
    "hasError": true,
    "error": {
      "message": message
    }
  })
}

// =========================================================================
// END utility functions
// =========================================================================


// =========================================================================
// START /check-exists
// =========================================================================

app.get("/check-exists", (req, res, next) => {
  var guid = req.query.guid;

  if(!uuid.validate(guid)) {
    ErrorResponse(res, "Invalid GUID");
    return;
  }

  var guid_exists = CheckGuidExists(guid);

  JsonResponse(res, {
    "exists": guid_exists
  });
});

// TODO
function CheckGuidExists(guid) {
  return guid == "00000000-0000-0000-0000-000000000000";
}

// =========================================================================
// END /check-exists
// =========================================================================



// =========================================================================
// START /gallery-auth
// =========================================================================

app.post("/gallery-auth", (req, res, next) => {
  var guid = req.body.guid;
  var password = req.body.password;

  var token;

  if(!uuid.validate(guid)) {
    ErrorResponse(res, "Invalid GUID");
    return;
  }

  if(!CheckGuidExists(guid)) {
    ErrorResponse(res, "Gallery GUID does not exist");
    return;
  }

  if(!ValidateGuidPassword(guid, password)) {
      ErrorResponse(res, "Incorrect password");
      return;
  }

  token = GenerateGalleryAccessToken(guid);

  JsonResponse(res, {
    "token": token
  });
});

// TODO
function ValidateGuidPassword(guid, password) {
  return guid == "00000000-0000-0000-0000-000000000000" && password == "password";
}

// TODO
function GenerateGalleryAccessToken(guid) {
  return "ffffffff-ffff-ffff-ffff-ffffffffffff";
}

// =========================================================================
// END /gallery-auth
// =========================================================================



// =========================================================================
// START /fetch-image-tokens
// =========================================================================

app.post("/fetch-image-tokens", (req, res, next) => {
  var page = req.body.page; // zero-indexed
  var page_size = req.body.pageSize;
  var gallery_access_token = req.body.token;

  if(page_size < 1) {
    ErrorResponse(res, "Page size must be greater than 0");
    return;
  }

  if(!CheckGalleryAccessTokenExists(gallery_access_token)) {
    ErrorResponse(res, "Gallery access token does not exist or has expired");
    return;
  }

  var all_image_ids = GetImageIdsByGalleryAccessToken(gallery_access_token)

  var index = page * page_size;
  var last_image_index = ((page + 1) * page_size) - 1

  var page_image_ids = []

  while(index <= last_image_index) {
    if (typeof all_image_ids[index] == `undefined`) {
      break;
    }
    page_image_ids.push(all_image_ids[index]);
    index++;
  }

  if(page_image_ids.length < 1) {
    ErrorResponse(res, "No images found on this page");
    return;
  }

  var image_access_tokens = [];
  page_image_ids.forEach(
    id => image_access_tokens.push(GetImageAccessTokenById(id))
  );

  JsonResponse(res, {
    "imageTokens": image_access_tokens
  });
});

// TODO
function CheckGalleryAccessTokenExists(token) {
  return true
}

// TODO
function GetImageIdsByGalleryAccessToken(token) {
  var image_ids = [];

  if (token == "ffffffff-ffff-ffff-ffff-ffffffffffff") {
    image_ids = [0, 1, 2, 3, 4];
  }

  return image_ids
}

// TODO
function GetImageAccessTokenById(id) {
  return [
   "11111111-1111-1111-1111-111111111111",
   "22222222-2222-2222-2222-222222222222",
   "33333333-3333-3333-3333-333333333333",
   "44444444-4444-4444-4444-444444444444",
   "55555555-5555-5555-5555-555555555555"
 ][id];
}

// =========================================================================
// END /fetch-image-tokens
// =========================================================================



// =========================================================================
// START /image
// =========================================================================

app.get("/image", (req, res, next) => {
  var token = req.query.token;

  var file_name = GetFileNameByImageToken(token);

  fs.readFile("C:/Users/looph/Documents/phtgrphr-images/" + file_name, function (err, data) {

    var extensionName = path.extname(file_name);
    var base64Image = new Buffer(data, 'binary').toString('base64');
    var base64String = "data:image/" + extensionName.split('.').pop()+ ";base64," + base64Image;

    res.json({
      "base64": base64String
    });
  })
});

// TODO
function GetFileNameByImageToken(token) {
  var file_name;

  switch(token) {
    case "11111111-1111-1111-1111-111111111111":
      file_name = "1.jpg";
      break;
    case "22222222-2222-2222-2222-222222222222":
      file_name = "2.jpg";
      break;
    case "33333333-3333-3333-3333-333333333333":
      file_name = "3.jpg";
      break;
    case "44444444-4444-4444-4444-444444444444":
      file_name = "4.jpg";
      break;
    case "55555555-5555-5555-5555-555555555555":
      file_name = "5.jpg";
      break;
  }

  return file_name;
}

// =========================================================================
// END /image
// =========================================================================
